using Godot;
using System;

public partial class HpBar : Control
{
    [Export] private TextureProgressBar redHpBar;
    [Export] private TextureProgressBar whiteHpBar;
    [Export] private Timer timer;

    public void Init(int maxHp)
    {
        whiteHpBar.MaxValue = maxHp * 10;
        whiteHpBar.Value = maxHp * 10;
        redHpBar.MaxValue = maxHp * 10;
        redHpBar.Value = maxHp * 10;
    }
    public async void UpdateHp(int hp, int maxHp)
    {
        redHpBar.MaxValue = maxHp * 10;
        redHpBar.Value = hp * 10;
        while (whiteHpBar.Value / 10 > hp && hp >= 0)
        {
            whiteHpBar.Value -= 1;
            timer.Start(0.05f);
            await ToSignal(timer, "timeout");
        }
    }
}