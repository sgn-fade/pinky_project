using Godot;
using System;
using projectpinky.scripts.Enemies;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.particles;

public partial class FireSpearParticle : SpellController
{
    [Export] public MoveComponent moveComponent;

    public override void _Ready()
    {
        moveComponent.Init(GetGlobalMousePosition(), GlobalPosition);
    }

    protected override void Delete() => QueueFree();
    public override string GetAnim()
    {
        throw new NotImplementedException();
    }

    private void OnEntityEntered() => QueueFree();

}