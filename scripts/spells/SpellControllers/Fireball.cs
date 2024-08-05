using System;
using Godot;
using projectpinky.scripts.Enemies;
using projectpinky.scripts.Globals;
using projectpinky.scripts.hands;
using projectpinky.scripts.Physics_components;

namespace projectpinky.scripts.particles;

public partial class Fireball : SpellController
{
    [Export] public MoveComponent moveComponent;

    public override void _Ready()
    {
        moveComponent.Init(GetGlobalMousePosition(), GlobalPosition);
    }

    protected override void Delete() => QueueFree();
    public override string GetAnim()
    {
        return SwordHands.Animations.Hit.ToString();
    }

    private void OnEntityEntered() => Delete();
}