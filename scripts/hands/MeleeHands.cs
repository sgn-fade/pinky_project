using Godot;
using System;
using System.Collections.Generic;
using projectpinky.scripts.Globals;
using projectpinky.scripts.hands;
using projectpinky.scripts.player;

public partial class MeleeHands : Hands
{

    public override void _Process(double delta)
    {
        if (GetNode<AnimationPlayer>("anim").CurrentAnimation == "")
        {
            LookAt(GetGlobalMousePosition());
        }
    }

    public override void PlayAnimation(string animationName)
    {
        if (animationName == "null")
        {
            return;
        }
        GetNode<AnimationPlayer>("anim").Play(animationName);
    }
}