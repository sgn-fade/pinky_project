using Godot;
using System;
using projectpinky.scripts.Globals;
using projectpinky.scripts.player;
using projectpinky.scripts.weapons;

namespace projectpinky.scripts.particles;

public partial class ContinuumShotgun : Weapon
{
    
    // YA PEREVEL NO TUT KOD STARSHE MENYA 
    // [Export] public PackedScene Bullet = GD.Load<PackedScene>("res://scenes/continuum_shotgun_bullet.tscn");
    // [Export] public PackedScene Orb = GD.Load<PackedScene>("res://scenes/c_shotgun_orb.tscn");
    //
    // private int _ammo = 6;
    // private float _shootCharge = 0;
    // private float _shootCooldown = 1f;
    //
    // private enum C_Shotgun_States
    // {
    //     HAND = 4
    // }
    //
    // public override void _Process(double delta)
    // {
    //     _shootCooldown -= (float)delta;
    //
    //     switch (CurrentState)
    //     {
    //         case DefaultStates.IDLE:
    //             Rotating();
    //             Shoot(delta);
    //             Reload();
    //             break;
    //         case DefaultStates.SHOOT:
    //             Rotating();
    //             Shoot(delta);
    //             break;
    //         case DefaultStates.RELOAD:
    //             Reload();
    //             Shoot(delta);
    //             break;
    //         case (DefaultStates)C_Shotgun_States.HAND:
    //             break;
    //     }
    //
    //     GlobalPosition = Global.Player.GetPosition();
    //     GlobalPosition += new Vector2(0, 2);
    // }
    //
    // public override void _Ready()
    // {
    //     Sprite.Play("the hand");
    //     CurrentState = (DefaultStates)C_Shotgun_States.HAND;
    //     Create();
    //     SetIdleState();
    // }
    //
    // private void Shoot(double delta)
    // {
    //     if (_ammo > 0 && _shootCooldown <= 0)
    //     {
    //         if (Input.IsActionPressed("mouse_left_button"))
    //         {
    //             if (_shootCharge > 1.83f)
    //             {
    //                 Sprite.Play("fullcharged");
    //             }
    //             else
    //             {
    //                 Sprite.Play("charging");
    //             }
    //             _shootCharge += (float)delta;
    //             CurrentState = DefaultStates.SHOOT;
    //             Player.Instance.GetBody().CharacterSlowdown();
    //         }
    //         else if (_shootCharge > 0)
    //         {
    //             _shootCooldown = 1f;
    //             Sprite.Stop();
    //             Sprite.Frame = 0;
    //
    //             if (_shootCharge < 1f)
    //             {
    //                 ShootStage1();
    //             }
    //             else if (_shootCharge < 1.75f)
    //             {
    //                 ShootStage2();
    //             }
    //             else
    //             {
    //                 ShootStage3();
    //             }
    //         }
    //     }
    // }
    //
    // private async void ShootStage1()
    // {
    //     Player.Instance.GetBody().CShotgunRecoil();
    //     Sprite.Play("shoot_1");
    //
    //     for (int i = 0; i < 6; i++)
    //     {
    //         var bulletInstance = (Node2D)Bullet.Instance();
    //         GetTree().CurrentScene.AddChild(bulletInstance);
    //     }
    //
    //     _shootCharge = 0;
    //     Timer timer = new Timer();
    //     AddChild(timer);
    //     timer.OneShot = true;
    //     timer.WaitTime = 0.583f;
    //     timer.Start();
    //     await ToSignal(timer, "timeout");
    //     SetIdleState();
    //     timer.QueueFree();
    // }
    //
    // private async void ShootStage2()
    // {
    //     _shootCharge = 0;
    //     SetIdleState();
    // }
    //
    // private async void ShootStage3()
    // {
    //     Sprite.Play("shoot_1");
    //     var orbInstance = (Node2D)Orb.Instance();
    //     GetTree().CurrentScene.AddChild(orbInstance);
    //
    //     _shootCharge = 0;
    //     Timer timer = new Timer();
    //     AddChild(timer);
    //     timer.OneShot = true;
    //     timer.WaitTime = 0.583f;
    //     timer.Start();
    //     await ToSignal(timer, "timeout");
    //     SetIdleState();
    //     timer.QueueFree();
    // }
    //
    //
    // private void SetIdleState()
    // {
    //     CurrentState = DefaultStates.IDLE;
    // }
}
