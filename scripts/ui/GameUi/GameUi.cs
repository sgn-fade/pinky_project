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
    [Export] private DashIndicator dashIndicator;

    public Dictionary<TextureProgressBar, Spell> Bars = new();

    [Export] public Control barsParent;

    public override void _Ready()
    {
        for (var i = 0; i < barsParent.GetChildCount(); i++)
        {
            Bars.Add(barsParent.GetChild<TextureProgressBar>(i), null);
        }
    }

    public override void _Process(double delta)
    {
        foreach (var bar in Bars)
        {
            if (bar.Value == null || bar.Value.GetReady())
                continue;

            bar.Value.AddSpendTime(delta);
            bar.Key.Value = bar.Value.TimeSpend * 1000;
        }
    }

    private void SetSpellIconToGame(Spell spell, int index)
    {
        var bar = barsParent.GetChild<TextureProgressBar>(index);
        bar.MaxValue = spell.GetMaxCooldownTime() * 1000;

        bar.Value = spell.GetCooldownTime() * 1000;

        bar.TextureProgress = spell.Data.Icon;
        Bars[bar] = spell;
    }

    private void ClearSpellIcons()
    {
        foreach (var node in barsParent.GetChildren())
        {
            if (node is TextureProgressBar textureProgress)
            {
                textureProgress.TextureProgress = null;
            }
        }
    }
}