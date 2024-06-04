using Godot;
using projectpinky.scripts.drops;

namespace projectpinky.scripts.spells;

public partial class FireballSpell : Spell
{
    public FireballSpell()
    {
        Rarity = "bronze";
        var spellIcon = GD.Load<Texture>("res://sprites/spell_icons/fireball_icon.png");
        var backgroundTexture = GD.Load<Texture>($"res://sprites/ui/{Rarity}_module_button_state.png");
        InvItem = new InventoryItem("spell", spellIcon, backgroundTexture);
        AnimationName = "fireball_cast";
        CooldownTime = 1;
        TimeSpend = 1;
        ManaCost = 3;
        Particle = GD.Load<PackedScene>("res://scenes/fireball.tscn");
    }
}