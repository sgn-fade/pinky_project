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
    public string Type { get; set; } = "none";
    public int Damage { get; set; }
    private double criticalChance = 10d;

    public double GetCriticalChance() => criticalChance;
    
    public InventoryItem InvItem { get; set; }

    private readonly Dictionary<int, string> buttonsBinds = Options.ButtonsBinds;
    private Dictionary<string, Spell> spellsButtons;

    //todo made it with nodes in ui
    private readonly List<Vector2> modulePositionList = new()
    {
        new Vector2(418, 123),
        new Vector2(649, 209),
        new Vector2(733, 446),
        new Vector2(649, 685),
        new Vector2(418, 767),
        new Vector2(187, 684),
        new Vector2(103, 446),
        new Vector2(188, 208)
    };

    private List<Cell> cells = new();

    public List<Cell> GetCells()
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
        var positions = new List<Vector2>(modulePositionList);
        for (int i = 0; i < 4; i++)
        {
            int index = (int)(GD.Randi() % positions.Count);
            Vector2 pos = positions[index];
            positions.RemoveAt(index);
            cells.Add(new Cell(pos));
        }
    }

    public override async void _Input(InputEvent @event)
    {
        foreach (var kvp in buttonsBinds)
        {
            if (!Input.IsActionJustPressed(kvp.Value)) continue;

            var action = spellsButtons[kvp.Value];
            if (action.GetReady()) await action.Cast();
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
        var cell = cells[(int)(GD.Randi() % cells.Count)];
        cell.Module = module;
        cell.Button = 0;
        spellsButtons[buttonsBinds[cell.Button]] = module;
        //EventBus.EmitSignal("set_spell_icon_to_game", module, cell.button);
    }


    public class Cell
    {
        public int Button { get; set; }
        public Vector2 Position { get; set; }
        private int cellIndex;
        private Spell _module;

        public Spell Module
        {
            get => _module;
            set => _module = value;
        }
        public Node Link { get; set; }

        public Cell(Vector2 pos)
        {
            Module = null;
            Position = pos;
        }
    }
}