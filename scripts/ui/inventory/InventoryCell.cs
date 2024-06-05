using Godot;

namespace projectpinky.scripts.ui.inventory;

public partial class InventoryCell : Node2D
{
    // Переменные
    public bool Empty { get; set; } = true;
    public InventoryCellObject Object;
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
        if (Object != null)
        {
            Object.SetCell(prevCell);
            prevCell.SetObject(Object);
            SetObject(newObject);
            return;
        }
        prevCell.Clear();
        SetObject(newObject);
    }

    public void SetObject(InventoryCellObject newObject)
    {
        Object = newObject;
        Empty = false;
    }

    public void Clear()
    {
        Empty = true;
        Object = null;
    }

    public void _OnCellAreaExited(Area2D area)
    {
        if (area.Name == "object")
        {
            area.GetParent<InventoryCellObject>().SetTargetCell(null);
        }
    }
}