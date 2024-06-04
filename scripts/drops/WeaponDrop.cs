using System;
using System.Collections.Generic;
using Godot;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.drops;

public partial class WeaponDrop : Area2D
{
    [Export] private PackedScene OldGoblinsMagicWand = GD.Load<PackedScene>("res://scripts/weapons/magic_weapons/old_goblins_magic_wand.gd");
    [Export] private PackedScene FireBookTome1 = GD.Load<PackedScene>("res://scripts/weapons/magic_weapons/fire_book_tome_1.gd");
    [Export] private PackedScene GoblinSword = GD.Load<PackedScene>("res://scripts/weapons/melee/sword.gd");

    private List<PackedScene> weaponList = new List<PackedScene>();
    private PackedScene weapon = null;

    private Sprite2D sprite;
    private Node2D body;

    private PlayerData player = Global.Player;
    private EventBus eventBus = Global.EventBus;

    public override void _Ready()
    {
        sprite = GetNode<Sprite2D>("body/sprite");
        body = GetNode<Node2D>("body");
    }

    public override void _Process(double delta)
    {
        body.ZIndex = (int)(GlobalPosition.Y / 2);
        body.Position = new Vector2(body.Position.X, (float)(Math.Sin(Time.GetTicksMsec() * 0.003) * 2));

        if (OverlapsBody(player.GetBody()))
        {
            sprite.Frame = 1;
            //eventBus.EmitSignal("show_weapon_stats_on_game_screen", weapon);

            if (Input.IsActionJustPressed("E"))
            {
                SetProcess(false);
                body.QueueFree();
                GetNode<CpuParticles2D>("end_particles").Emitting = true;
                //eventBus.EmitSignal("add_item", weapon);
                //eventBus.EmitSignal("hide_weapon_stats_on_game_screen");
                GetTree().CreateTimer(0.3f).Timeout += QueueFree;
            }
        }
        else if (sprite.Frame == 1)
        {
            //eventBus.EmitSignal("hide_weapon_stats_on_game_screen");
            sprite.Frame = 0;
        }
    }

}