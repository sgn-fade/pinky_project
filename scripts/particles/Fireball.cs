using Godot;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.particles;

public partial class Fireball : CharacterBody2D
{
    [Export] public float Speed = 200f;

    public override void _Ready()
    {
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
    private void DeleteFireball()
    {
       //GetNode("Sprite2D").Play("shoot");
        SetProcess(false);
        QueueFree();
    }

    private void OnBodyEntered(Node body)
    {
        DeleteFireball();
        //todo event bus
        Global.EventBus.EmitSignal("damage_to_enemy", body, 20, "burn");
    }
}