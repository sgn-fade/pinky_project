using Godot;
using System;

public partial class HpBar : Control
{
    [Export] private TextureProgressBar redHpBar;
    [Export] private TextureProgressBar whiteHpBar;
    [Export] private Timer _timer;
    [Export] private Hurtbox _hurtBox;
    public override void _Ready()
    {
        var hpValue = _hurtBox.MaxHp * 10;

        whiteHpBar.MaxValue = hpValue;
        whiteHpBar.Value = hpValue;
        redHpBar.MaxValue = hpValue;
        redHpBar.Value = hpValue;
    }
    public async void UpdateHp(int hp, int maxHp)
    {
        redHpBar.MaxValue = maxHp * 10;
        redHpBar.Value = hp * 10;
        while (whiteHpBar.Value / 10 > hp && hp >= 0)
        {
            whiteHpBar.Value -= 1;
            _timer.Start(0.05f);
            await ToSignal(_timer, "timeout");
        }
    }
}