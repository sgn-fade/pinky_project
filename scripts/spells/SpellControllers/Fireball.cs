using Godot;
using projectpinky.scripts.Enemies;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.particles;

public partial class Fireball : SpellController
{
    [Export] public MoveComponent moveComponent;

    public override void _Ready()
    {
        moveComponent.Init(GetGlobalMousePosition(), Global.Player.GetPosition());
    }

    protected override void Delete() => QueueFree();

    private void OnEntityEntered() => QueueFree();
}