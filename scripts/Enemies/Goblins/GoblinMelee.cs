using Godot;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.Enemies.Goblins;

public partial class GoblinMelee : Goblin
{
    [Export] private HpBar _hpBar;
    [Export] private Hurtbox _hurtbox;
    private Area2D attackArea;
    private AnimatedSprite2D sprite;
    private CollisionShape2D collision;
    private Timer timer = new Timer();
    private double attackCooldown;
    private float acceleration = 40;
    private float speed = 60;
    private int enemyDamage = 10;
    private Vector2 direction = Vector2.Zero;

    public override void _Ready()
    {
        _hpBar.Init(_hurtbox.MaxHp);
        attackArea = GetNode<Area2D>("attack_area");
        sprite = GetNode<AnimatedSprite2D>("sprite");
        collision = GetNode<CollisionShape2D>("collision");

        base._Ready();

        speed = 60;
        enemyDamage = 10;
        //Spawn();//Todo animation
        //attackArea.Connect("body_entered", this, nameof(OnMeleeGoblinAttackAreaEntered));
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

    private void OnEntityTakeDamage(int damage, int hp, int maxHp)
    {
        sprite.Play("take_damage");
        GetNode<AnimationPlayer>("anim_player").Play("take_damage");
        _hpBar.UpdateHp(hp, maxHp);
    }
}