using Godot;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.Enemies.Goblins;

public partial class GoblinMelee : Goblin
{
    private Area2D attackArea;
    private AnimatedSprite2D sprite;
    private ProgressBar hpBar;
    private ProgressBar whiteAnimationBar;
    private CollisionShape2D collision;
    private Timer timer = new Timer();
    private double attackCooldown;
    private float acceleration = 40;
    private int hp = 20;
    private float speed = 60;
    private int enemyDamage = 10;
    private Vector2 direction = Vector2.Zero;

    public override void _Ready()
    {
        attackArea = GetNode<Area2D>("attack_area");
        sprite = GetNode<AnimatedSprite2D>("sprite");
        hpBar = GetNode<ProgressBar>("hp_bar");
        whiteAnimationBar = GetNode<ProgressBar>("middle_white_bar");
        collision = GetNode<CollisionShape2D>("collision");

        base._Ready();
        hp = 20;
        speed = 60;
        enemyDamage = 10;
        hpBar.MaxValue = hp * 10;
        hpBar.Value = hp * 10;
        whiteAnimationBar.MaxValue = hp * 10;
        whiteAnimationBar.Value = hp * 10;
        Spawn();
        //attackArea.Connect("body_entered", this, nameof(OnMeleeGoblinAttackAreaEntered));
    }

    private async void Spawn()
    {
        sprite.Play("spawn");
        timer.Start(1.4f);
        await ToSignal(timer, "timeout");
        sprite.Play("idle");
        GetNode<Control>("middle_white_bar").Visible = true;
        hpBar.Visible = true;
        collision.SetDeferred("disabled", false);
        timer.Start(0.5f);
        await ToSignal(timer, "timeout");
        currentState = States.Idle;
    }

    public override void _Process(double delta)
    {
        attackCooldown -= delta;
        switch (currentState)
        {
            case States.Searching:
                SwapSpriteDirection();
                break;
            case States.Move:
                SwapSpriteDirection();
                Move(Global.Player.GetPosition() - GlobalPosition);
                PlayAnimation("move");
                Attack();
                break;
        }
    }

    private async void Idle(States state)
    {
        sprite.Play("idle");
        timer.Start((float)GD.RandRange(0, 1));
        await ToSignal(timer, "timeout");
        currentState = state;
    }

    private async void Attack()
    {
        if (direction.Length() < 30 && currentState != States.Attack && attackCooldown <= 0)
        {
            attackCooldown = 4;
            currentState = States.Attack;
            sprite.Play("attack");
            timer.Start(0.8f);
            await ToSignal(timer, "timeout");
            direction = Global.Player.GetPosition() - GlobalPosition;
            attackArea.Monitoring = true;
            for (int i = 0; i < 4; i++)
            {
                Velocity = Velocity.Lerp(direction.Normalized() * 320, 0.016f * acceleration);
                MoveAndSlide();
                timer.Start(0.02f);
                await ToSignal(timer, "timeout");
            }

            timer.Start(0.386f);
            await ToSignal(timer, "timeout");
            Idle(States.Move);
            attackArea.Monitoring = false;
        }
    }

    private void OnMeleeGoblinAttackAreaEntered(Node body)
    {
        if (body == Global.Player.GetBody() && currentState != States.DealsDamage)
        {
            currentState = States.DealsDamage;
            Vector2 playerOffsetDir = -(GlobalPosition - Global.Player.GetPosition()).Normalized();
            //EventBus.EmitSignal("player_take_damage", playerOffsetDir, 10);
            //TODO hurt box
        }
    }

    private async void OnDamageToEnemy(Node body, int damage, string status)
    {
        if (this == body)
        {
            currentState = States.None;
            sprite.Play("take_damage");
            GetNode<AnimationPlayer>("anim_player").Play("take_damage");
            await ToSignal(sprite, "animation_finished");
            currentState = States.Idle;
        }
    }

    private async void Slowdown()
    {
        float initialSpeed = 10.0f;
        while (initialSpeed < speed)
        {
            initialSpeed += 1.0f;
            await ToSignal(GetTree().CreateTimer(0.1f), "timeout");
        }
    }
}