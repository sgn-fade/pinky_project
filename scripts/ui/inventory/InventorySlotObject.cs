using Godot;
using projectpinky.scripts.drops;
using projectpinky.scripts.spells;

namespace projectpinky.scripts.ui.inventory;

public partial class InventorySlotObject : CharacterBody2D
{
    public InventoryItem Data { get; set; }
    public InventoryCell CurrentCell { get; set; }
    private InventoryCell targetCell;
    private bool mouseInArea;

    public void SetData(InventoryItem newData)
    {
        Data = newData;
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

        if (Input.IsActionJustReleased("LMB") && mouseInArea && IsProcessing())
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
            GlobalPosition = CurrentCell.GlobalPosition;
            return;
        }

        targetCell.SwapObjects(CurrentCell, this);
        CurrentCell = targetCell;
        targetCell = null;
    }


    public void SetTargetCell(InventoryCell cell)
    {
        if (
            cell == null ||
            cell.SlotType == InventoryItem.DataTypes.None ||
            cell.SlotType == Data.Type
        )
        {
            targetCell = cell;
        }
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