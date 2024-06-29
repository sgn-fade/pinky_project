using Godot;
using projectpinky.scripts.Enemies;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.particles;

public partial class Fireball : SpellController
{
    [Export] public float Speed = 200f;

    public override void _Ready()
    {
        Vector2 endPosition = GetGlobalMousePosition();
        GlobalPosition = Global.Player.GetPosition() - new Vector2(0, 5);
        Velocity = (endPosition - Global.Player.GetPosition()).Normalized() * Speed;
        LookAt(endPosition);
    }

    public override void _Process(double delta)
    {
        MoveAndSlide();
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body is Enemy enemy)
        {
            enemy.TakeDamage(10);
        }
        Delete();
    }

    protected override void Delete() => QueueFree();
}