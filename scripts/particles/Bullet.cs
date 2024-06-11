using Godot;
using projectpinky.scripts.Enemies;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.particles;

public partial class Bullet : CharacterBody2D
{
    private float speed = 300;
    private Vector2 startPosition;
    private Vector2 targetPosition;
    private float maxDistance = 10000;


    public void Init(Vector2 startPos, Vector2 targetPos)
    {
        GlobalPosition = startPos;
        LookAt(targetPos * 10000);

        startPosition = startPos;
        targetPosition = targetPos;

    }
    public override void _Process(double delta)
    {
        if ((GlobalPosition - startPosition).Length() < maxDistance)
        {
            Velocity = targetPosition.Normalized() * speed;
            MoveAndSlide();
        }
        else
        {
            Delete();
        }
    }

    private void OnBodyEntered(Node2D body)
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