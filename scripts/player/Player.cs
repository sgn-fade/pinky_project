using Godot;
using projectpinky.scripts.Globals;
using projectpinky.scripts.hands;

namespace projectpinky.scripts.player;

public partial class Player : CharacterBody2D
{
    [Export] private HandsManager hands;
    [Export] private AnimatedSprite2D animatedSprite;
    // Stats
    [Export] private float speed = 80;
    [Export] private double acceleration = 20;

    private Vector2 input = Vector2.Zero;

    private float dashSpeedConst = 5;

    private bool dashReady = true;

    private States currentState = States.Active;
    private PlayerData player = Global.Player;

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
        Scale =  new Vector2(GetLocalMousePosition().X >= 0 ? 1 : -1, Scale.Y);
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

        var animation = input.Length() == 0 ? "idle" : "move";

        input = input.Normalized();
        animatedSprite.Play(animation);
        Velocity = Velocity.Lerp(input * speed, (float)(acceleration * GetProcessDeltaTime()));
        MoveAndSlide();
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

    private void SetCastState(float animationTime, string animationName)
    {
        currentState = States.Spell;
        animatedSprite.Play(animationName);
        GetTree().CreateTimer(animationTime).Timeout += () => { currentState = States.Active; };
    }

    private void Teleport(Vector2 pos)
    {
        Velocity = pos;
        MoveAndSlide();
    }
}