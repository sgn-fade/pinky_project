using Godot;
using projectpinky.scripts.drops;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.spells;

public class Spell
{
    public SpellData Data { get; set; }
    private PlayerLoader player;
    public double TimeSpend { get; set; }

    public Spell(SpellData data)
    {
        Data = data;
        player = Global.Player;
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