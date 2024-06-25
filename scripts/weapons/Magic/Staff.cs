using Godot;
using projectpinky.scripts.drops;

namespace projectpinky.scripts.weapons.Magic;

public class Staff : MagicWeapon
{
   public Staff() : base()
    {
        Texture = GD.Load<Texture2D>("res://sprites/weapons/magic_weapons/old_goblins_magic_wand.png");
        var invIcon = GD.Load<Texture2D>("res://sprites/ui/inventory/item_icons/item_icon_goblin's_wand.png");
        InvItem = new InventoryItem(this, "weapon", invIcon);
        HandsScene = GD.Load<PackedScene>("res://scenes/hands/magic_hands.tscn");
    }
}