using Godot;
using projectpinky.scripts.Globals;
using projectpinky.scripts.Physics_components;

namespace projectpinky.scripts.Enemies.Goblins;

public partial class GoblinMelee : CharacterBody2D
{
    [Export] private HpBar _hpBar;
    [Export] private Hurtbox _hurtBox;
    [Export] private AnimatedSprite2D _sprite;
    [Export] private double _attackCooldown = 4;
    [Export] private MoveComponent _moveComponent;
    [Export] private AnimationTree _animationTree;
    private AnimationNodeStateMachinePlayback _stateMachine;
    private bool _canAttack = true;


    public override void _Ready()
    {
        _stateMachine = (AnimationNodeStateMachinePlayback)_animationTree.Get("parameters/playback");

        _hpBar.Init(_hurtBox.MaxHp);
    }

    public override void _Process(double delta)
    {
    }

    private void Attack()
    {
        if (_canAttack)
        {
            _canAttack = false;

            GetTree().CreateTimer(_attackCooldown).Timeout += () => _canAttack = true;


        }
    }

    private void OnEntityTakeDamage(int damage, int hp, int maxHp)
    {
        _stateMachine.Travel("take_damage");
        _hpBar.UpdateHp(hp, maxHp);
    }

    private void OnChaseTargetEntered(ChaseTarget chaseTarget)
    {
        _moveComponent.TargetEntity = chaseTarget;
    }

    private void OnTargetInAttackRange(Area2D target)
    {
        if(target == _moveComponent.TargetEntity) Attack();
    }
}