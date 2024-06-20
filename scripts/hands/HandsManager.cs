using Godot;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.hands;

public partial class HandsManager : Node2D
{
    private readonly PackedScene clear = GD.Load<PackedScene>("res://scenes/hands/clear_hands.tscn");

    private Hands currentHands;

    public override void _Ready()
    {
        AddChild(clear.Instantiate());
    }

    public void SwitchHandsStance(PackedScene newHands)
    {
        if (newHands == null)
        {
            AddChild(clear.Instantiate());
            return;
        }
        if (GetChildCount() > 0) GetChild(0).QueueFree();
        AddChild(newHands.Instantiate());
    }

    public void PlayAnimation(string animationName)
    {
        currentHands.PlayAnimation(animationName);
    }
}