using Godot;

namespace projectpinky.scripts.ui;

public partial class DamageLabel : Label
{
    [Export] public int Damage;
    [Export] private AnimationPlayer _animationPlayer;

    public void ShowValue(int playerDamage)
    {
        if (playerDamage > 0)
        {
            _animationPlayer.Play("Move");
            Damage += playerDamage;
            Text = (-Damage).ToString();
        }
    }
}