using Godot;
using projectpinky.scripts.drops;

namespace projectpinky.scripts.weapons.melee;

public partial class Sword : MeleeWeapon
{
    private Texture weaponSpritePath = (Texture)GD.Load("res://sprites/weapons/melee/goblin_sword.png");

    private int critChance = 24;

    public override void _Ready()
    {
        base._Ready();
        Texture2D icon = GD.Load<Texture2D>("res://sprites/weapons/melee/goblin_sword_inventory.png");
        InvItem = new InventoryItem(this, "weapon", icon);
        Damage = 4;
        ComboCount = 2;
        Type = "melee";
    }
    public Texture GetWeaponSpritePath()
    {
        return weaponSpritePath;
    }
}