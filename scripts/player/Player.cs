using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using projectpinky.scripts.Globals;
using projectpinky.scripts.hands;
using projectpinky.scripts.utillComponents;

namespace projectpinky.scripts.player;

public partial class Player : CharacterBody2D
{	
	[Export] private HandsManager _hands;
	[Export] private AnimatedSprite2D _animatedSprite;
	[Export] private float _speed = 80;
	[Export] private double _acceleration = 20;
	[Export] private float _dashSpeedConst = 5;

	private Vector2 _input = Vector2.Zero;
	private bool _dashReady = true;
	private States _currentState = States.Active;
	private PlayerData _player = Global.Player;

	[Export] private Hurtbox playerHp;
	public delegate void PlayerHpChanged(int hp, int maxHp);
	public static event PlayerHpChanged playerHpChanged;
	public delegate void ShowAllInteractableObjects(bool status);
	public static event ShowAllInteractableObjects showAllInteractableObjects;
	public delegate void PlayerDashEventHandler();
	public static event PlayerDashEventHandler playerDashEventHandler;
	public HandsManager GetHands() => _hands;

	private List<InteractableComponent> _nearbyObjects = new List<InteractableComponent>();
	
	public enum States
	{
		Active,
		Inactive,
		Attack
	}

	public override void _Process(double delta)
	{
		Move();
	}

	private void OnTakeDamage(int damage)
	{
		playerHpChanged?.Invoke(playerHp.Hp, playerHp.MaxHp);
	}

	private void Rotating()
	{
		Scale =  new Vector2(GetLocalMousePosition().X >= 0 ? 1 : -1, Scale.Y);
	}

	public States GetState()
	{
		return _currentState;
	}

	public void SetState(States state)
	{
		_currentState = state;
	}

	public override void _Ready()
	{
		_currentState = States.Active;
	}

	private void Move()
	{
		if (Input.IsActionJustPressed("Shift") && _dashReady)
		{
			Dash();
		}
		else if (Input.IsActionJustPressed("E"))
		{
			FindClosestObject()?.Interact();
		}

		if (Input.IsActionJustPressed("Tilda"))
		{
			showAllInteractableObjects?.Invoke(true);
		}
		if (Input.IsActionJustReleased("Tilda"))
		{
			showAllInteractableObjects?.Invoke(false);
		}
		
		Rotating();
		_input.X = Input.GetAxis("ui_left", "ui_right");
		_input.Y = Input.GetAxis("ui_up", "ui_down");

		var animation = _input.Length() == 0 ? "idle" : "move";
		if (_nearbyObjects.Count > 0 && animation == "move")
		{
			FindClosestObject();
		}
		else if (_nearbyObjects.Count > 1 && animation == "move")
		{
			FindClosestObject();
		}
		_input = _input.Normalized();
		_animatedSprite.Play(animation);
		Velocity = Velocity.Lerp(_input * _speed, (float)(_acceleration * GetProcessDeltaTime()));
		MoveAndSlide();
	}


	private void Dash()
	{
		playerDashEventHandler?.Invoke();
		_dashReady = false;
		GetTree().CreateTimer(Global.Player.DashCooldown).Timeout += () => { _dashReady = true; };

		Velocity *= _dashSpeedConst;
		GetTree().CreateTimer(0.09f).Timeout += () =>
		{
			Velocity /= _dashSpeedConst;
		};
	}

	private void Die()
	{
		_currentState = States.Inactive;
		EmitSignal(nameof(EventBus.PlayerDeadEventHandler));
	}

	public void Activate()
	{
		Visible = true;
		_currentState = States.Active;
	}

	private void SwitchCollision(bool state)
	{
		GetNode<Area2D>("player_area").SetCollisionMaskValue(2, state);
		SetCollisionMaskValue(2, state);
	}

	private void SetCastState(float animationTime, string animationName)
	{
		_currentState = States.Attack;
		_animatedSprite.Play(animationName);
		GetTree().CreateTimer(animationTime).Timeout += () => { _currentState = States.Active; };
	}

	private void Teleport(Vector2 pos)
	{
		GlobalPosition = pos;
	}

	public InteractableComponent FindClosestObject()
	{
		InteractableComponent closestObject = null;
		var closestDistance = float.MaxValue;
		foreach (var obj in _nearbyObjects)
		{
				var distance = (obj.GlobalPosition - GlobalPosition).Length();
				if (distance < closestDistance)
				{
					closestDistance = distance;
					closestObject = obj;
				}
		}

		return closestObject;
	}
	
	public void AddNewClosestObjects(InteractableComponent obj) => _nearbyObjects.Add(obj);
	
	public void DeleteFromClosestObjects(InteractableComponent obj) => _nearbyObjects.Remove(obj);
	
}
