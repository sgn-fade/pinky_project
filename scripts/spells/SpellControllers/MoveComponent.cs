using Godot;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.particles;

public partial class MoveComponent : Node2D
{
    [Export] public CharacterBody2D MoveTarget;
    [Export] public int Speed;
    public void Init(Vector2 targetPosition, Vector2 spawnPosition)
    {
        MoveTarget.GlobalPosition = spawnPosition;
        MoveTarget.Velocity = (targetPosition - GlobalPosition).Normalized() * Speed;
        MoveTarget.LookAt(targetPosition);
    }

    public override void _Process(double delta)
    {
        MoveTarget.MoveAndSlide();
    }
}