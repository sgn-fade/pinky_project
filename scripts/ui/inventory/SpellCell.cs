using System;
using Godot;
using projectpinky.scripts.drops;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.ui.inventory;

public partial class SpellCell : InventoryCell
{
    private int _cellIndex;

    public override void _Ready()
    {
        SlotType = InventoryItem.DataTypes.Spell;
    }

    // public void OnCellAreaEntered(Area2D area)
    // {
    //     if (area.Name == "object" && area.GetParent<InventorySlotObject>().DataType == "spell")
    //     {
    //         area.GetParent<InventorySlotObject>().SetTargetCell(this);
    //     }
    // }

    // public new void SetObject(InventorySlotObject newObject)
    // {
    //     if (newObject.DataType == "spell")
    //     {
    //         Object = newObject;
    //         Empty = false;
    //         Global.Player.GetWeapon().AddSpell((Spell)newObject.Data, cellIndex);
    //     }
    // }

    public void RestoreObject(InventorySlotObject oldObject)
    {
        Object = oldObject;
        Empty = false;
    }

    public new void Clear()
    {
        base.Clear();
        Global.Player.GetWeapon().RemoveSpell(_cellIndex);
    }
}