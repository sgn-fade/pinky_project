using Godot;
using System.Collections.Generic;
using projectpinky.scripts.Enemies;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.particles;

public partial class FireEye : SpellController
{
    private Timer _damageTimer = new ();
    private float _tickTime = 1f;
    private List<Enemy> _enemyInside = new();
    [Export] private AnimationTree animationTree;
    private AnimationNodeStateMachinePlayback stateMachine;


    public override void _Ready()
    {
        stateMachine = (AnimationNodeStateMachinePlayback)animationTree.Get("parameters/playback");

        _damageTimer.OneShot = false;
        AddChild(_damageTimer);
    }

    protected override void Delete()
    {
        stateMachine.Travel("closing");
    }

    private async void DealDamage(Enemy enemy)
    {
        while (_enemyInside.Contains(enemy))
        {
            enemy.TakeDamage(Damage);
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
            enemy.TakeDamage(Damage, "burn");
        }
    }
}