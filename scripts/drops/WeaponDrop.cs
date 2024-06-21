using System;
using System.Collections.Generic;
using Godot;
using projectpinky.scripts.Globals;
using projectpinky.scripts.spells;

namespace projectpinky.scripts.drops;

public partial class WeaponDrop : Area2D
{

    [Export] private Spell[] dropList = new Spell[1];
    private PackedScene weapon;

    private Sprite2D sprite;
    private Node2D body;

    private PlayerData player = Global.Player;
    private EventBus eventBus = Global.EventBus;

    public override void _Ready()
    {
        body = GetNode<Node2D>("body");
    }
    public override void _Process(double delta)
    {
        GD.Print(dropList[0].Icon);
        if (OverlapsBody(player.GetBody()))
        {
            dropList[0].Cast();
            if (Input.IsActionJustPressed("E"))
            {
                SetProcess(false);
                body.QueueFree();
                GetNode<CpuParticles2D>("end_particles").Emitting = true;
                Global.Player.AddItem(dropList[0].InvItem);
                GetTree().CreateTimer(0.3f).Timeout += QueueFree;
            }
        }

    }
}