using Godot;
using System;

public partial class ChasingComponent : Area2D
{
    [Signal]
    public delegate void TargetEnteredEventHandler(ChaseTarget chaseTarget);
    
    private void OnBodyEntered(Node2D body)
    {
        if (body is ChaseTarget chaseTarget)
        {
            EmitSignal(SignalName.TargetEntered, chaseTarget);
        }
    }
}
    