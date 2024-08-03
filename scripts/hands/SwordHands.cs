using projectpinky.scripts.Globals;
using projectpinky.scripts.player;

namespace projectpinky.scripts.hands;

using Godot;

public partial class SwordHands : Hands
{
    private string[] comboNames = { "hit_1", "hit_2", "hit_3" };
    private int comboCount;
    private Timer comboTimer = new();

    public enum Animations
    {
        Hit,
    }

    public override void _Ready()
    {
        base._Ready();
        AddChild(comboTimer);
        comboTimer.OneShot = false;

        comboTimer.Timeout += () => comboCount = 0;
    }

    protected override void LeftClickSpell()
    {
        comboCount++;
        Hit();
    }

    protected override void RightClickSpell()
    {
        PlayAnimation("parry");
        Player.Parry();
    }

    private void Hit()
    {
        PlayAnimation(comboNames[comboCount]);
        Player.CurrentState = Player.States.Attack;
        comboTimer.Start(1);
        Player.CurrentState = Player.States.Active;
    }
}