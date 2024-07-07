using System;
using System.Collections.Generic;
using Godot;
using projectpinky.scripts.Globals;
using projectpinky.scripts.player;
using projectpinky.scripts.spells;

namespace projectpinky.scripts.drops;

public partial class ItemDrop : Area2D
{
	[Export] private SpellData[] dropList = new SpellData[1];
	[Export] private AnimationPlayer animator;

	private void AddItem()
	{
		animator.Play("delete");
		Global.Player.AddItem(dropList[GD.Randi() % dropList.Length]);
		QueueFree();
	}
}
