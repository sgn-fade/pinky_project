using Godot;
using System.Threading.Tasks;
using projectpinky.scripts.Globals;
namespace projectpinky.scripts.particles;
public partial class FirePillar : CharacterBody2D
{
    private Timer _timer = new Timer();
    private Timer _lightTimer = new Timer();
    private Timer _particlesTimer = new Timer();

    [Export] public NodePath DamageAreaPath;
    [Export] public NodePath PullsTgAreaPath;
    [Export] public NodePath LightPath;
    [Export] public NodePath FloorLightPath;
    [Export] public NodePath SoundPath;
    [Export] public NodePath SpritePath;
    [Export] public NodePath StartParticles1Path;
    [Export] public NodePath StartParticles2Path;
    [Export] public NodePath StartParticles3Path;

    private Area2D _damageArea;
    private Area2D _pullsTgArea;
    private Light2D _light;
    private Light2D _floorLight;
    private AudioStreamPlayer _sound;
    private AnimatedSprite2D _sprite;
    private CpuParticles2D _startParticles1;
    private CpuParticles2D _startParticles2;
    private CpuParticles2D _startParticles3;

    public override void _Ready()
    {   
        //todo ochen' mnogo tut getNode
        _damageArea = GetNode<Area2D>(DamageAreaPath);
        _pullsTgArea = GetNode<Area2D>(PullsTgAreaPath);
        _light = GetNode<Light2D>(LightPath);
        _floorLight = GetNode<Light2D>(FloorLightPath);
        _sound = GetNode<AudioStreamPlayer>(SoundPath);
        _sprite = GetNode<AnimatedSprite2D>(SpritePath);
        _startParticles1 = GetNode<CpuParticles2D>(StartParticles1Path);
        _startParticles2 = GetNode<CpuParticles2D>(StartParticles2Path);
        _startParticles3 = GetNode<CpuParticles2D>(StartParticles3Path);

        _damageArea.Connect("body_entered", new Callable(this, nameof(OnDamageAreaBodyEntered)));
        _pullsTgArea.Connect("body_entered", new Callable(this, nameof(OnPullsTgAreaBodyEntered)));

        _sound.Playing = true;

        _timer.OneShot = false;
        _lightTimer.OneShot = false;
        _particlesTimer.OneShot = false;

        AddChild(_timer);
        AddChild(_lightTimer);
        AddChild(_particlesTimer);

        EmitStartParticles();

        GlobalPosition = GetGlobalMousePosition();

        _timer.Start(0.5f);
        TimerTimeoutAsync(_timer).ContinueWith(_ =>
        {
            LightTransform();
            _sprite.Visible = true;
            _sprite.Frame = 0;
            _sprite.Play("cast");
            _timer.Start(1.5f);
            TimerTimeoutAsync(_timer).ContinueWith(__ => QueueFree());
        });
    }

    public override void _Process(double delta)
    {
        if (_sprite.Frame == 8)
        {
            _damageArea.Monitoring = true;
            _pullsTgArea.Monitoring = false;
        }
    }

    private async Task TimerTimeoutAsync(Timer timer)
    {
        await ToSignal(timer, "timeout");
    }

    private async void EmitStartParticles()
    {
        _particlesTimer.Start(0.2f);
        await TimerTimeoutAsync(_particlesTimer);
        _startParticles1.Emitting = true;
        _startParticles2.Emitting = true;
        _startParticles3.Emitting = true;
    }

    private async void LightTransform()
    {
        for (int i = 0; i < 5; i++)
        {
            _floorLight.Energy += 0.2f;
            _floorLight.Scale += new Vector2(0.15f, 0.1f);
            _light.Energy += 0.4f;
            _light.Scale += new Vector2(0.01f, 0f);

            _lightTimer.Start(0.12f);
            await TimerTimeoutAsync(_lightTimer);
        }
        for (int i = 0; i < 5; i++)
        {
            _floorLight.Energy -= 0.2f;
            _floorLight.Scale -= new Vector2(0.15f, 0.1f);
            _light.Energy -= 0.4f;
            _light.Scale += new Vector2(0.2f, 0f);

            _lightTimer.Start(0.1f);
            await TimerTimeoutAsync(_lightTimer);
        }
    }

    private void OnDamageAreaBodyEntered(Node body)
    {
        Global.EventBus.EmitSignal("damage_to_enemy", body, 8, "burn");
    }

    private void OnPullsTgAreaBodyEntered(Node body)
    {
        Global.EventBus.EmitSignal("pulls_body", body, GlobalPosition);
    }
}
