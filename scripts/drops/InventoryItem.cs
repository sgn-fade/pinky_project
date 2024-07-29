using System;
using Godot;

namespace projectpinky.scripts.drops;
[GlobalClass]
public partial class InventoryItem : Resource
{
	public enum DataTypes
	{
		None,
		Weapon,
		Spell,
		Consumable,
		Resource,
	}
	[Export] public DataTypes Type { get; set; }
	[Export] public Texture2D Icon { get; set; }
	[Export] public Texture2D Background { get; set; }
	[Export] public bool IsStackable;
	[Export] public int Value;

	public InventoryItem()
	{
	}
}
