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

	[Export] private CharacterBody2D Body;
	
	public override void _Ready() {}

	public override void _EnterTree()
	{
		Player.showAllInteractableObjects += ShowObjectName;
	}

	public override void _ExitTree()
	{
		Player.showAllInteractableObjects -= ShowObjectName;
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
		else if (body is InteractableComponent itemDrop)
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
	
	private void OnInteractEntered(Area2D area)
	{
		if (area is InteractableComponent interactableComponent)
		{
			Body?.MoveAndSlide();
		}
	}

	//todo 
	private void OnMouseEntered() => Modulate = new Color(1, 0.5f, 1);

	private void OnMouseExited() => Modulate = new Color(1, 1, 1);
}


