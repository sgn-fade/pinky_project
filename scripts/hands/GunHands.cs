using System;
using Godot;

namespace projectpinky.scripts.hands;

public partial class GunHands : Hands
{
    private AnimatedSprite2D sprite;
    private float shootCooldown;


    private void Rotating()
    {
        LookAt(GetGlobalMousePosition());
    }

    public override void PlayAnimation(string animationName)
    {
        sprite.Play(animationName);
    }
}