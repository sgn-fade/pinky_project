using Godot;
using System;
using projectpinky.scripts.drops;
using projectpinky.scripts.Globals;
using projectpinky.scripts.player;

namespace projectpinky.scripts.utillComponents;

public partial class InteractableComponent : Area2D
{
	[Signal]
	public delegate void InteractObjectEventHandler();
	[Signal]
	public delegate void EntityEnteredEventHandler();
	
	public override void _Ready()
	{
		Player.showAllInteractableObjects += ShowObjectName;
	}

	
	public void Interact()
	{
		EmitSignal(SignalName.InteractObject);
	}
	
	private void OnBodyEntered(Node2D body)
	{
		if (body is Player player)
		{
			player.AddNewClosestObjects(this);
		}
		else if (body is ItemDrop itemDrop)
		{
			GD.Print("Hello world");
		}

		EmitSignal(SignalName.EntityEntered);
	}
	private void OnBodyExited(Node2D body)
	{
		if (body is Player player)
		{
			player.DeleteFromClosestObjects(this);
		}
		EmitSignal(SignalName.EntityEntered);
	}

	private void ShowObjectName(bool status)
	{
		if (status) Modulate = new Color(1, 1, 0);
		else Modulate = new Color(1, 1, 1);
	}

	//todo 
	private void OnMouseEntered() => Modulate = new Color(1, 0.5f, 1);

	private void OnMouseExited() => Modulate = new Color(1, 1, 1);
}





