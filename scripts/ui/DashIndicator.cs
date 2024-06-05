using Godot;

namespace projectpinky.scripts.ui;

public partial class DashIndicator : TextureProgressBar
{
    private const float Cooldown = 4.0f;
    private bool isReady = true;

    public override void _Ready()
    {
        MaxValue = Cooldown * 1000;
        SetProcess(false);

    }

    public override void _Process(double delta)
    {
        if (Value < MaxValue)
        {
            Value += delta * 1000;
            return;
        }
        isReady = true;
        SetProcess(false);
    }

    private void StartCooldown()
    {
        Value = 0;
        isReady = false;
        SetProcess(true);
    }
    //todo connection to player
    public bool GetReady() => isReady;
}