using Godot;

namespace projectpinky.scripts.weapons;
[GlobalClass]
public partial class WeaponData : Resource
{
    public enum Types
    {
        Melee,
        Range,
        Magic
    }
    [Export] public Texture2D Texture { get; set; }
    [Export] public Texture2D InventoryIcon { get; set; }
    [Export] public Types Type { get; set; } = Types.Melee;
    [Export] public PackedScene HandsScene { get; set; }
    [Export] public int Damage { get; set; }
}