using Godot;

namespace projectpinky.scripts.hands;

public abstract partial class Hands : Node2D
{
    public abstract void PlayAnimation(string animationName);
}