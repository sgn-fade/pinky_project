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

    private Dictionary<string, Spell> Bars = new()
    {
        {"slot1", null},
        {"slot2", null},
        {"slot3", null},
        {"slot4", null},
        {"slot5", null},
        {"slot6", null},
    };

    public override void _Ready()
    {
        eventBus = Global.EventBus;
        //todo event bus
        // EventBus.Connect("set_spell_icon_to_game", this, nameof(OnSetSpellIconToGame));
        // EventBus.Connect("start_spell_cooldown", this, nameof(OnStartSpellCooldown));
        // EventBus.Connect("remove_spell_icon_from_game", this, nameof(OnRemoveSpellIconFromGame));
        // EventBus.Connect("clear_spell_icons", this, nameof(ClearSpellIcons));

    }

    public override void _Process(double delta)
    {
        foreach (var barNode in GetNode("spell_slot_panel").GetChildren())
        {
            var bar = barNode as ProgressBar;
            if (bar == null || Bars[bar.Name] == null)
                continue;

            if (bar.Value <= bar.MaxValue)
            {
                bar.Value += delta * 1000;
                Bars[bar.Name].AddSpendTime(delta);
            }
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

    private void OnRemoveSpellIconFromGame(string button)
    {
        GetNode<TextureProgressBar>($"spell_slot_panel/{button}").TextureProgress = null;
        Bars[button] = null;
    }

    private void OnSetSpellIconToGame(Spell spell, string button)
    {
        var bar = GetNode<TextureProgressBar>($"spell_slot_panel/{button}");
        bar.MaxValue = spell.GetMaxCooldownTime() * 1000;

        if (spell.GetReady())
        {
            bar.Value = bar.MaxValue;
        }
        else
        {
            bar.Value = spell.GetCooldownTime() * 1000;
        }

        //bar.TextureProgress = module.InventoryItem.Icon;
        Bars[button] = spell;
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

    private void OnStartSpellCooldown(string button)
    {
        var bar = GetNode<TextureProgressBar>($"spell_slot_panel/{button}");
        bar.Value = Bars[button].GetCooldownTime() * 1000;
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