using Godot;
using System;
using projectpinky.scripts.Globals;
namespace projectpinky.scripts.particles;
public partial class ContinuumShotgunBullet : Node2D	
{
	[Export] public float Speed = 300;
	[Export] public int MaxDistance = 10000;

	private Vector2 _mousePos;
	private Vector2 _characterPos;
	private Timer _timer = new Timer();

	private Node2D _barrel;
	private Area2D _area2D;
	private AnimatedSprite2D _b;
	private CpuParticles2D _particles;
	private Light2D _pointLight2D;
	private CpuParticles2D _endParticles;

	public override void _Ready()
	{
		_barrel = GetNode<Node2D>("/root/World/player/hands/heands_continuum_shotgun/pos");
		_area2D = GetNode<Area2D>("Area2D");
		_b = GetNode<AnimatedSprite2D>("$b");
		_particles = GetNode<CpuParticles2D>("$particles");
		_pointLight2D = GetNode<Light2D>("$PointLight2D");
		_endParticles = GetNode<CpuParticles2D>("$end_particles");

		_timer.OneShot = false;
		AddChild(_timer);

		GlobalPosition = _barrel.GlobalPosition;
		_mousePos = _barrel.GlobalPosition;

		_area2D.Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));

		_characterPos = Global.Player.GlobalPosition;
		_characterPos.Y += 1.5f;

		RandomizeMousePos();

		_b.Frame = (int)(GD.Randi() % 5);
		Speed += GD.Randi() % 30;

		LookAt(_characterPos);
		_particles.Emitting = true;
	}

	private void RandomizeMousePos()
	{
		var rand = new Random();
		_mousePos.X += rand.Next(0, 5);
		_mousePos.Y += rand.Next(0, 5);
		_mousePos.X -= 2.5f;
		_mousePos.Y -= 2.5f;
	}

	public async void Delete()
	{
		_b.QueueFree();
		_area2D.QueueFree();
		_pointLight2D.QueueFree();
		_particles.QueueFree();

		Speed = 0;
		_endParticles.Emitting = true;

		_timer.Start(0.2f);
		await ToSignal(_timer, "timeout");
		QueueFree();
	}
	
	private void OnBodyEntered(Node body)
	{
		//todo logica popadania
	}
}
