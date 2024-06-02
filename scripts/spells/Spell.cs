using Godot;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.spells;

public abstract partial class Spell : Node
{
    public string AnimationName { get; set; }
    public int Rarity { get; set; }
    public bool IsReady { get; set; } = true;
    public float CooldownTime { get; set; }
    public float TimeSpend { get; set; }
    public int ManaCost { get; set; }
    public PackedScene Particle { get; set; }
    public PackedScene InventoryItemScene { get; set; } = GD.Load<PackedScene>("res://scripts/drops/inventory_item.gd");
    public Node InventoryItem { get; set; }

    private PlayerData player = GetNode<PlayerData>("/root/PlayerData");
    public void Cast()
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