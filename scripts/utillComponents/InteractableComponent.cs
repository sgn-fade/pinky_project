using Godot;
using System;
using projectpinky.scripts.Globals;
using projectpinky.scripts.player;

namespace projectpinky.scripts.utillComponents;

public partial class InteractableComponent : Area2D
{
	[Signal]
	public delegate void InteractObjectEventHandler();
	[Signal]
	public delegate void EntityEnteredEventHandler();
	
	public void Interact()
	{
		EmitSignal(SignalName.InteractObject);
	}
	
	private void OnBodyEntered(Node2D body)
	{
		if (body is Player player)
		{
			GD.Print("Player in");
			player.AddNewClosestObjects(this);
		}
		EmitSignal(SignalName.EntityEntered);
	}
	private void OnBodyExited(Node2D body)
	{
		if (body is Player player)
		{
			GD.Print("Player out");
			player.DeleteFromClosestObjects(this);
		}
		EmitSignal(SignalName.EntityEntered);
	}
}
