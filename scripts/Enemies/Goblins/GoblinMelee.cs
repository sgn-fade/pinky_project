using Godot;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.Enemies.Goblins;

public partial class GoblinMelee : Goblin
{
    [Export] private HpBar _hpBar;
    [Export] private Hurtbox _hurtbox;
    [Export] private AnimatedSprite2D _sprite;
    [Export] private CollisionShape2D _collision;
    private Timer timer = new Timer();
    private double attackCooldown;
    private float acceleration = 40;
    private float speed = 60;
    private int enemyDamage = 10;
    private Vector2 direction = Vector2.Zero;
    private ChaseTarget _chaseTarget;
    public override void _Ready()
    {
        _hpBar.Init(_hurtbox.MaxHp);
        base._Ready();

        speed = 60;
        enemyDamage = 10;
        //Spawn();//Todo animation
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
        _sprite.Play("idle");
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
            _sprite.Play("attack");
            timer.Start(0.8f);
            await ToSignal(timer, "timeout");
            direction = Global.Player.GetPosition() - GlobalPosition;
            //_attackArea.Monitoring = true;
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
            //_attackArea.Monitoring = false;
        }
    }

    private void OnEntityTakeDamage(int damage, int hp, int maxHp)
    {
        _sprite.Play("take_damage");
        GetNode<AnimationPlayer>("anim_player").Play("take_damage");
        _hpBar.UpdateHp(hp, maxHp);
    }

    private void OnChaseTargetEntered(ChaseTarget chaseTarget)
    {
        _chaseTarget = chaseTarget;
    }
}