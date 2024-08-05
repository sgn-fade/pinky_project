using Godot;

namespace projectpinky.scripts.ui;

public partial class DamageLabel : Label
{
    [Export] public int Damage;
    [Export] private AnimationPlayer _animationPlayer;

    public void ShowValue(int damage)
    {
        if (damage <= 0) return;

        _animationPlayer.Play("Move");
        Damage += damage;
        Text = (-Damage).ToString();
    }
}