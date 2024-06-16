using System;
using System.Threading.Tasks;
using Godot;
using projectpinky.scripts.drops;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.spells;

[GlobalClass]
public abstract partial class Spell : Resource
{
    [Export] public Texture2D Icon { get; set; }
    [Export] public string AnimationName { get; set; }
    [Export] public Rarities Rarity { get; set; }
    [Export] public float CooldownTime { get; set; }
    [Export] public int ManaCost { get; set; }
    [Export] public PackedScene Particle { get; set; }
    public InventoryItem InvItem { get; set; }

    private PlayerData player = Global.Player;
    public bool IsReady { get; set; } = true;
    public float TimeSpend { get; set; }


    public Spell() : this(null, null, Rarities.Bronze, 0, 0, null)
    {
    }

    public enum Rarities
    {
        Bronze,
        Silver,
        Gold,
    }
    public Spell(Texture2D icon, string animationName, Rarities rarity, float cooldownTime, int manaCost,
        PackedScene particle)
    {
        Icon = icon;
        AnimationName = animationName;
        Rarity = rarity;
        CooldownTime = cooldownTime;
        ManaCost = manaCost;
        Particle = particle;
        TimeSpend = CooldownTime;
        var backgroundTexture = GD.Load<Texture2D>($"res://sprites/ui/{Rarity}_module_button_state.png");
        InvItem = new InventoryItem(this, "spell", icon, backgroundTexture);
    }

    public void Cast()
    {
        if (player.SetMana(-ManaCost))
        {
            if (Particle != null)
            {
                Global.GlobalWorldInfo.GetWorld().AddChild(Particle.Instantiate());
            }

            Global.Player.PlayAnimation(AnimationName);
            Cooldown();
        }
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