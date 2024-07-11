using Godot;

namespace projectpinky.scripts.particles;

public partial class FireEye : SpellController
{
    [Export] private AnimationTree animationTree;
    private AnimationNodeStateMachinePlayback stateMachine;

    private Hitbox _hitbox;
    
    public override void _Ready()
    {
        stateMachine = (AnimationNodeStateMachinePlayback)animationTree.Get("parameters/playback");
    }

    protected override void Delete()
    {
        stateMachine.Travel("closing");
    }
}