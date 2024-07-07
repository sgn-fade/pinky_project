using System;
using Godot;

namespace projectpinky.scripts.hands;

public partial class ClearHands : Hands
{
    [Export] private AnimatedSprite2D sprite;

    public override void _Ready()
    {
        sprite = GetNode<AnimatedSprite2D>("sprite");
    }
    public override void PlayAnimation(string animationName)
    {
        sprite.Play(animationName);
    }
}