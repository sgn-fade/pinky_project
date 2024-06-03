using Godot;
using System;
using System.Collections.Generic;
using projectpinky.scripts.Globals;
using projectpinky.scripts.player;

public partial class MeleeHands : Node2D
{
    private Timer comboTimer = new Timer();
    private List<string> comboNames = new List<string> { "hit_1", "hit_2", "hit_3" };
    private PlayerData player;
    public override void _Ready()
    {
        player = GetNode<PlayerData>("/root/PlayerData");
        //EventBus.Connect("hands_play_animation", this, "PlayAnimation");
        AddChild(comboTimer);
        comboTimer.OneShot = true;
    }

    public override void _Process(double delta)
    {
        if (GetNode<AnimationPlayer>("anim").CurrentAnimation == "")
        {
            LookAt(GetGlobalMousePosition());
        }
    }

    public void PlayAnimation(string animationName)
    {
        if (animationName == "null")
        {
            return;
        }
        GetNode<AnimationPlayer>("anim").Play(animationName);
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton eventMouseButton &&
            eventMouseButton.Pressed &&
            //eventMouseButton.ButtonIndex == (int)ButtonList.Left &&
            player.GetState() != Player.States.Spell)
        {
            if (comboTimer.TimeLeft <= 0)
            {
                comboNames = new List<string> { "hit_1", "hit_2", "hit_3" };
            }
            string hitName = comboNames[0];
            comboNames.RemoveAt(0);
            comboNames.Add(hitName);
            Hit(hitName);
        }
    }

    private void Hit(string hitCount)
    {
        player.SetState(Player.States.Attack);
        player.GetBody().SetSpeed(20);
        PlayAnimation(hitCount);
        comboTimer.Start(1);
        player.GetBody().SetSpeed(80);
        player.SetState(Player.States.Idle);
        PlayAnimation("null");
    }
}