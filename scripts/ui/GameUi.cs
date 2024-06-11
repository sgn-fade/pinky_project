using System.Collections.Generic;
using Godot;
using projectpinky.scripts.Globals;
using projectpinky.scripts.spells;

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

        // EventBus.Connect("set_spell_icon_to_game", this, nameof(OnSetSpellIconToGame));
        // EventBus.Connect("start_spell_cooldown", this, nameof(OnStartSpellCooldown));
        // EventBus.Connect("remove_spell_icon_from_game", this, nameof(OnRemoveSpellIconFromGame));
        // EventBus.Connect("clear_spell_icons", this, nameof(ClearSpellIcons));
        //
        // EventBus.Connect("show_module_stats_on_game_screen", this, nameof(OnShowModuleStatsOnGameScreen));
        // EventBus.Connect("hide_module_stats_on_game_screen", this, nameof(OnHideModuleStatsOnGameScreen));
        // EventBus.Connect("show_weapon_stats_on_game_screen", this, nameof(OnShowWeaponStatsOnGameScreen));
        // EventBus.Connect("hide_weapon_stats_on_game_screen", this, nameof(OnHideWeaponStatsOnGameScreen));
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
                //Bars[bar.Name].SetCooldownTime(delta);
            }
        }
    }

    public void StartDashCooldown()
    {
        dashIndicator.StartCooldown();
    }
    private void OnShowModuleStatsOnGameScreen(Spell module)
    {
        GetNode<Control>("module_discription").Visible = true;
        //GetNode<TextureRect>("module_discription/module_icon").Texture = module.InventoryItem.Icon;
    }

    private void OnHideModuleStatsOnGameScreen()
    {
        GetNode<Control>("module_discription").Visible = false;
    }

    private void OnShowWeaponStatsOnGameScreen(Node2D weapon)
    {
        GetNode<Control>("weapon_discription").Visible = true;
        //GetNode<TextureRect>("weapon_discription/weapon_texture").Texture = weapon.Icon;
    }

    private void OnHideWeaponStatsOnGameScreen()
    {
        GetNode<Control>("weapon_discription").Visible = false;
    }

    private void OnRemoveSpellIconFromGame(string button)
    {
        GetNode<TextureProgressBar>($"spell_slot_panel/{button}").TextureProgress = null;
        Bars[button] = null;
    }

    private void OnSetSpellIconToGame(Spell module, string button)
    {
        var bar = GetNode<TextureProgressBar>($"spell_slot_panel/{button}");
        bar.MaxValue = module.GetMaxCooldownTime() * 1000;

        if (module.GetReady())
        {
            bar.Value = bar.MaxValue;
        }
        else
        {
            bar.Value = module.GetCooldownTime() * 1000;
        }

        //bar.TextureProgress = module.InventoryItem.Icon;
        Bars[button] = module;
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