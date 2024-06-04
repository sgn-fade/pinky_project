using Godot;
using System;
using projectpinky.scripts.Globals;

public partial class BaseSpellParticle : CharacterBody2D
{
	private Timer _timer;
	private float _speed = 300f;
	private Vector2 _globalPosition;

	public override void _Ready()
	{
		_timer = new Timer();
		_timer.OneShot = false;
		AddChild(_timer);

		Vector2 endPosition = GetGlobalMousePosition();
		_globalPosition = Global.Player.GetPosition();
		_globalPosition.Y -= 5;
		Global.Player.SetPosition(_globalPosition);
		Velocity = (endPosition - Global.Player.GetPosition()).Normalized() * _speed;
		LookAt(endPosition);
	}

	public override void _Process(double delta)
	{
		Velocity = Velocity.Normalized() * _speed;
		MoveAndSlide();
		Velocity = Velocity;
	}

	public void OnBodyEntered(Node body)
	{
		Delete();
		Global.EventBus.EmitSignal("damage_to_enemy", body, 10);
	}

	private void Delete() => QueueFree();
}
