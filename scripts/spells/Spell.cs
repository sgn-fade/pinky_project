using System;
using System.Threading.Tasks;
using Godot;
using projectpinky.scripts.drops;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.spells;

[GlobalClass] [Tool]
public partial class Spell : InventoryItem
{
    [Export] public Rarities Rarity { get; set; }
    [Export] public float CooldownTime { get; set; }
    [Export] public int ManaCost { get; set; }
    [Export] public PackedScene Particle { get; set; }

    public double TimeSpend { get; set; }

    public bool GetReady() => !(TimeSpend < CooldownTime);

    public void Cooldown() => TimeSpend = 0;

    public void AddSpendTime(double value) => TimeSpend += value;

    public Spell()
    {
        Rarity = Rarities.Bronze;
        CooldownTime = 0;
        ManaCost = 0;
        Background = GD.Load<Texture2D>($"res://sprites/ui/{Rarity}_module_button_state.png");
        Particle = null;
    }
    public enum Rarities
    {
        Bronze,
        Silver,
        Gold,
    }
}