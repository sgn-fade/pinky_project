using System;
using System.Collections.Generic;
using Godot;
using projectpinky.scripts.Globals;
using projectpinky.scripts.player;
using projectpinky.scripts.spells;

namespace projectpinky.scripts.drops;

public partial class ItemDrop : Area2D
{

    [Export] private Spell[] dropList = new Spell[1];
    [Export] private AnimationPlayer animator;

    private bool _playerInArea;

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("E"))
        {
            animator.Play("delete");
        }
    }

    public void AddItem()
    {
        Global.PlayerLoader.AddItem(dropList[GD.Randi() % dropList.Length]);
    }

    public void OnBodyEntered(Node2D body)
    {
        TryChangePlayerState(true, body);
    }
    public void OnBodyExited(Node2D body)
    {
        TryChangePlayerState(false, body);
    }

    public void TryChangePlayerState(bool state, Node2D body)
    {
        if (body == Global.PlayerLoader.GetBody())
        {
            _playerInArea = state;
        }
    }

}