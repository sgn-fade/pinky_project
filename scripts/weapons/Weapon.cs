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
    private double critChance = 10d;

    public double GetCritChance() => critChance;
    
    public InventoryItem InvItem { get; set; }

    private readonly Dictionary<string, string> buttonsBinds = Options.ButtonsBinds;
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

    private List<Vector2> positions;
    private List<Cell> cells = new();

    public List<Cell> GetCells()
    {
        return cells;
    }

    public override void _Ready()
    {
        spellsButtons = new Dictionary<string, Spell>
        {
            {buttonsBinds["slot1"], null},
            {buttonsBinds["slot2"], null},
            {buttonsBinds["slot3"], null},
            {buttonsBinds["slot4"], null},
            {buttonsBinds["slot5"], null},
            {buttonsBinds["slot6"], null}
        };
        positions = new List<Vector2>(modulePositionList);
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
        cells[cellIndex].module = module;
        foreach (var key in buttonsBinds.Keys)
        {
            if (spellsButtons[buttonsBinds[key]] == null)
            {
                cells[cellIndex].SetButton(key);
                spellsButtons[buttonsBinds[cells[cellIndex].GetButton()]] = module;
                //EventBus.EmitSignal("set_spell_icon_to_game", module, cells[cellIndex].button);
                return;
            }
        }
    }

    public void RemoveModuleFromWeapon(int cellIndex)
    {
        //EventBus.EmitSignal("remove_spell_icon_from_game", cells[cellIndex].button);
        spellsButtons[buttonsBinds[cells[cellIndex].GetButton()]] = null;
        cells[cellIndex].SetButton(null);
        cells[cellIndex].module = null;
    }

    public void SwapModules(string firstSlot, string secondSlot)
    {
        (spellsButtons[buttonsBinds[firstSlot]], spellsButtons[buttonsBinds[secondSlot]]) = (spellsButtons[buttonsBinds[secondSlot]], spellsButtons[buttonsBinds[firstSlot]]);

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

    public void AddBaseSpell(Spell module)
    {
        var cell = cells[(int)(GD.Randi() % cells.Count)];
        cell.module = module;
        cell.SetButton("slot3");
        spellsButtons[buttonsBinds[cell.GetButton()]] = module;
        //EventBus.EmitSignal("set_spell_icon_to_game", module, cell.button);
    }

    public Spell GetSpellFromButton(string button)
    {
        return spellsButtons[button];
    }

    public class Cell
    {
        private string button;
        private Vector2 position;
        private int cellIndex;
        public Spell module
        {
            get => _module;
            set => _module = value;
        }
        private Spell _module;
        public Node link;

        public Cell(Vector2 pos)
        {
            module = null;
            position = pos;
        }

        public string GetButton() => button;
        public Vector2 GetPosition() => position;
        public Spell GetModule() => module;

        public void SetButton(string newButton)
        {
            button = newButton;
        }
        public void SetModule(Spell newModule)
        {
            module = newModule;
        }
    }
}