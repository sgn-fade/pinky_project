using Godot;
using projectpinky.scripts.Globals;
using projectpinky.scripts.player;

namespace projectpinky.scripts.hands;

public partial class PlayerView : Node2D
{
    private readonly PackedScene clear = GD.Load<PackedScene>("res://scenes/hands/clear_hands.tscn");
    [Export] private Player _player;
    private Hands _currentHands;

    public void Init()
    {
        SwitchHandsStance(clear);
    }

    public void SwitchHandsStance(PackedScene newHands)
    {
        _currentHands?.QueueFree();
        _currentHands = newHands == null ? clear.Instantiate<Hands>() : newHands.Instantiate<Hands>();
        _currentHands.Player = _player;
        AddChild(_currentHands);
    }
}