using Godot;
using projectpinky.scripts.Enemies;
using projectpinky.scripts.Globals;
using projectpinky.scripts.Physics_components;

namespace projectpinky.scripts.particles;

public partial class Bullet : CharacterBody2D
{
    [Export] public MoveComponent moveComponent;

    public override void _Ready()
    {
        moveComponent.Init(GetGlobalMousePosition(), Global.Player.GetPosition());
    }

    private void OnEntityEntered() => QueueFree();
}