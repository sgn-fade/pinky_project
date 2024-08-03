using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using projectpinky.scripts.drops;
using projectpinky.scripts.Globals;
using projectpinky.scripts.hands;
using projectpinky.scripts.weapons;
using projectpinky.scripts.utillComponents;

namespace projectpinky.scripts.player;

public partial class Player : CharacterBody2D
{
    [Export] private PlayerView _hands;
    [Export] private AnimatedSprite2D _animatedSprite;
    [Export] private float _speed = 80;
    [Export] private double _acceleration = 20;
    [Export] private float _dashSpeedConst = 5;
    [Export] public Hurtbox PlayerHurtBox;

    private Vector2 _input = Vector2.Zero;
    private bool _dashReady = true;
    public States CurrentState { get; set; } = States.Active;
    private PlayerLoader _player = Global.PlayerLoader;

    [Export] private Hurtbox playerHp;

    [Export] public PlayerView PlayerView { get; set; }
    public PlayerData PlayerData { get; set; }
    private List<InteractableComponent> _nearbyObjects = new();

    public delegate void ShowAllInteractableObjects(bool status);

    public static event ShowAllInteractableObjects showAllInteractableObjects;

    public delegate void PlayerHpChanged(int hp, int maxHp);

    public static event PlayerHpChanged playerHpChanged;

    public delegate void PlayerDashEventHandler(float cooldownTime);

    public static event PlayerDashEventHandler playerDashEventHandler;
    public PlayerView GetHands() => _hands;

    public enum States
    {
        Active,
        Inactive,
        Attack
    }

    public override void _Ready()
    {
        PlayerData = new PlayerData();
        CurrentState = States.Active;
    }

    public override void _Process(double delta) => Move();

    private void OnTakeDamage(int damage) => playerHpChanged?.Invoke(playerHp.Hp, playerHp.MaxHp);

    private void Rotating() => Scale = new Vector2(GetLocalMousePosition().X >= 0 ? 1 : -1, Scale.Y);


    private void Move()
    {
        if (Input.IsActionJustPressed("Shift") && _dashReady) Dash();

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

        _animatedSprite.Play(animation);
        Velocity = Velocity.Lerp(_input.Normalized() * _speed, (float)(_acceleration * GetProcessDeltaTime()));
        MoveAndSlide();

        if (Input.IsActionJustPressed("E"))
        {
            //todo dlya testov pomenyat
            //ItemDrop drop1 = (ItemDrop)drop.Instantiate();
            //drop1.GlobalPosition = GetGlobalMousePosition();
            //Global.World.AddChild(drop1);
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
    }

    private void Dash()
    {
        playerDashEventHandler?.Invoke(PlayerData.DashCooldown);
        _dashReady = false;
        GetTree().CreateTimer(PlayerData.DashCooldown).Timeout += () => { _dashReady = true; };

        Velocity *= _dashSpeedConst;
        GetTree().CreateTimer(0.09f).Timeout += () => { Velocity /= _dashSpeedConst; };
    }

    private void Die()
    {
        CurrentState = States.Inactive;
        EmitSignal(nameof(EventBus.PlayerDeadEventHandler));
    }

    public void Activate()
    {
        Visible = true;
        CurrentState = States.Active;
    }

    public void Parry()
    {
        PlayerHurtBox.Invincible = true;
        GetTree().CreateTimer(1).Timeout += () => { PlayerHurtBox.Invincible = false; };
    }

    private void SetCastState(float animationTime, string animationName)
    {
        CurrentState = States.Attack;
        _animatedSprite.Play(animationName);
        GetTree().CreateTimer(animationTime).Timeout += () => { CurrentState = States.Active; };
    }

    private void Teleport(Vector2 pos)
    {
        GlobalPosition = pos;
    }

    public void SetWeapon(Weapon weapon)
    {
        PlayerView.SwitchHandsStance(weapon.WeaponData.HandsScene);
        PlayerData.Weapon = weapon;
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
