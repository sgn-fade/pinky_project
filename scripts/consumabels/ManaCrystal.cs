using Godot;
using System;
namespace projectpinky.scripts.consumabels;
public partial class ManaCrystal : ConsumablesData, IConsumabels
{
	[Export] public int ManaRestored { get; set; } = 50;
	
	public ManaCrystal()
	{
		Name = "Mana Crystal";
		Description = "Restores a portion of your mana.";
	}

	public void Use()
	{
		GD.Print($"Used {Name}, restored {ManaRestored} mana.");	
	}
}
