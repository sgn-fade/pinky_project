using Godot;
using System;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.Enemies.Goblins;

public partial class Goblin : CharacterBody2D
{
    [Export] private AnimatedSprite2D sprite;

    protected enum States
    {
        Idle,
        Move,
        Pulls,
        Attack,
        DealsDamage,
        Searching,
        None
    }

    protected States currentState = States.Idle;
    private Vector2 direction = Vector2.Zero;
    protected float speed = 100.0f;

    private Vector2 RandomizeDirection()
    {
        Random random = new Random();
        int dir = random.Next(1, 5);
        switch (dir)
        {
            case 1: return new Vector2(-1, 0);
            case 2: return new Vector2(1, 0);
            case 3: return new Vector2(0, -1);
            case 4: return new Vector2(0, 1);
            default: return Vector2.Zero;
        }
    }

    protected async void PlayAnimation(string name)
    {
        sprite.Play(name);
        await ToSignal(sprite, "animation_finished");
    }

    protected void SwapSpriteDirection()
    {
        if (direction.X <= 0)
        {
            Scale = new Vector2(-1, Scale.Y);
        }
        else if (direction.X > 0)
        {
            Scale = new Vector2(1, Scale.Y);
        }
    }

    protected void Move(Vector2 dir)
    {
        direction = dir;
        Velocity = direction.Normalized() * speed;
        MoveAndSlide();
        if (direction.Length() >= 1000)
        {
            currentState = States.Idle;
        }
    }

}