using System.Threading.Tasks;
using Godot;
using Godot.Collections;
using projectpinky.scripts.Globals;
using projectpinky.scripts.particles;
using projectpinky.scripts.player;

namespace projectpinky.scripts.hands;

public partial class ShotgunHands : GunHands
{
    [Export] private int ammo = 4;
    [Export] private double shootCooldown = 0.5;
    [Export] private Marker2D barrelPosition;
    [Export] private AnimationPlayer animationPlayer;
    [Export] private AnimationTree animationTree;
    [Export] private Node2D body;

    private PackedScene bullet = (PackedScene)ResourceLoader.Load("res://scenes/shotgun_bullet.tscn");

    private AnimationNodeStateMachinePlayback stateMachine;

    public override void _Ready()
    {
        GD.Randomize();
        stateMachine = (AnimationNodeStateMachinePlayback)animationTree.Get("parameters/playback");
    }

    public override void _Process(double delta)
    {
        body.LookAt(GetGlobalMousePosition());
        shootCooldown -= delta;
    }

    public override void _Input(InputEvent @event)
    {
        animationTree.Set("parameters/conditions/IsShoted", Input.IsActionPressed("mouse_left_button"));
        animationTree.Set("parameters/conditions/IsReload", Input.IsActionJustPressed("R"));
    }

    public void Shoot()
    {
        if (ammo > 0 && shootCooldown <= 0)
        {
            ammo -= 1;
            SpawnBullets();
            if (ammo == 0)
            {
                stateMachine.Travel("reload");
            }
        }
    }

    private void SpawnBullets()
    {
        for (int i = 0; i < 6; i++)
        {
            var bulletInstance = bullet.Instantiate<Bullet>();

            var targetPosition = (GetGlobalMousePosition() - barrelPosition.GlobalPosition).Rotated((float)GD.RandRange(-0.15, 0.15));
            bulletInstance.Init(barrelPosition.GlobalPosition,
                targetPosition);

            Global.GlobalWorldInfo.GetWorld().AddChild(bulletInstance);
        }
    }

    public void Reload()
    {
        if (++ammo >= 6)
        {
            ((AnimationNodeStateMachinePlayback)animationTree.Get("parameters/playback")).Travel("idle");
        }
        else animationTree.Set("parameters/conditions/IsReload", false);
    }

    public void Cooldown()
    {
        shootCooldown = 0.5;
    }
}