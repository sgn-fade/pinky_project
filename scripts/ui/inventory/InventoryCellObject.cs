using Godot;
using projectpinky.scripts.drops;

namespace projectpinky.scripts.ui.inventory;

public partial class InventoryCellObject : CharacterBody2D
{
    public object Data { get; set; }
    public string DataType { get; set; }
    private InventoryCell currentCell;
    private InventoryCell targetCell;
    private bool mouseInArea;

    public void SetData(InventoryItem newData)
    {
        Data = newData.Data;
        DataType = newData.DataType;
        GetNode<Sprite2D>("icon").Texture = newData.Icon;
        GetNode<Sprite2D>("background").Texture = newData.Background;
    }

    public object GetData()
    {
        return Data;
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("mouse_left_button") && mouseInArea)
        {
            ZIndex = 10;
            SetProcess(true);
        }
        if (Input.IsActionJustReleased("mouse_left_button") && mouseInArea)
        {
            ZIndex = 0;
            SetToCell();
            SetProcess(false);
        }
    }

    public override void _Ready()
    {
        SetProcess(false);
    }

    public override void _Process(double delta)
    {
        GlobalPosition = GetGlobalMousePosition();
    }

    public void SetToCell()
    {
        if (targetCell == null)
        {
            GlobalPosition = currentCell.GetPos();
            return;
        }
        targetCell.SwapObjects(currentCell, this);
        SetCell(targetCell);
        targetCell = null;
    }

    public void SetCell(InventoryCell cell)
    {
        currentCell = cell;
        GlobalPosition = currentCell.GetPos();
    }

    public InventoryCell GetCell()
    {
        return currentCell;
    }

    public void SetTargetCell(InventoryCell cell)
    {
        targetCell = cell;
    }

    public void _OnMouseEntered()
    {
        mouseInArea = true;
    }

    public void _OnMouseExited()
    {
        mouseInArea = false;
    }
}