using Godot;

namespace projectpinky.scripts.weapons.Range;

public partial class Shotgun : RangeWeapon
{
    public override void _Ready()
    {
        base._Ready();
        HandsScene = GD.Load<PackedScene>("res://scenes/hands/shotgun_hands.tscn");
    }
}