using System;
using Godot;
using projectpinky.scripts.drops;
using projectpinky.scripts.Globals;
using projectpinky.scripts.spells;

namespace projectpinky.scripts.ui.inventory;

public partial class SpellCell : InventoryCell
{
    private int _cellIndex;

    public delegate void SpellChanged(Spell spell, int cellIndex);

    public static event SpellChanged spellChanged;

    public override void _Ready()
    {
        SlotType = InventoryItem.DataTypes.Spell;
    }

    public override void SetObject(InventorySlotObject newObject)
    {
        base.SetObject(newObject);
        spellChanged?.Invoke(Object.Data as Spell, _cellIndex);
    }
    public void RestoreObject(InventorySlotObject oldObject)
    {
        base.SetObject(oldObject);        
    }

    public override void Clear()
    {
        base.Clear();
        spellChanged?.Invoke(null, _cellIndex);
    }
}