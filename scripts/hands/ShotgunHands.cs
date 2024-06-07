using System.Threading.Tasks;
using Godot;
using Godot.Collections;
using projectpinky.scripts.Globals;
using projectpinky.scripts.player;

namespace projectpinky.scripts.hands;

public partial class ShotgunHands : GunHands
{
    [Export] private int ammo = 4;
    [Export] private double shootCooldown = 0.5;

    private PackedScene bullet = (PackedScene)ResourceLoader.Load("res://scenes/shotgun_bullet.tscn");


    private AnimationPlayer animationPlayer;
    private AnimationTree animationTree;
    private AnimationNodeStateMachinePlayback stateMachine;
    private Node world;

    private Label label;
    public override void _Ready()
    {
        label = GetNode<Label>("Label");
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        animationTree = GetNode<AnimationTree>("AnimationTree");
        stateMachine = (AnimationNodeStateMachinePlayback)animationTree.Get("parameters/playback");
    }

    public override void _Process(double delta)
    {
        LookAt(GetGlobalMousePosition());
        shootCooldown -= delta;
    }

    public override void _Input(InputEvent @event)
    {
        animationTree.Set("parameters/conditions/IsShoted", Input.IsActionJustPressed("mouse_left_button"));
        animationTree.Set("parameters/conditions/IsReload", Input.IsActionJustPressed("R"));
    }

    public void Shoot()
    {
        if (ammo > 0 && shootCooldown <= 0)
        {
            //Global.Player.GetBody().CharacterSlowdown();
            ammo -= 1;
            SpawnBullets();
        }
    }

    private void SpawnBullets()
    {
        for (int i = 0; i < 6; i++)
        {
            var bulletsInstance = bullet.Instantiate<Node2D>();
            Global.GlobalWorldInfo.GetWorld().AddChild(bulletsInstance);
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