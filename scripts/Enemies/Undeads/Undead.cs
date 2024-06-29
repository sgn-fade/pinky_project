using Godot;
using System;

public class Undead : Enemy
{
    private string type = "undead";
    private AnimatedSprite sprite;
    private Area2D body;

    public override void _Ready()
    {
        sprite = GetNode<AnimatedSprite>("Sprite2D");
        body = GetNode<Area2D>("Area2D");
    }

    private void Move()
    {
        SwapSpriteDirection();
        // sprite.Play("move");
        if (hp > 0 && Player.GetHp() > 0)
        {
            SetVelocity(GetDirection().Normalized() * speed);
            MoveAndSlide();
        }
    }

    private void SwapSpriteDirection()
    {
        if (GetDirection().x <= 0)
        {
            body.Transform = new Transform2D(1, 0, 0, 1, body.Transform.origin);
        }
        else if (GetDirection().x > 0)
        {
            body.Transform = new Transform2D(-1, 0, 0, 1, body.Transform.origin);
        }
    }

    private Vector2 GetDirection()
    {
        // Implement your method to get the direction here
        return new Vector2(); // Placeholder
    }
}