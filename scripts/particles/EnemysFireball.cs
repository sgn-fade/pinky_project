using Godot;
using System;
using projectpinky.scripts.Globals;
using projectpinky.scripts.player;

namespace projectpinky.scripts.particles;
public partial class EnemysFireball : CharacterBody2D
{
	[Export] public int Speed = 200;
	[Export] public int MaxDistance = 1000;
	private Vector2 _direction = Vector2.Zero;
	private Vector2  _enemyPos;
	private Vector2 _characterPos;
	private Timer _timer;
	private AnimatedSprite2D _sprite;
	private CpuParticles2D _particles;
	private Area2D _area;
	private Vector2 _velocity = Vector2.Zero;

	public override void _Ready()
	{
		_sprite = GetNode<AnimatedSprite2D>("Sprite2D");
		_particles = GetNode<CpuParticles2D>("Particles");
		_area = GetNode<Area2D>("Area2D");
		_timer = new Timer();
		_timer.OneShot = false;
		AddChild(_timer);
		_timer.Connect("timeout", new Callable(this, nameof(OnTimeout)));
		_area.Connect("body_entered",  new Callable(this, nameof(OnBodyEntered)));

		_sprite.Play("shoot");
		_timer.Start(0.1f);

		// todo GOVNOKOD???? perepisat na abstract???
		_characterPos = Global.Player.GetPosition();
		
		//zachem?
		_enemyPos = GlobalPosition;
	}

	public override void _Process(double delta)
	{
		// todo GOVNOKOD????
		_velocity = (_enemyPos - _characterPos).Normalized() * Speed;
		if ((GlobalPosition - _characterPos).Length() < MaxDistance)
		{
			Position += _velocity * (float)delta;
		}
		else
		{
			QueueFree();
		}
	}

	private void OnTimeout()
	{
		_particles.Emitting = true;
	}

	private void OnBodyEntered(Node body)
	{
		// todo GOVNOKOD????
		if (body.Name == "player")
		{
			Vector2 playerOffsetDir = -(GlobalPosition - Global.Player.GlobalPosition).Normalized();
			Global.EventBus.EmitSignal("player_take_damage", playerOffsetDir, 3);
		}

		if (body.Name != "enemy")
		{
			QueueFree();
		}
	}
}
