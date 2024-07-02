using System.Collections.Generic;
using Godot;
using projectpinky.scripts.Globals;
using projectpinky.scripts.spells;
using projectpinky.scripts.weapons;

namespace projectpinky.scripts.ui;

public partial class GameUi : Control
{
    private EventBus eventBus;
    [Export] private PlayerBars playerBars;

    public List<SpellCooldownBar> Bars = new();

    [Export] public Control barsParent;

    public override void _Ready()
    {
        for (var i = 0; i < barsParent.GetChildCount(); i++)
        {
            Bars.Add(barsParent.GetChild<SpellCooldownBar>(i));
        }
    }
    private void ClearSpellIcons()
    {
        foreach (var bar in Bars)
        {
            bar.UnlinkSpell();
        }
    }
}