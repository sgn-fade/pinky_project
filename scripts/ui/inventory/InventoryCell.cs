using Godot;

namespace projectpinky.scripts.ui.inventory;

public partial class InventoryCell : Control
{
	public bool Empty { get; set; } = true;
	public InventorySlotObject Object;
	public string SlotType { get; set; }= "inventory";

	public void SwapObjects(InventoryCell prevCell, InventorySlotObject newObject)
	{
		prevCell.RemoveChild(newObject);
		AddChild(newObject);
		newObject.GlobalPosition = GlobalPosition;
		if (Object != null)
		{
			Object.SetCell(prevCell);
			RemoveChild(Object);
			prevCell.AddChild(Object);
			prevCell.SetObject(Object);

			SetObject(newObject);
			return;
		}
		prevCell.Clear();
		SetObject(newObject);
	}

	public void SetObject(InventorySlotObject newObject)
	{
		Object = newObject;
		Empty = false;
	}

	public void Clear()
	{
		Empty = true;
		Object = null;
	}

	private void OnCellAreaEntered(Area2D area)
	{
		if (area.GetParent() is InventorySlotObject slotObject)
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


