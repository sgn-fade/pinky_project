namespace projectpinky.scripts.weapons.Magic;

public class MagicWeapon : Weapon
{
    private int mana;
    private int magicDamage;

    public MagicWeapon()
    {
        Type = Types.Magic;
    }
}