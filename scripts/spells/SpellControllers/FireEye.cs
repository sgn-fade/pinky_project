using Godot;
using System.Collections.Generic;
using projectpinky.scripts.Enemies;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.particles;

public partial class FireEye : SpellController
{
    private Timer _damageTimer = new();
    private float _tickTime = 1f;
    [Export] private AnimationTree animationTree;
    private AnimationNodeStateMachinePlayback stateMachine;

    private Hitbox _hitbox;


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
}