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

    [Export] public Dictionary<TextureProgressBar, Spell> Bars = new();

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

    public void StartDashCooldown()
    {
        dashIndicator.StartCooldown();
    }

    //todo connect to player
    private void ChangeModuleCardVisible(bool state, Texture2D texture = null)
    {
        GetNode<Control>("module_discription").Visible = state;
        GetNode<TextureRect>("module_discription/module_icon").Texture = texture;
    }

    private void ChangeWeaponCardVisible(bool state, Texture2D texture = null)
    {
        GetNode<Control>("weapon_discription").Visible = state;
        GetNode<TextureRect>("weapon_discription/weapon_texture").Texture = texture;
    }


    private void SetSpellIconToGame(Spell spell, int index)
    {
        var bar = barsParent.GetChild<TextureProgressBar>(index);
        bar.MaxValue = spell.GetMaxCooldownTime() * 1000;

        bar.Value = spell.GetCooldownTime() * 1000;

        bar.TextureProgress = spell.InvItem.Icon;
        Bars[bar] = spell;
    }

    private void ClearSpellIcons()
    {
        foreach (var node in GetNode("spell_slot_panel").GetChildren())
        {
            if (node is TextureProgressBar textureProgress)
            {
                textureProgress.TextureProgress = null;
            }
        }
    }


    public void UpdateHpValue(int hp, int maxHp)
    {
        playerBars.UpdateHpValue(hp, maxHp);
    }

    public void UpdateManaValue(int mana, int maxMana)
    {
        playerBars.UpdateManaValue(mana, maxMana);
    }
}