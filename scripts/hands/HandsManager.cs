using Godot;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.hands;

public partial class HandsManager : Node2D
{
    private readonly PackedScene magic = GD.Load<PackedScene>("res://scenes/hands/magic_hands.tscn");
    private readonly PackedScene clear = GD.Load<PackedScene>("res://scenes/hands/clear_hands.tscn");
    private readonly PackedScene melee = GD.Load<PackedScene>("res://scenes/hands/melee_hands.tscn");
    private readonly PackedScene sword = GD.Load<PackedScene>("res://scenes/hands/sword_hands.tscn");

    private enum States
    {
        Empty,
        Magic,
        Gun,
        Melee
    }

    private EventBus eventBus = Global.EventBus;
    private States currentState = States.Gun;
    private Hands currentHands;

    public override void _Ready()
    {
        SwitchHands(clear);
        //eventBus.Connect("switch_hands_stance", this, "_OnSwitchHandsStance");
    }

    public void SwitchHandsStance(object weapon)
    {
        if (weapon == null)
        {
            SwitchHands(clear);
            return;
        }

        switch (weapon.GetType().ToString())
        {
            case "magic":
                currentState = States.Magic;
                SwitchHands(magic);
                break;
            case "melee":
                currentState = States.Melee;
                SwitchHands(sword);
                break;
        }
    }

    private void SwitchHands(PackedScene type)
    {
        if (GetChildCount() > 0)
        {
            GetChild(0).QueueFree();
        }

        currentHands = (Hands)type.Instantiate();
        AddChild(currentHands);
    }

    public void PlayAnimation(string animationName)
    {
        currentHands.PlayAnimation(animationName);
    }
}