using Godot;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.particles;

public partial class Fireball : CharacterBody2D
{
    [Export] public float Speed = 200f;

    private GpuParticles2D _particles;
    private GpuParticles2D _endParticles;
    private Area2D _area;
    private Timer _timer;

    public override void _Ready()
    {
        _particles = GetNode<GpuParticles2D>("particles");
        _endParticles = GetNode<GpuParticles2D>("end_partcl");
        _area = GetNode<Area2D>("Area2D");
        _timer = new Timer();
        _timer.OneShot = false;
        AddChild(_timer);

        _area.Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));

        GetNode<AnimatedSprite2D>("Sprite2D").Play("shoot");

        Vector2 endPosition = GetGlobalMousePosition();
        GlobalPosition = Global.Player.GetPosition();
        GlobalPosition -= new Vector2(0, 5);
        Velocity = (endPosition - Global.Player.GetPosition()).Normalized() * Speed;
        LookAt(endPosition);
    }

    public override void _Process(double delta)
    {
        Velocity = Velocity.Normalized() * Speed;
        MoveAndSlide();
    }

    private async void DeleteFireball()
    {
        SetProcess(false);
        _endParticles.Emitting = true;
        GetNode<AnimatedSprite2D>("Sprite2D").Visible = false;
        _particles.Emitting = false;

        var light = GetNode<Light2D>("light");
        var collisionShape = GetNode<CollisionShape2D>("Area2D/CollisionShape2D");

        for (int i = 0; i < 15; i++)
        {
  
            light.Scale += new Vector2(0.02f, 0.02f);
            light.Energy -= 0.05f;
            collisionShape.Scale += new Vector2(0.03f, 0.03f);
            _timer.Start(0.03f);
            await ToSignal(_timer, "timeout");
        }

        QueueFree();
    }

    private void OnBodyEntered(Node body)
    {
        DeleteFireball();
        //todo event bus
        Global.EventBus.EmitSignal("damage_to_enemy", body, 20, "burn");
    }
}