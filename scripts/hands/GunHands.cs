using System;
using Godot;

namespace projectpinky.scripts.hands;

public partial class CustomStaticBody2D : Hands
{
    private Node world;
    private AnimatedSprite2D sprite;
    private float shootCooldown;
    private Timer timer = new ();
    private float preparationAnimationTime;

    private enum DefaultStates
    {
        Idle,
        Shoot,
        Reload
    }

    private DefaultStates currentState = DefaultStates.Idle;

    public override void _Ready()
    {
        world = GetNode("/root/World");
        sprite = GetNode<AnimatedSprite2D>("sprite");
        Creating();
    }

    private void Creating()
    {
        AddChild(timer);
        timer.OneShot = false;
    }

    private void Rotating()
    {
        LookAt(GetGlobalMousePosition());
    }

    private void SetIdleState()
    {
        sprite.Play("idle");
        sprite.Stop();
        sprite.Frame = 0;
        currentState = DefaultStates.Idle;
    }

    public override void PlayAnimation(string animationName)
    {
        sprite.Play(animationName);
    }
}