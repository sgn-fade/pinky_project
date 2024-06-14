namespace projectpinky.scripts.weapons.Range;

public partial class RangeWeapon : Weapon
{
    public override void _Ready()
    {
        base._Ready();
        Type = Types.Range;

    }
}