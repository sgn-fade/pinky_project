using Godot;
using System;
using projectpinky.scripts.drops;
using projectpinky.scripts.Globals;
using projectpinky.scripts.spells;

namespace projectpinky.scripts.consumabels;
[GlobalClass] [Tool]
public partial class ConsumableData : InventoryItem
{
	[Export] public PackedScene Consumable { get; set; }
	[Export] public string Name { get; set; }
	[Export] public string Description { get; set; }
	[Export] public float CooldownTime { get; set; }
	[Export] public Rarities Rarity { get; set; }
	public ConsumableData()
	{
		CooldownTime = 0;
		Background = GD.Load<Texture2D>($"res://sprites/ui/{Rarity}_module_button_state.png");
		Consumable = null;
	}
	
	public enum Rarities
	{
		Bronze,
		Silver,
		Gold,
	}
}
