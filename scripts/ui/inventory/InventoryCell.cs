using Godot;
using projectpinky.scripts.drops;

namespace projectpinky.scripts.ui.inventory;

public partial class InventoryCell : Control
{
    public bool Empty { get; set; } = true;
    public InventorySlotObject Object;
    public InventoryItem.DataTypes SlotType { get; set; }

    public override void _Ready()
    {
        SlotType = InventoryItem.DataTypes.None;
    }

    public void SwapObjects(InventoryCell prevCell, InventorySlotObject newObject)
    {
        prevCell.SetObject(Object);
        SetObject(newObject);
    }

    public virtual void SetObject(InventorySlotObject newObject)
    {
        Clear();

        if (newObject == null) return;

        AddChild(newObject);
        newObject.GlobalPosition = GlobalPosition;
        newObject.CurrentCell = this;
        Object = newObject;
        Empty = false;
    }

    public virtual void Clear()
    {
        if (Object != null)
        {
            RemoveChild(Object);
            Object = null;
        }
        Empty = true;
    }

    public void OnCellAreaEntered(Area2D area)
    {
        if (area.GetParent() is not InventorySlotObject slotObject) return;

        if (slotObject.Data.Type == SlotType ||
            SlotType == InventoryItem.DataTypes.None)
        {
            slotObject.SetTargetCell(this);
        }
    }

    private void OnCellAreaExited(Area2D area)
    {
        if (area.GetParent() is InventorySlotObject slotObject)
        {
            slotObject.SetTargetCell(null);
        }
    }
}