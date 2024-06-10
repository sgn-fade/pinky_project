using Godot;
using System.Threading.Tasks;
using projectpinky.scripts.Globals;
namespace projectpinky.scripts.particles;
public partial class CShotgunOrb : CharacterBody2D
{
    //todo animation tree
    private Node2D _barrel;
    private AnimatedSprite2D _bulletSprite;
    private Area2D _area;
    private CpuParticles2D _endParticlesPink;
    private CpuParticles2D _endParticlesWhite;
    private PointLight2D _pointLight;
    private Timer _timer;
    private Vector2 _characterPos;
    private float _speed;

    public override void _Ready()
    {
        _barrel = GetNode<Node2D>("/root/World/main_character/hands/heands_continuum_shotgun/pos");
        _bulletSprite = GetNode<AnimatedSprite2D>("$b");
        _area = GetNode<Area2D>("$Area2D");
        _endParticlesPink = GetNode<CpuParticles2D>("$end_partcl_pink");
        _endParticlesWhite = GetNode<CpuParticles2D>("$end_partcl_white");
        _pointLight = GetNode<PointLight2D>("$PointLight2D");

        _timer = new Timer();
        _timer.OneShot = false;
        AddChild(_timer);

        _speed = 100;
        GlobalPosition = _barrel.GlobalPosition;
        _bulletSprite.Play("bullet");

        _area.Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));

        _characterPos = Global.Player.GlobalPosition;
        _characterPos.Y += 1.5f;
        LookAt(_characterPos);
    }

    private async Task Delete()
    {
        _bulletSprite.Play("explode");
        _timer.Start(0.33f);
        await ToSignal(_timer, "timeout");
        _bulletSprite.QueueFree();
        _endParticlesPink.Emitting = true;
        _endParticlesWhite.Emitting = true;
        _area.QueueFree();

        for (int i = 0; i < 4; i++)
        {
            _pointLight.TextureScale += 0.02f;
            _timer.Start(0.04f);
            await ToSignal(_timer, "timeout");
        }

        _timer.Start(0.5f);
        await ToSignal(_timer, "timeout");
        QueueFree();
    }

    private void OnBodyEntered(Node body)
    {
        //todo
    }
}