using Godot;
using System.Collections.Generic;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.particles;

public partial class FireEye : CharacterBody2D
{
    private Timer _timer = new Timer();
    private Timer _damageTimer = new Timer();
    private AnimatedSprite2D _eyeAnimation;
    private float _spellDuration = 9f;
    private float _tickTime = 1f;
    private List<Node> _enemyInside = new List<Node>();
    [Export] private AnimationTree animationTree;
    private AnimationNodeStateMachinePlayback stateMachine;


    public override void _Ready()
    {
        stateMachine = (AnimationNodeStateMachinePlayback)animationTree.Get("parameters/playback");
        GetTree().CreateTimer(_spellDuration).Timeout += () => { stateMachine.Travel("closing");};
        
        _damageTimer.OneShot = false;
        AddChild(_damageTimer);

    }

    private async void DealDamage(Node body)
    {
        while (_enemyInside.Contains(body))
        {
            //todo event bus
            Global.EventBus.EmitSignal("damage_to_enemy", body, 2);
            _damageTimer.Start(_tickTime);
            await ToSignal(_damageTimer, "timeout");
        }
    }

    private void OnDamageAreaBodyEntered(Node2D body)
    {
        _enemyInside.Add(body);
        DealDamage(body);
    }

    private void OnDamageAreaBodyExited(Node2D body)
    {
        _enemyInside.Remove(body);
        //todo event bus
        Global.EventBus.EmitSignal("damage_to_enemy", body, 5, "burn");
    }
}