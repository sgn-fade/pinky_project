using Godot;
using System;

public partial class FireElemental : Elemental
{
    private object currentState = null;
    private float attackCooldown = 0;
    private enum States
    {
        IDLE,
        MOVE,
        ATTACK,
    }

    public override void _Ready()
    {
        hp = 20;
        hpBar.MaxValue = hp * 10;
        hpBar.Value = hp * 10;
        whiteAnimationBar.MaxValue = hp * 10;
        whiteAnimationBar.Value = hp * 10;
    }

    public override void _Process(float delta)
    {
        Move(Player.GetPosition() - GlobalPosition);
    }

    private void Move(Vector2 direction)
    {
        if (hp > 0 && Player.GetHp() > 0)
        {
            SwapSpriteDirection(direction);
            currentState = States.MOVE;
            SetVelocity(direction.Normalized() * speed);
            MoveAndSlide();
        }
    }

    private void SwapSpriteDirection(Vector2 direction)
    {
        if (direction.x <= 0)
        {
            GetNode<Node2D>("body").Transform = new Transform2D(1, 0, 0, 1, GetNode<Node2D>("body").Transform.origin);
        }
        else if (direction.x > 0)
        {
            GetNode<Node2D>("body").Transform = new Transform2D(-1, 0, 0, 1, GetNode<Node2D>("body").Transform.origin);
        }
    }
}