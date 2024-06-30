using Godot;
using System;

public partial class Hitbox : Area2D
{
    [Signal]
    public delegate void EntityEnteredEventHandler();

    [Export] private int Damage;

    private void OnHurtBoxEntered(Node2D body)
    {
        if (body is Hurtbox hurtBox)
        {
            hurtBox.TakeDamage(Damage);
        }
        EmitSignal(SignalName.EntityEntered);
    }
}