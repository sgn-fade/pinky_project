using Godot;

namespace projectpinky.scripts.ui;

public partial class DamageLabel : Label
{
    private int damage;
    private double timeToFree = 0.5;


    public override void _Process(double delta)
    {
        timeToFree -= delta;
        if (!(timeToFree <= 0) || damage <= 0) return;
        damage = 0;
        Visible = false;
    }

    public void ShowValue(int playerDamage)
    {
        if (playerDamage > 0)
        {
            timeToFree = 0.5f;
            Visible = true;
            damage += playerDamage;
            Text = (-damage).ToString();
        }
    }
}