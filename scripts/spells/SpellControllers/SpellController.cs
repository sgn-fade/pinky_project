using Godot;
using System;
using projectpinky.scripts.Enemies;
using projectpinky.scripts.Globals;
namespace projectpinky.scripts.particles;

public  abstract partial class SpellController : CharacterBody2D
{
	[Export] protected float Duration;
	[Export] protected int Damage;
	public override void _Ready()
	{
		GetTree().CreateTimer(Duration).Timeout += Delete;
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body is Enemy enemy)
		{
			enemy.TakeDamage(Damage);
		}
	}

	protected abstract void Delete();
}
