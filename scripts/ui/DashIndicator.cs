using Godot;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.ui;

public partial class DashIndicator : TextureProgressBar
{
    public override void _Ready()
    {
        SetProcess(false);
    }

    public override void _Process(double delta)
    {
        if (Value < MaxValue)
        {
            Value += delta * 1000;
            return;
        }
        SetProcess(false);
    }

    public void StartCooldown()
    {
        MaxValue = Global.Player.DashCooldown * 1000;
        Value = 0;
        SetProcess(true);
    }
}