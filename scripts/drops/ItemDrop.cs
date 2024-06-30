using System;
using System.Collections.Generic;
using Godot;
using projectpinky.scripts.Globals;
using projectpinky.scripts.player;
using projectpinky.scripts.spells;

namespace projectpinky.scripts.drops;

public partial class ItemDrop : Area2D
{

    [Export] private SpellData[] dropList = new SpellData[1];
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
        Global.Player.AddItem((new Spell(dropList[GD.Randi() % dropList.Length])).Data);
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
        if (body == Global.Player.GetBody())
        {
            _playerInArea = state;
        }
    }

}