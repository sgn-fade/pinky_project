using Godot;
using projectpinky.scripts.weapons;

namespace projectpinky.scripts.player;

public class PlayerData
{
    public delegate void PlayerManaChanged(int mana, int maxMana);
    public static event PlayerManaChanged playerManaChanged;
    public Weapon Weapon { get; set; }
    private int mana = 30;
    private int maxMana = 30;
    public float DashCooldown { get; set; } = 4f;

    public int GetMana() => mana;
    public int GetMaxMana() => maxMana;
    public PlayerData()
    {

    }

    public bool SetMana(int value)
    {
        if (GetMana() < Mathf.Abs(value))
        {
            return false;
        }
        mana += value;
        playerManaChanged?.Invoke(mana, maxMana);
        return true;
    }
    public void SetMaxMana(int value)
    {
        maxMana = value;
        playerManaChanged?.Invoke(mana, maxMana);
    }
}