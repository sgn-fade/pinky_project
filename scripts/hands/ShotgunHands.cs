using System.Threading.Tasks;
using Godot;
using projectpinky.scripts.Globals;
using projectpinky.scripts.player;

namespace projectpinky.scripts.hands;


public partial class ShotgunHands : GunHands
{
    private int ammo = 4;
    private PackedScene bullet = (PackedScene)ResourceLoader.Load("res://scenes/shotgun_bullet.tscn");

    private enum DefaultStates
    {
        Idle,
        Shoot,
        Reload
    }

    private DefaultStates currentState = DefaultStates.Idle;
    private double shootCooldown;
    private float preparationAnimationTime = 1.54f;
    private AnimatedSprite2D sprite;
    private Timer timer;
    private Node2D pos;
    private CpuParticles2D particles;
    private CpuParticles2D sleeveParticle;
    private Node world;

    public override void _Ready()
    {
        sprite = GetNode<AnimatedSprite2D>("sprite");
        timer = GetNode<Timer>("timer");
        pos = GetNode<Node2D>("pos");
        particles = GetNode<CpuParticles2D>("pos/particles");
        sleeveParticle = GetNode<CpuParticles2D>("sleeve_particle");
        world = GetNode<Node>("..");

        PreparationAsync().ConfigureAwait(false);
    }

    public override void _Process(double delta)
    {
        shootCooldown -= delta;

        switch (currentState)
        {
            case DefaultStates.Idle:
                Rotating();
                ShootAsync().ConfigureAwait(false);
                ReloadAsync().ConfigureAwait(false);
                break;
            case DefaultStates.Shoot:
                Rotating();
                ShootAsync().ConfigureAwait(false);
                break;
            case DefaultStates.Reload:
                ReloadAsync().ConfigureAwait(false);
                ShootAsync().ConfigureAwait(false);
                break;
        }

        GlobalPosition = Global.Player.GetBody().Position;
    }

    private async Task PreparationAsync()
    {
        sprite.Play("prep");
        timer.Start(preparationAnimationTime);
        await ToSignal(timer, "timeout");
        SetIdleState();
    }

    private async Task ShootAsync()
    {
        if (ammo > 0 && currentState != DefaultStates.Shoot && Input.IsActionPressed("mouse_left_button") && shootCooldown <= 0)
        {
            currentState = DefaultStates.Shoot;
            Global.Player.GetBody().CharacterSlowdown();
            ammo -= 1;
            sprite.Play("shoot");
            timer.Start(0.27f);
            await ToSignal(timer, "timeout");
            particles.Emitting = true;
            sleeveParticle.Emitting = true;

            for (int i = 0; i < 6; i++)
            {
                var bulletsInstance = bullet.Instantiate<Node2D>();
                world.AddChild(bulletsInstance);
            }

            timer.Start(0.27f);
            await ToSignal(timer, "timeout");
            SetIdleState();
            shootCooldown = 0.5f;
        }
    }

    private async Task ReloadAsync()
    {
        if ((Input.IsActionJustPressed("reload") || ammo == 0) && currentState != DefaultStates.Reload && currentState != DefaultStates.Shoot)
        {
            currentState = DefaultStates.Reload;

            while (ammo < 6 && Global.Player.GetHp() > 0 && currentState == DefaultStates.Reload)
            {
                sprite.Play("reload");
                timer.Start(0.4f);
                await ToSignal(timer, "timeout");

                if (currentState != DefaultStates.Reload)
                    return;

                ammo += 1;
            }

            SetIdleState();
        }
    }

    private void Rotating()
    {
        // Rotating logic here
    }

    private void SetIdleState()
    {
        currentState = DefaultStates.Idle;
    }
}
