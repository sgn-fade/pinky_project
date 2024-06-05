using Godot;
using projectpinky.scripts.drops;

namespace projectpinky.scripts.spells;

public class FireEyeSpell : Spell
{
    public FireEyeSpell()
    {
        Rarity = "bronze";
        var spellIcon = GD.Load<Texture2D>("res://sprites/spell_icons/fire_eye_spell_icon.png");
        var backgroundTexture = GD.Load<Texture2D>($"res://sprites/ui/{Rarity}_module_button_state.png");
        InvItem = new InventoryItem(this, "spell", spellIcon, backgroundTexture);
        AnimationName = "pillar_cast";
        CooldownTime = 5;
        TimeSpend = 5;
        ManaCost = 10;
        Particle = GD.Load<PackedScene>("res://scenes/fire_eye.tscn");
    }
}