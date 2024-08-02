using System;
using System.Collections.Generic;
using Godot;
using projectpinky.scripts.Globals;
using projectpinky.scripts.player;
using projectpinky.scripts.spells;

namespace projectpinky.scripts.drops;

public partial class ItemDrop : CharacterBody2D
{
	[Export] private Spell[] dropList = new Spell[1];
	[Export] private AnimationPlayer animator;


	private void AddItem()
	{
		Global.PlayerLoader.AddItem(dropList[GD.Randi() % dropList.Length]);
		animator.Play("delete");
	}
}
