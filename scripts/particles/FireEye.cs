using Godot;
using System.Collections.Generic;
using projectpinky.scripts.Enemies;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.particles;

public partial class FireEye : CharacterBody2D
{
    private Timer _timer = new Timer();
    private Timer _damageTimer = new Timer();
    private AnimatedSprite2D _eyeAnimation;
    private float _spellDuration = 9f;
    private float _tickTime = 1f;
    private List<Enemy> _enemyInside = new();
    [Export] private AnimationTree animationTree;
    private AnimationNodeStateMachinePlayback stateMachine;


    public override void _Ready()
    {
        stateMachine = (AnimationNodeStateMachinePlayback)animationTree.Get("parameters/playback");
        GetTree().CreateTimer(_spellDuration).Timeout += () => { stateMachine.Travel("closing"); };

        _damageTimer.OneShot = false;
        AddChild(_damageTimer);
    }

    private async void DealDamage(Enemy enemy)
    {
        while (_enemyInside.Contains(enemy))
        {
            enemy.TakeDamage(2);
            _damageTimer.Start(_tickTime);
            await ToSignal(_damageTimer, "timeout");
        }
    }

    private void OnDamageAreaBodyEntered(Node2D body)
    {
        if (body is Enemy enemy)
        {
            _enemyInside.Add(enemy);
            DealDamage(enemy);
        }
    }

    private void OnDamageAreaBodyExited(Node2D body)
    {
        if (body is Enemy enemy)
        {
            _enemyInside.Remove(enemy);
            enemy.TakeDamage(2, "burn");
        }
    }
}