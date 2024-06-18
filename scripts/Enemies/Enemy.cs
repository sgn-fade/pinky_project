using Godot;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.Enemies;

public partial class Enemy : CharacterBody2D
{
    protected int hp = 1000000;
    protected int enemyDamage = 1;
    [Export] private PackedScene damageLabel;
    [Export] private PackedScene modulesDrop;

    [Export] protected ProgressBar hpBar;
    [Export] private CollisionShape2D collision;
    [Export] protected ProgressBar whiteAnimationBar;
    [Export] private Label status;
    [Export] private int speed;
    private Timer whiteBarTimer = new Timer();
    private Timer slowdownTimer = new Timer();
    private Timer timer = new Timer();
    private Timer fireDamageTimer = new Timer();
    private Vector2 direction = Vector2.Zero;
    private Node damageLabelInstance;
    private int damageToEnemy;

    public override void _Ready()
    {
        speed = 60;
        Global.GlobalWorldInfo.AddEnemy(this);

        damageLabelInstance = damageLabel.Instantiate();
        AddChild(damageLabelInstance);
        AddChild(whiteBarTimer);
        AddChild(slowdownTimer);
        AddChild(timer);
        AddChild(fireDamageTimer);

        whiteBarTimer.OneShot = false;
        slowdownTimer.OneShot = false;
        timer.OneShot = false;

        //EventBus.Connect("pulls_body", this, nameof(OnPullsBody));
        //EventBus.Connect("damage_to_enemy", this, nameof(OnDamageToEnemy));
        //EventBus.Connect("push_away_enemy", this, nameof(OnPushAwayEnemy));
        //TODO eventbus
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir;
    }

    public Vector2 GetDirection()
    {
        return direction;
    }

    private void EnemyDeath()
    {
        if (hp <= 0)
        {
            //EventBus.EmitSignal("enemy_killed");
            //TODO eventbus
            SpawnDrop();
            QueueFree();
        }
    }

    private void SpawnDrop()
    {
        Node2D moduleDrop = modulesDrop.Instantiate<Node2D>();
        Global.GlobalWorldInfo.GetWorld().AddChild(moduleDrop);
        moduleDrop.GlobalPosition = GlobalPosition;
        moduleDrop.ZIndex = ZIndex;
    }

    private async void UpdateHp()
    {
        hpBar.Value = hp * 10;
        while (whiteAnimationBar.Value / 10 > hp && hp >= 0)
        {
            whiteAnimationBar.Value -= 1;
            whiteBarTimer.Start(0.05f);
            await ToSignal(whiteBarTimer, "timeout");
        }
    }

    private async void Slowdown()
    {
        speed = 10;
        while (speed < 60)
        {
            speed += 1;
            slowdownTimer.Start(0.1f);
            await ToSignal(slowdownTimer, "timeout");
        }
    }

    private async void FireDamage()
    {
        status.Visible = true;
        GetNode<CpuParticles2D>("period_dmg_particle").Emitting = true;
        for (var i = 0; i < 3; i++)
        {
            fireDamageTimer.Start(1);
            await ToSignal(fireDamageTimer, "timeout");
            if (hp > 0)
            {
                hp -= 1;
            }

            UpdateHp();
            EnemyDeath();
        }

        GetNode<CpuParticles2D>("period_dmg_particle").Emitting = false;
        status.Visible = false;
    }

    private void OnPullsBody(Node body, Vector2 position)
    {
        //TODO Implement logic for pulls_body event
    }

    private void OnDamageToEnemy(Node body, int damage, string status)
    {
        if (this == body && hp >= 1)
        {
            if (status == "burn")
            {
                FireDamage();
            }

            damageLabelInstance.Call("ShowValue", damage);
            Velocity = (-direction.Normalized() * speed);
            MoveAndSlide();
            hp -= damage;
            Slowdown();
            ChasingPlayer();
            UpdateHp();
            EnemyDeath();
        }
    }

    private void ChasingPlayer()
    {
        //TODO Implement logic for chasing the player
    }

    private async void OnPushAwayEnemy(Node body, Vector2 dir)
    {
        if (this == body && hp >= 1)
        {
            float distance = 7;
            Velocity = dir.Normalized() * distance;
            for (int i = 0; i < 20; i++)
            {
                Velocity = dir.Normalized() * distance;
                MoveAndSlide();
                await ToSignal(GetTree().CreateTimer(0.01f), "timeout");
                distance += 7;
            }
        }
    }

    public void SetFocused(bool state)
    {
        GetNode<Sprite2D>("focus").Visible = state;
    }
}