using Godot;
using projectpinky.scripts.Enemies;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.particles;

public partial class Bullet : CharacterBody2D
{
    [Export] public MoveComponent moveComponent;

    public override void _Ready()
    {
        moveComponent.Init(GetGlobalMousePosition(), GlobalPosition);
    }
    private void OnEntityEntered() => QueueFree();
}