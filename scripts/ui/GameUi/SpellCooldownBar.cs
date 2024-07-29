using Godot;
using projectpinky.scripts.spells;

namespace projectpinky.scripts.ui;

public partial class SpellCooldownBar : TextureProgressBar
{
    public Spell LinkedSpell { get; set; }
    public override void _Process(double delta)
    {
        if (Value < MaxValue)
        {
            LinkedSpell.AddSpendTime(delta);
            Value = LinkedSpell.TimeSpend * 1000;
        }
    }

    public void LinkSpell(Spell newSpell)
    {
        MaxValue = newSpell.CooldownTime * 1000;
        Value = newSpell.TimeSpend * 1000;
        TextureProgress = newSpell.Icon;
    }

    public void UnlinkSpell()
    {
        LinkedSpell = null;
        TextureProgress = null;
    }
}