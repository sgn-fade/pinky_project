using Godot;
using System;

public partial class Hurtbox : Area2D
{
    [Signal]
    public delegate void EntityDeadEventHandler();

    [Export] private int maxHp;
    private int hp;
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
