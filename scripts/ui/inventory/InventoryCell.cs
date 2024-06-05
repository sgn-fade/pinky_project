using Godot;

namespace projectpinky.scripts.ui.inventory;

public partial class InventoryCell : Node2D
{
    // Переменные
    public bool Empty { get; set; } = true;
    public InventoryCellObject @object;
    public string SlotType { get; set; }= "inventory";

    public bool IsEmpty() => Empty;


    public Vector2 GetPos()
    {
        return GetNode<Node2D>("object_pos").GlobalPosition;
    }

    public void _OnCellAreaEntered(Area2D area)
    {
        if (area.Name == "object")
        {
            area.GetParent<InventoryCellObject>().SetTargetCell(this);
        }
    }

    public void SwapObjects(InventoryCell prevCell, InventoryCellObject newObject)
    {
        if (@object != null)
        {
            @object.SetCell(prevCell);
            prevCell.SetObject(@object);
            SetObject(newObject);
            return;
        }
        prevCell.Clear();
        SetObject(newObject);
    }

    public void SetObject(InventoryCellObject newObject)
    {
        @object = newObject;
        Empty = false;
    }

    public void Clear()
    {
        Empty = true;
        @object = null;
    }

    public void _OnCellAreaExited(Area2D area)
    {
        if (area.Name == "object")
        {
            area.GetParent<InventoryCellObject>().SetTargetCell(null);
        }
    }
}