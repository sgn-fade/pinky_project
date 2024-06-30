using Godot;
using projectpinky.scripts.drops;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.spells;

public class Spell
{
    public SpellData Data { get; set; }
    private PlayerData player;
    public double TimeSpend { get; set; }

    public Spell(SpellData data)
    {
        Data = data;
        player = Global.Player;
    }
    public void Cast()
    {
        if (player.SetMana(-Data.ManaCost))
        {
            if (Data.Particle != null)
            {
                Global.World.AddEntity(Data.Particle);
            }
            Global.Player.PlayAnimation(Data.AnimationName);
            Cooldown();
        }
    }

    public bool GetReady()
    {
        return !(TimeSpend < Data.CooldownTime);
    }

    public void Cooldown()
    {
        TimeSpend = 0;
    }

    public double GetCooldownTime()
    {
        return TimeSpend;
    }

    public float GetMaxCooldownTime()
    {
        return Data.CooldownTime;
    }

    public void AddSpendTime(double value)
    {
        TimeSpend += value;
    }
}