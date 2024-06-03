using Godot;
using projectpinky.scripts.drops;

namespace projectpinky.scripts.spells;

public class FireSpearSpell : Spell
{
    public FireSpearSpell()
    {
        Rarity = "bronze";
        var spellIcon = GD.Load<Texture>("res://sprites/spell_icons/fire_spear_spell_icon.png");
        var backgroundTexture = GD.Load<Texture>($"res://sprites/ui/{Rarity}_module_button_state.png");
        InvItem = new InventoryItem("spell", spellIcon, backgroundTexture);
        AnimationName = "spear_throw";
        CooldownTime = 4;
        TimeSpend = 4;
        ManaCost = 7;
        Particle = GD.Load<PackedScene>("res://scenes/spell_particles/fire_spear_particle.tscn");
    }

    // public override async Task Cast()
    // {
    //     Cooldown();
    //     EventBus.EmitSignal("hands_play_animation", 0.58f, AnimationName);
    //     EventBus.EmitSignal("player_cast_spell", 0.58f, AnimationName);
    //     //await GlobalWorldInfo.GetWorld().GetTree().CreateTimer(0.33f).WaitDeferred();
    //     //GlobalWorldInfo.GetWorld().AddChild(Particle.Instantiate());
    // }
}