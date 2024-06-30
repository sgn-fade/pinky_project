using Godot;
using projectpinky.scripts.Enemies;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.particles;

public partial class Bullet : CharacterBody2D
{
    private float Speed = 300;

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

    private void Delete()
    {
        SetProcess(false);
        QueueFree();
    }
}