using Godot;
using System;
using projectpinky.scripts.Globals;
using projectpinky.scripts.player;

namespace projectpinky.scripts.utillComponents;

public partial class InteractableComponent : Area2D
{
	[Signal]
	public delegate void InteractObjectEventHandler();
	public void Interact()
	{
		EmitSignal(SignalName.InteractObject);
	}
	
	private void OnBodyEntered(Node2D body)
	{
		if (body is InteractComponent component)
		{
			component.NewNearbyInteract(this);
		}
	}
	private void OnBodyExited(Node2D body)
	{
		if (body is InteractComponent component)
		{
			component.DeleteNearbyInteract(this);
		}
	}
}
