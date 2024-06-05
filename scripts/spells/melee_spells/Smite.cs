using Godot;
using projectpinky.scripts.drops;

namespace projectpinky.scripts.spells.melee_spells;

public class Smite : Spell
{
    public Smite()
    {
        Rarity = "bronze";
        var spellIcon = GD.Load<Texture2D>("res://sprites/spell_icons/sword_smite.png");
        var backgroundTexture = GD.Load<Texture2D>($"res://sprites/ui/{Rarity}_module_button_state.png");
        InvItem = new InventoryItem(this, "spell", spellIcon, backgroundTexture);
        AnimationName = null;
        CooldownTime = 0;
        ManaCost = 0;
        Particle = null;
        //Player.SetSmite(true);
    }
}