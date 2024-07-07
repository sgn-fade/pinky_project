using Godot;
using System;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.utillComponents;

public partial class InteractComponent : Area2D
{
	[Signal]
	public delegate void NewNearbyObjectEventHandler(InteractableComponent obj);

	[Signal]
	public delegate void DeleteNearbyObjectEventHandler(InteractableComponent obj);

	public void NewNearbyInteract(InteractableComponent obj)
	{
		EmitSignal(SignalName.NewNearbyObject, obj);
	}
	public void DeleteNearbyInteract(InteractableComponent obj)
	{
		EmitSignal(SignalName.DeleteNearbyObject, obj);
	}
}
