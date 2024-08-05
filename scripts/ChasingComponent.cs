using Godot;
using System;

public partial class ChasingComponent : Area2D
{
    [Export]private AnimationPlayer _animationPlayer;
    private ChaseTarget _currentTarget;

    [Signal]
    public delegate void TargetChangedEventHandler(ChaseTarget chaseTarget);


    private void OnAreaEntered(Area2D area)
    {
        if (area is ChaseTarget chaseTarget && chaseTarget != _currentTarget)
        {
            _currentTarget = chaseTarget;
            _animationPlayer.Stop();
            EmitSignal(SignalName.TargetChanged, chaseTarget);
        }
    }

    private void OnTimerTimeout()
    {
        _animationPlayer.Play("patrol");
    }
}
    