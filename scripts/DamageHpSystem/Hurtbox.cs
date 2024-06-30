using Godot;
using System;

public partial class Hurtbox : Area2D
{
    [Signal]
    public delegate void EntityBurnEventHandler(double duration);
    [Signal]
    public delegate void EntityStunedEventHandler(double duration);
    [Signal]
    public delegate void EntityFreezedEventHandler(double duration);
    [Signal]
    public delegate void EntitySlowdownEventHandler(double duration);
    [Signal]
    public delegate void EntityDeadEventHandler();

    [Export] private int maxHp;
    private int hp;

    public enum Statuses
    {
        None,
        Burn,
        Stun,
        Freeze,
        Slowdown,

    }

    public void SetStatus(Statuses status, double duration)
    {
        switch (status)
        {
            case Statuses.Burn :
                EmitSignal(SignalName.EntityBurn, duration);
                break;
            case Statuses.Stun :
                EmitSignal(SignalName.EntityStuned, duration);
                break;
            case Statuses.Freeze :
                EmitSignal(SignalName.EntityFreezed, duration);
                break;
            case Statuses.Slowdown :
                EmitSignal(SignalName.EntitySlowdown, duration);
                break;
        }
    }
    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0) EmitSignal(SignalName.EntityDead);
    }

    public void Heal(int damage)
    {
        hp += damage;
        if (hp > maxHp) hp = maxHp;
    }
}
