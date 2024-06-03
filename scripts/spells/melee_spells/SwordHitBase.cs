using Godot;
using projectpinky.scripts.drops;

namespace projectpinky.scripts.spells.melee_spells;

public class SwordHitBase : Spell
{
    public SwordHitBase()
    {
        Rarity = "bronze";
        var spellIcon = GD.Load<Texture>("res://sprites/spell_icons/sword_hit.png");
        var backgroundTexture = GD.Load<Texture>($"res://sprites/ui/{Rarity}_module_button_state.png");
        InvItem = new InventoryItem("spell", spellIcon, backgroundTexture);
        AnimationName = null;
        CooldownTime = 0.5f;
        TimeSpend = 0.5f;
        ManaCost = 0;
        Particle = null;
    }
}