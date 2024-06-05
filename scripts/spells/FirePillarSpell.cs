using Godot;
using projectpinky.scripts.drops;

namespace projectpinky.scripts.spells;

public class FirePillarSpell : Spell
{
    public FirePillarSpell()
    {
        Rarity = "bronze";
        var spellIcon = GD.Load<Texture2D>("res://sprites/spell_icons/fire_pillar_icon.png");
        var backgroundTexture = GD.Load<Texture2D>($"res://sprites/ui/{Rarity}_module_button_state.png");
        InvItem = new InventoryItem(this, "spell", spellIcon, backgroundTexture);
        AnimationName = "pillar_cast";
        CooldownTime = 8;
        TimeSpend = 8;
        ManaCost = 13;
        Particle = GD.Load<PackedScene>("res://scenes/particles/fire_pillar.tscn");
    }
}