using System;
using System.Threading.Tasks;
using Godot;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.Enemies.Goblins;

public partial class GoblinMage : Goblin
{
    private PackedScene fireball = (PackedScene)ResourceLoader.Load("res://scenes/particles/enemy's_fireball.tscn");

    private PackedScene fireElemental =
        (PackedScene)ResourceLoader.Load("res://scenes/enemies/elementals/fire_elemental.tscn");

    private AnimatedSprite2D sprite;
    private Timer attackCooldownTimer = new Timer();
    private Timer elementalCooldownTimer = new Timer();
    private double attackCooldown = 2;
    private double elementalCooldown = 10;
    private float acceleration = 1;

    public override void _Ready()
    {
        sprite = GetNode<AnimatedSprite2D>("sprite");
        PlayAnimation("idle");

        hp = 10;
        hpBar.MaxValue = hp * 10;
        hpBar.Value = hp * 10;
        whiteAnimationBar.MaxValue = hp * 10;
        whiteAnimationBar.Value = hp * 10;
        speed = 40;
    }

    public override void _Process(double delta)
    {
        attackCooldown -= delta;
        elementalCooldown -= delta;

        switch (currentState)
        {
            case States.SEARCHING:
                SwapSpriteDirection();
                break;
            case States.IDLE:
                break;
            case States.MOVE:
                SwapSpriteDirection();
                Move(Global.Player.GetPosition() - GlobalPosition);
                CastFireball();
                break;
            case States.PULLS:
                Attract(delta);
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
        if (attackCooldown <= 0 && currentState != States.ATTACK)
        {
            Scale = new Vector2(-Scale.X, Scale.Y);
            attackCooldown = 2;
            currentState = States.ATTACK;
            await PlayAnimation("attack_before");
            Node2D fireballParticle = fireball.Instantiate<Node2D>();
            fireballParticle.GlobalPosition = GetNode<Marker2D>("fireball_pos").GlobalPosition;
            Global.GlobalWorldInfo.GetWorld().AddChild(fireballParticle);
            GetNode<CpuParticles2D>("fireball_cast_particles").Emitting = true;
            await PlayAnimation("attack_after");
            currentState = States.MOVE;
            PlayAnimation("move");
        }
    }

    private async void SummonElemental()
    {
        if (elementalCooldown <= 0 && currentState != States.ATTACK)
        {
            currentState = States.ATTACK;
            Node2D summon = fireElemental.Instantiate<Node2D>();
            Global.GlobalWorldInfo.GetWorld().AddChild(summon);
            summon.GlobalPosition = GlobalPosition + new Vector2(10, 0);
            elementalCooldown = new Random().Next(10, 40);
            await Idle();
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
        currentState = States.IDLE;
    }
}