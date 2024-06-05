using System.Threading.Tasks;
using Godot;
using projectpinky.scripts.drops;
using projectpinky.scripts.Globals;
using projectpinky.scripts.player;

namespace projectpinky.scripts.spells;

public class FireTeleportSpell : Spell
{
    // public override async Task Cast()
    // {
    //     Cooldown();
    //     EventBus.EmitSignal("player_teleport", Player.GetBody().GetLocalMousePosition());
    //     EventBus.EmitSignal("hands_play_animation", 1.25f, "teleport_start");
    //     await ToSignal(EventBus, "spell_animation_ended");
    //     EventBus.EmitSignal("hands_play_animation", 0.833f, "teleport_end");
    // }

    public FireTeleportSpell()
    {
        Rarity = "bronze";
        var spellIcon = GD.Load<Texture2D>("res://sprites/spell_icons/fire_teleport_icon.png");
        var backgroundTexture = GD.Load<Texture2D>($"res://sprites/ui/{Rarity}_module_button_state.png");
        InvItem = new InventoryItem(this, "spell", spellIcon, backgroundTexture);
        AnimationName = null;
        CooldownTime = 8;
        TimeSpend = 8;
        ManaCost = 20;
        Particle = null;
    }
}