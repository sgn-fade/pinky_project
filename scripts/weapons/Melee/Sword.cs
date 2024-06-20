using Godot;
using projectpinky.scripts.drops;

namespace projectpinky.scripts.weapons.melee;

public partial class Sword : MeleeWeapon
{
    public override void _Ready()
    {
        base._Ready();
        var icon = GD.Load<Texture2D>("res://sprites/weapons/melee/goblin_sword_inventory.png");
        HandsScene = GD.Load<PackedScene>("res://scenes/hands/sword_hands.tscn");
        Texture = GD.Load<Texture2D>("res://sprites/weapons/melee/goblin_sword.png");
        InvItem = new InventoryItem(this, "weapon", icon);
        Damage = 4;
        ComboCount = 2;
    }
}