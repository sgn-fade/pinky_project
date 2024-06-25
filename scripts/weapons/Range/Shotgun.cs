using Godot;

namespace projectpinky.scripts.weapons.Range;

public class Shotgun : RangeWeapon
{
    [Export] public int ShootRate;
    public Shotgun() : base()
    {
        base.
        HandsScene = GD.Load<PackedScene>("res://scenes/hands/shotgun_hands.tscn");
    }
}