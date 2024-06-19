using System;
using System.Collections;
using System.Collections.Generic;
using Godot;
using projectpinky.scripts.drops;
using projectpinky.scripts.Globals;
using projectpinky.scripts.spells;

namespace projectpinky.scripts.weapons;

public partial class Weapon : Node
{
    [Export] public Texture2D Texture { get; set; }
    [Export] public string Rarity { get; set; }
    public Types Type { get; set; }
    public int Damage { get; set; }
    private double criticalChance = 10d;
    public PackedScene HandsScene;
    public double GetCriticalChance() => criticalChance;
    
    public InventoryItem InvItem { get; set; }

    private readonly Dictionary<int, string> buttonsBinds = Options.ButtonsBinds;
    private Dictionary<string, Spell> spellsButtons;

    public enum Types
    {
        Melee,
        Range,
        Magic
    }

    private Cell[] cells = new Cell[8];

    public Cell[] GetCells()
    {
        return cells;
    }

    public override void _Ready()
    {
        spellsButtons = new Dictionary<string, Spell>
        {
            {buttonsBinds[0], null},
            {buttonsBinds[1], null},
            {buttonsBinds[2], null},
            {buttonsBinds[3], null},
            {buttonsBinds[4], null},
            {buttonsBinds[5], null}
        };
        for (int i = 0; i < 4; i++)
        {
            var index = GD.Randi() % cells.Length;
            cells[index] ??= new Cell();
        }
    }

    public override void _Input(InputEvent @event)
    {
        foreach (var kvp in buttonsBinds)
        {
            if (!Input.IsActionJustPressed(kvp.Value)) continue;

            var action = spellsButtons[kvp.Value];
            if (action.GetReady()) action.Cast();
            return;
        }
    }

    public void AddModuleToWeapon(Spell module, int cellIndex)
    {
        cells[cellIndex].Module = module;
        foreach (var key in buttonsBinds.Keys)
        {
            if (spellsButtons[buttonsBinds[key]] == null)
            {
                cells[cellIndex].Button = key;
                spellsButtons[buttonsBinds[key]] = module;
                //EventBus.EmitSignal("set_spell_icon_to_game", module, cells[cellIndex].button);
                return;
            }
        }
    }

    public void RemoveModuleFromWeapon(int cellIndex)
    {
        //EventBus.EmitSignal("remove_spell_icon_from_game", cells[cellIndex].button);
        spellsButtons[buttonsBinds[cells[cellIndex].Button]] = null;
        cells[cellIndex].Button = -1;
        cells[cellIndex].Module = null;
    }

    private void SwapModules(int firstSlot, int secondSlot)
    {
        var firstModule = spellsButtons[buttonsBinds[firstSlot]];
        var secondModule = spellsButtons[buttonsBinds[secondSlot]];
        spellsButtons[buttonsBinds[firstSlot]] = secondModule;
        spellsButtons[buttonsBinds[secondSlot]] = firstModule;

        if (spellsButtons[buttonsBinds[firstSlot]] != null)
        {
            //EventBus.EmitSignal("set_spell_icon_to_game", spellsButtons[buttonsBinds[firstSlot]], firstSlot);
        }
        else
        {
            //EventBus.EmitSignal("remove_spell_icon_from_game", firstSlot);
        }

        if (spellsButtons[buttonsBinds[secondSlot]] != null)
        {
            //EventBus.EmitSignal("set_spell_icon_to_game", spellsButtons[buttonsBinds[secondSlot]], secondSlot);
        }
        else
        {
            //EventBus.EmitSignal("remove_spell_icon_from_game", secondSlot);
        }
    }

    protected void AddBaseSpell(Spell module)
    {
        var cell = cells[(int)(GD.Randi() % cells.Length)];
        cell.Module = module;
        cell.Button = 0;
        spellsButtons[buttonsBinds[cell.Button]] = module;
        //EventBus.EmitSignal("set_spell_icon_to_game", module, cell.button);
    }


    public class Cell
    {
        public int Button { get; set; }
        public int Index { get; set; }
        private Spell _module;

        public Spell Module
        {
            get => _module;
            set => _module = value;
        }
        public Node Link { get; set; }

        public Cell()
        {
            Module = null;
        }
    }
}