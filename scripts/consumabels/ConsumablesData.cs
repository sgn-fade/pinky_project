using Godot;
using System;
using projectpinky.scripts.drops;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.consumabels;
public partial class ConsumablesData : InventoryItem
{
	public ConsumablesData()
	{
		DataType = DataTypes.Consumable;
	}
	
	[Export] public string Name { get; set; }
	[Export] public string Description { get; set; }
	public void Use()
	{
		//pochemy tut ne vidit PlayerData hz
		//Global.PlayerData.SetMaxMana(200);
	}
}
