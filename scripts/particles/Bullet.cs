using Godot;
using projectpinky.scripts.Enemies;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.particles;

public partial class Bullet : CharacterBody2D
{
    private float speed = 300;
    private Vector2 mousePos;
    private Vector2 characterPos;
    private float maxDistance = 10000;

    public override void _Ready()
    {
        Position = Global.Player.GetPosition();
        mousePos = GetGlobalMousePosition();
        LookAt(GetGlobalMousePosition());
        characterPos = Position;
    }

    public override void _Process(double delta)
    {
        if ((GlobalPosition - characterPos).Length() < maxDistance)
        {
            Velocity = (mousePos - characterPos).Normalized() * speed;
            MoveAndSlide();
        }
        else
        {
            Delete();
        }
    }

    private void _on_Body_entered(Node2D body)
    {
        if (body is Enemy enemy)
        {
            //todo enemy take damage
        }
        Delete();
    }

    private void Delete()
    {
        SetProcess(false);
        QueueFree();
    }
}