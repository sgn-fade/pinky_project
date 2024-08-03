using Godot;
using System;
[Icon("res://sprites/Editor/Icons/HurtBoxIcon.png")]
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
	public delegate void EntityTakeDamageEventHandler(int damage);

	[Signal]
	public delegate void EntityDeadEventHandler();

	[Export] public int MaxHp;
	public int Hp;

	public bool Invincible = false;

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
			case Statuses.Burn:
				EmitSignal(SignalName.EntityBurn, duration);
				break;
			case Statuses.Stun:
				EmitSignal(SignalName.EntityStuned, duration);
				break;
			case Statuses.Freeze:
				EmitSignal(SignalName.EntityFreezed, duration);
				break;
			case Statuses.Slowdown:
				EmitSignal(SignalName.EntitySlowdown, duration);
				break;
		}
	}

	public void TakeDamage(int damage)
	{
		if (!Invincible)
		{
			Hp -= damage;
			if (Hp <= 0) EmitSignal(SignalName.EntityDead);
			EmitSignal(SignalName.EntityTakeDamage, damage);
		}
	}

	public void Heal(int damage)
	{
		Hp += damage;
		if (Hp > MaxHp) Hp = MaxHp;
	}
}
