using Godot;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.player;

public partial class Player : CharacterBody2D
{
    private float acceleration = 20;
    private float dashCooldown;

    // Stats
    private float speed = 20;
    private float maxSpeed = 80;
    private float magicDamage = 1;

    private AnimatedSprite2D animatedSprite;

    private Vector2 input = Vector2.Zero;

    private Timer timer = new();

    private float dashSpeedConst = 1;
    //private Vector2? movePosition = null;
    private int direction = 1;
    private States currentState = States.Idle;
    private EventBus eventBus;
    public enum States
    {
        None,
        Idle,
        Move,
        Dash,
        ButtHitDash,
        Spell,
        Dead,
        Inventory,
        Attack
    }

    public override void _Process(double delta)
    {
        switch (currentState)
        {
            case States.Idle:
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
        eventBus = GetNode<EventBus>("/root/EventBus");
        animatedSprite = GetNode<AnimatedSprite2D>("aSprite");
        currentState = States.Idle;
        eventBus.EmitSignal("hands_play_animation", "idle");
        eventBus.Connect("player_cast_spell", new Callable(this, nameof(SetCastState)));
        eventBus.Connect("player_teleport", new Callable(this, nameof(Teleport)));
        eventBus.Connect("player_take_damage", new Callable(this, nameof(SetCastState)));
        //eventBus.Connect("player_cast_spell", new Callable(this, nameof(_OnPlayerTakeDamage)));
        timer.OneShot = false;
        AddChild(timer);
    }

    // public override void _Input(InputEvent @event)
    // {
    //     if (Input.IsActionJustPressed("F"))
    //     {
    //         GlobalWorldInfo.FocusCamera();
    //     }
    //     if (Player.GetWeapon() != null)
    //     {
    //         Player.GetWeapon().Input(@event);
    //     }
    // }

    private void Move()
    {
        //var dash = GetNode("/root/World/Ui/game_ui/dash_indicator");
        if (Input.IsActionJustPressed("Shift")/* && dash.Ready*/)
        {
            DisableCollision();
            GetNode<CpuParticles2D>("dash_particles1").Emitting = true;
            GetNode<CpuParticles2D>("dash_particles2").Emitting = true;
            eventBus.EmitSignal("dash_cooldown");
            Velocity *= 5f;
            currentState = States.Dash;
            GetTree().CreateTimer(0.09f).Timeout += () => {
                currentState = States.Idle;
                Velocity /= 5f;
                CharacterSlowdown();
            };
        }
        input = Vector2.Zero;
        if (Input.IsActionPressed("ui_right"))
        {
            input.X += 1;
        }
        if (Input.IsActionPressed("ui_left"))
        {
            input.X += -1;
        }
        if (Input.IsActionPressed("ui_up"))
        {
            input.Y += -1;
        }
        if (Input.IsActionPressed("ui_down"))
        {
            input.Y += 1;
        }

        string animation;
        if ((int)input.X == -direction)
        {
            animation = "move_back";
            maxSpeed = 60;
        }
        else
        {
            animation = "move";
            maxSpeed = 80;
        }
        if (currentState == States.Attack)
        {
            maxSpeed /= 2;
            animation = "move_back";
        }
        if (input.Length() == 0)
        {
            speed = 30;
            animation = "idle";
        }
        else if (speed < maxSpeed)
        {
            speed += 5;
        }
        if (speed > maxSpeed)
        {
            speed = maxSpeed;
        }
        input = input.Normalized();
        animatedSprite.Play(animation);
        Velocity = Velocity.Lerp(input * speed, acceleration * 0.016f);
        MoveAndSlide();
    }

    public void SetSpeed(float newSpeed)
    {
        maxSpeed = newSpeed;
    }

    private void Dash()
    {
        MoveAndSlide();
    }



    private void Die()
    {
        currentState = States.Dead;
        eventBus.EmitSignal("player_dead");
    }

    public void SetIdleState()
    {
        Visible = true;
        currentState = States.Idle;
    }


    private async void CharacterSlowdown()
    {
        speed = 0;
        while (speed < 20)
        {
            speed += 5;
            timer.Start(GetProcessDeltaTime());
            await ToSignal(timer, "timeout");
        }
    }

    private void DisableCollision()
    {
        SetCollisionMaskValue(2, false);
        GetNode<Area2D>("player_area").SetCollisionMaskValue(2, false);
    }

    private void EnableCollision()
    {
        GetNode<Area2D>("player_area").SetCollisionMaskValue(2, true);
        SetCollisionMaskValue(2, true);
    }

    // private void _OnPlayerTakeDamage(Vector2 playerOffsetDir, float enemyDamage)
    // {
    //     Player.UpdateHp(-enemyDamage);
    //     var hp = Player.GetHp();
    //     var maxHp = Player.GetMaxHp();
    //     DisableCollision();
    //     Velocity = Velocity.Lerp(playerOffsetDir * 1000, 0.40f);
    //     SetVelocity(Velocity);
    //     MoveAndSlide();
    //     if (Player.GetHp() <= 0)
    //     {
    //         Die();
    //     }
    //     EnableCollision();
    // }

    private async void SetCastState(float animationTime, string animationName)
    {
        currentState = States.Spell;
        animatedSprite.Play(animationName);
        timer.Start(animationTime);
        await ToSignal(timer, "timeout");
        currentState = States.Idle;
    }

    private async void Teleport(Vector2 pos)
    {
        currentState = States.Spell;
        GetNode<RayCast2D>("teleport_ray").TargetPosition = pos;
        var globalMousePos = GetGlobalMousePosition();
        animatedSprite.Play("teleport_start");
        await ToSignal(animatedSprite, "animation_finished");

        DisableCollision();
        if (GetNode<RayCast2D>("teleport_ray").IsColliding())
        {
            GlobalPosition = GetNode<RayCast2D>("teleport_ray").GetCollisionPoint();
            GlobalPosition += new Vector2(-8 * direction, -4 - 4 * pos.Sign().Y);
        }
        else
        {
            GlobalPosition = globalMousePos;
        }
        EnableCollision();
        animatedSprite.Play("teleport_end");
        await ToSignal(animatedSprite, "animation_finished");
        currentState = States.Idle;
    }

    private void SetInventoryState()
    {
        animatedSprite.Play("idle");
        currentState = States.Inventory;
    }

    // private void ThrowNotEnoughManaMessage()
    // {
    //     var message = (Node2D)GD.Load<PackedScene>("res://scenes/ui/Player_massages/not_enough_mana_label.tscn").Instance();
    //     message.GlobalPosition = GlobalPosition;
    //     GlobalWorldInfo.GetWorld().AddChild(message);
    // }
}