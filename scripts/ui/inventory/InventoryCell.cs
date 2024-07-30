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
        var prevObject = Object;
        prevCell.Clear();
        Clear();
        prevCell.SetObject(prevObject);
        SetObject(newObject);
    }

    public virtual void SetObject(InventorySlotObject newObject)
    {
        if (newObject == null) return;

        AddChild(newObject);
        newObject.GlobalPosition = GlobalPosition;
        newObject.CurrentCell = this;
        Object = newObject;
        Empty = false;
    }

    public virtual void Clear()
    {
        HideObject();
        Object = null;
    }

    public override void _Process(double delta)
    {
        if (Object != null)
        {
            GetNode<Label>("Label").Text = Object.GetInstanceId().ToString();

        }
        else
        {
            GetNode<Label>("Label").Text = "Empty";

        }
    }

    public void OnCellAreaEntered(Area2D area)
    {
        if (!Visible) return;

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

    public void HideObject()
    {
        if (Object != null)
        {
            RemoveChild(Object);
        }
        Empty = true;
    }

    public void SwitchMonitoring(bool state)
    {
        GetNode<Area2D>("area").Monitoring = state;
    }
}