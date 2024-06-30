using Godot;
using projectpinky.scripts.Globals;
using projectpinky.scripts.ui;

namespace projectpinky.scripts.Enemies;

public abstract partial class Enemy : CharacterBody2D
{
    [Export] protected int hp;
    [Export] protected int damage;
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
    private DamageLabel damageLabelInstance;

    public enum Statuses
    {
        None,
        Burning,
        
    }
    public override void _Ready()
    {
        Global.World.AddEnemy(this);

        damageLabelInstance = damageLabel.Instantiate<DamageLabel>();
        AddChild(damageLabelInstance);
        AddChild(whiteBarTimer);
        AddChild(slowdownTimer);
        AddChild(timer);
        AddChild(fireDamageTimer);

        whiteBarTimer.OneShot = false;
        slowdownTimer.OneShot = false;
        timer.OneShot = false;
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
            SpawnDrop();
            QueueFree();
        }
    }

    public abstract void SpawnDrop();

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

    public void TakeDamage(int damage, Statuses status = Statuses.None)
    {
        if (hp >= 1)
        {
            if (status == Statuses.Burning)
            {
                FireDamage();
            }

            damageLabelInstance.ShowValue(damage);
            Velocity = (-direction.Normalized() * speed);
            MoveAndSlide();
            hp -= damage;
            UpdateHp();
            EnemyDeath();
        }
    }
    
    public void SetFocused(bool state)
    {
        GetNode<Sprite2D>("focus").Visible = state;
    }
}