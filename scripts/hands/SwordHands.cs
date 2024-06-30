using projectpinky.scripts.Globals;
using projectpinky.scripts.player;

namespace projectpinky.scripts.hands;

using Godot;

public partial class SwordHands : MeleeHands
{
    private string[] comboNames = { "hit_1", "hit_2", "hit_3" };
    private int comboCount;
    private Timer comboTimer = new();
    private PlayerData player = Global.Player;

    public override void _Ready()
    {
        base._Ready();
        AddChild(comboTimer);
        comboTimer.OneShot = false;
    }

    public void Combos()
    {
        if (comboTimer.TimeLeft <= 0)
        {
            comboCount = 0;
        }
        comboCount++;
        Hit();
    }
    private void Hit()
    {
        PlayAnimation(comboNames[comboCount]);
        player.SetState(Player.States.Attack);
        comboTimer.Start(1);
        player.SetState(Player.States.Active);
    }
}