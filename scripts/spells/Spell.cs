using projectpinky.scripts.drops;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.spells;

public class Spell
{
    public SpellData Data { get; set; }
    public InventoryItem InvItem { get; set; }
    private PlayerData player;
    public float TimeSpend { get; set; }


    public Spell(SpellData data)
    {
        Data = data;
        InvItem = new InventoryItem(this, "spell", data.Icon, data.BackgroundTexture);
        player = Global.Player;
    }
    public void Cast()
    {
        if (player.SetMana(-Data.ManaCost))
        {
            if (Data.Particle != null)
            {
                Global.GlobalWorldInfo.GetWorld().AddChild(Data.Particle.Instantiate());
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

    public float GetCooldownTime()
    {
        return TimeSpend;
    }

    public float GetMaxCooldownTime()
    {
        return Data.CooldownTime;
    }

    public void SetCooldownTime(float value)
    {
        TimeSpend += value;
    }
}