using Godot;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.hands;

public partial class HandsManager : Node2D
{
    private readonly PackedScene magic = GD.Load<PackedScene>("res://scenes/hands/magic_hands.tscn");
    private readonly PackedScene clear = GD.Load<PackedScene>("res://scenes/hands/clear_hands.tscn");
    private readonly PackedScene melee = GD.Load<PackedScene>("res://scenes/hands/melee_hands.tscn");
    private readonly PackedScene sword = GD.Load<PackedScene>("res://scenes/hands/sword_hands.tscn");

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