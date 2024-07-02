using Godot;
using System;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.consumabels;
public partial class ManaCrystal : Node
{
	[Export] public int ManaRestored { get; set; } = 50;
	[Export] public String _Name { get; set; } = "ManaCrystal";
	[Export] public String Description { get; set; } = "Restores a portion of your mana.";
	public ManaCrystal()
	{
	}
	public override void _Ready()
	{
		//Global.Player.AddMana(ManaRestored);
	}
}
