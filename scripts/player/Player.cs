using Godot;
using projectpinky.scripts.Globals;
using projectpinky.scripts.hands;

namespace projectpinky.scripts.player;

public partial class Player : CharacterBody2D
{
    private double acceleration = 20;
    private float dashCooldown;

    // Stats
    [Export] private float speed = 20;
    [Export] private float maxSpeed = 80;
    [Export] private float magicDamage = 1;

    private Vector2 input = Vector2.Zero;

    private Timer timer = new();

    private float dashSpeedConst = 5;

    private bool dashReady = true;

    private int direction = 1;
    private States currentState = States.Active;
    private EventBus eventBus = Global.EventBus;
    private PlayerData player = Global.Player;
    [Export] private HandsManager hands;
    [Export] private AnimatedSprite2D animatedSprite;

    public HandsManager GetHands() => hands;

    public enum States
    {
        Active,
        Move,
        Dash,
        Spell,
        Inactive,
        Attack
    }

    public override void _Process(double delta)
    {
        switch (currentState)
        {
            case States.Active:
                Move();
                Rotating();
                break;
            case States.Dash:
                Dash();
                break;
            case States.Attack:
                Move();
                break;
            default:
                return;
        }
    }

    private void Rotating()
    {
        if (GetLocalMousePosition().X >= 0)
        {
            Scale = new Vector2(1, Scale.Y);
        }
        else
        {
            direction *= -1;
            Scale = new Vector2(-1, Scale.Y);
        }
    }

    public States GetState()
    {
        return currentState;
    }

    public void SetState(States state)
    {
        currentState = state;
    }

    public override void _Ready()
    {
        currentState = States.Active;
        //todo event bus
        //eventBus.Connect("player_cast_spell", new Callable(this, nameof(SetCastState)));
        //eventBus.Connect("player_teleport", new Callable(this, nameof(Teleport)));
        //eventBus.Connect("player_take_damage", new Callable(this, nameof(SetCastState)));
        //eventBus.Connect("player_cast_spell", new Callable(this, nameof(_OnPlayerTakeDamage)));
        timer.OneShot = false;
        AddChild(timer);
    }

    public override void _Input(InputEvent @event)
    {
        //todo focus camera
        // if (Input.IsActionJustPressed("F"))
        // {
        //     GlobalWorldInfo.FocusCamera();
        // }
        //todo weapon input
        // if (Player.GetWeapon() != null)
        // {
        //     Player.GetWeapon().Input(@event);
        // }
    }

    private void Move()
    {
        if (Input.IsActionJustPressed("Shift") && dashReady)
        {
            dashReady = false;
            GetTree().CreateTimer(Global.Player.DashCooldown).Timeout += () => { dashReady = true; };

            Velocity *= dashSpeedConst;
            currentState = States.Dash;
            GetTree().CreateTimer(0.09f).Timeout += () =>
            {
                currentState = States.Active;
                Velocity /= dashSpeedConst;
            };
        }

        input.X = Input.GetAxis("ui_left", "ui_right");
        input.Y = Input.GetAxis("ui_up", "ui_down");

        var animation = "move";

        if (input.Length() == 0)
        {
            animation = "idle";
        }

        input = input.Normalized();
        animatedSprite.Play(animation);
        Velocity = Velocity.Lerp(input * speed, (float)(acceleration * GetProcessDeltaTime()));
        MoveAndSlide();
    }

    public void SetMaxSpeed(float newSpeed)
    {
        maxSpeed = newSpeed;
    }

    private void Dash()
    {
        Global.Player.OnPlayerDash();
        MoveAndSlide();
    }

    private void Die()
    {
        currentState = States.Inactive;
        EmitSignal(nameof(EventBus.PlayerDeadEventHandler));
    }

    public void SetIdleState()
    {
        Visible = true;
        currentState = States.Active;
    }

    private void SwitchCollision(bool state)
    {
        GetNode<Area2D>("player_area").SetCollisionMaskValue(2, state);
        SetCollisionMaskValue(2, state);
    }
    private void TakeDamage(Vector2 playerOffsetDir, int enemyDamage)
    {
        if (!player.SetHp(enemyDamage))
        {
            Die();
            return;
        }
        Velocity = Velocity.Lerp(playerOffsetDir * 1000, 0.40f);
        MoveAndSlide();
    }
    private async void SetCastState(float animationTime, string animationName)
    {
        currentState = States.Spell;
        animatedSprite.Play(animationName);
        //todo animation ended signal
        timer.Start(animationTime);
        await ToSignal(timer, "timeout");
        currentState = States.Active;
    }

    private void Teleport(Vector2 pos)
    {
        currentState = States.Spell;
        Velocity = pos;
        MoveAndSlide();
        currentState = States.Active;
    }
}