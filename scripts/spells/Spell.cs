using System.Threading.Tasks;
using Godot;
using projectpinky.scripts.drops;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.spells;

public class Spell
{
    public string AnimationName { get; set; }
    public string Rarity { get; set; }
    public bool IsReady { get; set; } = true;
    public float CooldownTime { get; set; }
    public float TimeSpend { get; set; }
    public int ManaCost { get; set; }
    public PackedScene Particle { get; set; }
    public InventoryItem InvItem { get; set; }

    private PlayerData player = Global.Player;


    public Task Cast()
    {
        if (player.ChangeMana(-ManaCost))
        {
            if (Particle != null)
            {
                //GlobalWorldInfo.GetWorld().AddChild(Particle.Instantiate());
            }
            //EventBus.Instance.EmitSignal("hands_play_animation", AnimationName);
            Cooldown();
        }

        return Task.CompletedTask;
    }

    public bool GetReady()
    {
        return !(TimeSpend < CooldownTime);
    }

    public void Cooldown()
    {
        TimeSpend = 0;
        IsReady = false;
    }

    public float GetCooldownTime()
    {
        return TimeSpend;
    }

    public float GetMaxCooldownTime()
    {
        return CooldownTime;
    }

    public void SetCooldownTime(float value)
    {
        TimeSpend += value;
    }
}