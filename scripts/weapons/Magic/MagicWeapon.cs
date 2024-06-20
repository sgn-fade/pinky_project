namespace projectpinky.scripts.weapons.Magic;

public partial class MagicWeapon : Weapon
{
    private int mana;
    private int magicDamage;

    public override void _Ready()
    {
        base._Ready();
        Type = Types.Magic;
    }
}