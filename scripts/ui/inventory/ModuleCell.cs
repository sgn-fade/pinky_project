using System;
using Godot;
using projectpinky.scripts.Globals;
using projectpinky.scripts.player;
using projectpinky.scripts.spells;

namespace projectpinky.scripts.ui.inventory;

public partial class ModuleCell : InventoryCell
{
    private int cellIndex;

    public override void _Ready()
    {
        SlotType = "spell";
    }

    public new void _OnCellAreaEntered(Area2D area)
    {
        if (area.Name == "object" && area.GetParent<InventoryCellObject>().DataType == "spell")
        {
            area.GetParent<InventoryCellObject>().SetTargetCell(this);
        }
    }

    public new void SetObject(InventoryCellObject newObject)
    {
        if (newObject.DataType == "spell")
        {
            Object = newObject;
            Empty = false;
            Global.Player.GetWeapon().AddModuleToWeapon((Spell)newObject.Data, cellIndex);
        }
    }

    public void RestoreObject(InventoryCellObject oldObject)
    {
        Object = oldObject;
        Empty = false;
    }

    public new void Clear()
    {
        base.Clear();
        Global.Player.GetWeapon().RemoveModuleFromWeapon(cellIndex);
    }
}