using Godot;
using System;
[Icon("res://sprites/Editor/Icons/HitBoxIcon.png")]
public partial class Hitbox : Area2D
{
    [Signal]
    public delegate void EntityEnteredEventHandler();

    [Export] private Hurtbox.Statuses status;
    [Export] private double statusDuration;
    [Export] private int Damage;

    private void OnHurtBoxEntered(Node2D body)
    {
        if (body is Hurtbox hurtBox)
        {
            hurtBox.TakeDamage(Damage);
            if(status != Hurtbox.Statuses.None) hurtBox.SetStatus(status, statusDuration);
        }
        EmitSignal(SignalName.EntityEntered);
    }
}