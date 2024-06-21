using Godot;
using projectpinky.scripts.drops;

namespace projectpinky.scripts.ui.inventory;

public partial class InventorySlotObject : CharacterBody2D
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
        GetNode<TextureRect>("icon").Texture = newData.Icon;
        GetNode<TextureRect>("background").Texture = newData.Background;
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("LMB") && mouseInArea)
        {
            ZIndex = 10;
            SetProcess(true);
        }
        if (Input.IsActionJustReleased("LMB") && mouseInArea)
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
            GlobalPosition = currentCell.GlobalPosition;
            return;
        }
        targetCell.SwapObjects(currentCell, this);
        SetCell(targetCell);
        targetCell = null;
    }

    public void SetCell(InventoryCell cell)
    {
        currentCell = cell;
        GlobalPosition = currentCell.GlobalPosition;
    }

    public InventoryCell GetCell()
    {
        return currentCell;
    }

    public void SetTargetCell(InventoryCell cell)
    {
        targetCell = cell;
    }

    private void OnMouseEntered()
    {
        mouseInArea = true;
    }

    private void OnMouseExited()
    {
        mouseInArea = false;
    }
}