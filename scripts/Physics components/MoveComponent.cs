using Godot;

namespace projectpinky.scripts.Physics_components;

public partial class MoveComponent : Node2D
{
    [Signal]
    public delegate void EntityComeToTargetEventHandler();

    public bool IsActive { get; set; }
    [Export] public CharacterBody2D MoveTarget;

    private Node2D _targetEntity;
    public Node2D TargetEntity
    {
        get => _targetEntity;
        set
        {
            _targetEntity = value;
            IsActive = true;
        }
    }
    private Vector2 _targetPosition;

    public Vector2 TargetPosition
    {
        get => _targetPosition;
        set
        {
            _targetPosition = value;
            IsActive = true;
        }
    }

    [Export] public bool LookAtTarget;
    [Export] public bool FlipByDirection;
    [Export] public Node2D NodeToFlip;
    [Export] public double Speed;
    [Export] public double MaxSpeed;
    [Export] public double Acceleration;

    public void Init(CharacterBody2D targetEntity, Vector2 spawnPosition)
    {
        TargetEntity = targetEntity;
        MoveTarget.GlobalPosition = spawnPosition;
    }
    public void Init(Vector2 targetPoint, Vector2 spawnPosition)
    {
        TargetPosition = targetPoint;
        MoveTarget.GlobalPosition = spawnPosition;
    }

    public override void _Process(double delta)
    {
        if (IsActive) return;
        if (TargetEntity != null) TargetPosition = TargetEntity.GlobalPosition;

        MoveTarget.Velocity = (TargetPosition - GlobalPosition).Normalized() * (float)Speed;

        MoveTarget.MoveAndSlide();

        if (MoveTarget.Velocity.Length() < 0.1)
        {
            EmitSignal(SignalName.EntityComeToTarget);
            IsActive = false;
        }


        if (Speed < MaxSpeed)
        {
            Speed *= Acceleration * delta;
            if (Speed > MaxSpeed) Speed = MaxSpeed;
        }

        if (FlipByDirection) SwapSpriteDirection();
        if (LookAtTarget) MoveTarget.LookAt(TargetPosition);

    }

    private void SwapSpriteDirection()
    {
        NodeToFlip.Scale = MoveTarget.Velocity.X switch
        {
            <= 0 => new Vector2(-1, NodeToFlip.Scale.Y),
            > 0 => new Vector2(1, NodeToFlip.Scale.Y),
            _ => NodeToFlip.Scale
        };
    }
}