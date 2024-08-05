using Godot;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.drops;

public partial class Coin : CharacterBody2D
{
    private Vector2 direction = Vector2.Zero;
    private CollisionShape2D collision;
    private Area2D area;
    private enum States
    {
        Idle,
        Move
    }
    private States currentState = States.Idle;

    public override void _Ready()
    {
        collision = GetNode<CollisionShape2D>("damage_area/collision");
        area = GetNode<Area2D>("damage_area");
    }

    public override void _Process(double delta)
    {
        switch (currentState)
        {
            case States.Idle:
                IdleState();
                break;
            case States.Move:
                MoveState();
                break;
        }
    }

    private void IdleState()
    {
        if (!area.OverlapsBody(Global.PlayerLoader.GetBody())) return;

        collision.Disabled = true;
        currentState = States.Move;
    }

    private void MoveState()
    {
        direction = Global.PlayerLoader.GetPosition() - GlobalPosition;
        Velocity = direction.Normalized() * 100;
        MoveAndSlide();

        if (!((Global.PlayerLoader.GetPosition() - GlobalPosition).Length() < 7)) return;

        //Global.Player.SetMoney(1);
        QueueFree();
    }
}