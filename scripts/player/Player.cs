using Godot;
using projectpinky.scripts.Globals;
using projectpinky.scripts.hands;
using projectpinky.scripts.weapons;

namespace projectpinky.scripts.player;

public partial class Player : CharacterBody2D
{
    [Export] private PlayerView _hands;
    [Export] private AnimatedSprite2D _animatedSprite;
    [Export] private float _speed = 80;
    [Export] private double _acceleration = 20;
    [Export] private float _dashSpeedConst = 5;

    private Vector2 _input = Vector2.Zero;
    private bool _dashReady = true;
    public States CurrentState { get; set; } = States.Active;
    private PlayerLoader _player = Global.PlayerLoader;

    [Export] private Hurtbox playerHp;

    [Export]public PlayerView PlayerView { get; set; }
    public PlayerData PlayerData { get; set; }

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
        PlayerView.Init();
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

        _animatedSprite.Play(animation);
        Velocity = Velocity.Lerp(_input.Normalized() * _speed, (float)(_acceleration * GetProcessDeltaTime()));
        MoveAndSlide();
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
        PlayerView.SwitchHandsStance(weapon.HandsScene);
        PlayerData.Weapon = weapon;
    }
}