using System;
using System.Threading.Tasks;
using Godot;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.Enemies.Goblins;

public partial class GoblinMage : Goblin
{
    private PackedScene fireball = (PackedScene)ResourceLoader.Load("res://scenes/particles/enemy's_fireball.tscn");

    private AnimatedSprite2D sprite;
    private Timer attackCooldownTimer = new Timer();
    private double attackCooldown = 2;
    private float acceleration = 1;
    private ChaseTarget _chaseTarget;

    public override void _Ready()
    {
        sprite = GetNode<AnimatedSprite2D>("sprite");
        PlayAnimation("idle");
        speed = 40;
    }

    public override void _Process(double delta)
    {
        attackCooldown -= delta;

        switch (currentState)
        {
            case States.Searching:
                SwapSpriteDirection();
                break;
            case States.Idle:
                break;
            case States.Move:
                SwapSpriteDirection();
                Move(Global.Player.GetPosition() - GlobalPosition);
                CastFireball();
                break;
        }
    }

    private new void Move(Vector2 dir)
    {
        if (Math.Abs(dir.Length()) > 140)
        {
            dir = -dir;
            PlayAnimation("move");
        }
        else if (Math.Abs(dir.Length()) > 135 && Math.Abs(dir.Length()) < 140)
        {
            dir = Vector2.Zero;
            PlayAnimation("idle");
        }

        base.Move(dir);
    }

    private async void CastFireball()
    {
        if (attackCooldown <= 0 && currentState != States.Attack)
        {
            Scale = new Vector2(-Scale.X, Scale.Y);
            attackCooldown = 2;
            currentState = States.Attack;
            await PlayAnimation("attack_before");
            Node2D fireballParticle = fireball.Instantiate<Node2D>();
            fireballParticle.GlobalPosition = GetNode<Marker2D>("fireball_pos").GlobalPosition;
            Global.World.GetWorld().AddChild(fireballParticle);
            GetNode<CpuParticles2D>("fireball_cast_particles").Emitting = true;
            await PlayAnimation("attack_after");
            currentState = States.Move;
            PlayAnimation("move");
        }
    }

    private async Task PlayAnimation(string animationName)
    {
        sprite.Play(animationName);
        await ToSignal(sprite, "animation_finished");
    }

    private async Task Idle()
    {
        sprite.Play("idle");
        await ToSignal(GetTree().CreateTimer((float)GD.RandRange(0, 1)), "timeout");
        currentState = States.Idle;
    }
    private void OnChaseTargetEntered(ChaseTarget chaseTarget)
    {
        _chaseTarget = chaseTarget;
    }
}