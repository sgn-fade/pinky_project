using Godot;
using System;
using projectpinky.scripts.Enemies;
using projectpinky.scripts.Globals;
namespace projectpinky.scripts.particles;

public  abstract partial class SpellController : CharacterBody2D
{
	[Export] protected float Duration;
	public override void _Ready()
	{
		GetTree().CreateTimer(Duration).Timeout += Delete;
	}
	protected abstract void Delete();
}
