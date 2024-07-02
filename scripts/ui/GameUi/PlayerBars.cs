using Godot;
using projectpinky.scripts.Globals;
using projectpinky.scripts.player;

namespace projectpinky.scripts.ui;

public partial class PlayerBars : Node
{
    [Export] private TextureProgressBar hpBar;
    [Export] private TextureProgressBar manaBar;

    public override void _Ready()
    {
        Player.playerHpChanged += UpdateHpValue;
        PlayerData.playerManaChanged += UpdateManaValue;
    }

    public override void _ExitTree()
    {
        Player.playerHpChanged -= UpdateHpValue;
        PlayerData.playerManaChanged -= UpdateManaValue;
    }

    public void UpdateHpValue(int hp, int maxHp)
    {
        hpBar.MaxValue = maxHp;
        hpBar.Value = hp;
    }

    public void UpdateManaValue(int mana, int maxMana)
    {
        manaBar.MaxValue = maxMana;
        manaBar.Value = mana;
    }
}