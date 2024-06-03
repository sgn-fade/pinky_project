using Godot;

namespace projectpinky.scripts.hands;

public abstract class Hands : Node2D
{
    public abstract void PlayAnimation(string animationName);
}