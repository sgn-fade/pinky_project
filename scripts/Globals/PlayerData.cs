using Godot;
using projectpinky.scripts.player;
using projectpinky.scripts.ui;

namespace projectpinky.scripts.Globals;

public partial class PlayerData : Node2D
{
    private int hp = 50;
    private int mana = 30;
    private int maxHp = 50;
    private int maxMana = 30;
    private int coins;
    private int score;
    private int weaponCurrentSlot = 1;
    private bool canSmite;
    private Node2D closestInteractiveObject;
    private int magicDamage = 1;

    [Export] private PackedScene playerScene = (PackedScene)ResourceLoader.Load("res://scenes/main_character.tscn");
    [Export] private PackedScene book = (PackedScene)ResourceLoader.Load("res://scripts/weapons/magic_weapons/fire_book_tome_1.gd");

    private UiCore ui;
    private Player player;
    private Node2D weaponSlot1;
    private Node2D weaponSlot2;
    private EventBus eventBus;

    public override void _Ready()
    {
        eventBus = GetNode<EventBus>("/root/EventBus");
        ui = GetNode<UiCore>("/root/World/Ui");
        SetWeapon(book.Instantiate<Node2D>());
    }

    public void SetState(string state)
    {
        player.Call("change_state", state);
    }

    public string GetState()
    {
        return (string)player.Get("currentState");
    }

    public int GetHp() => hp;
    public int GetMaxHp() => maxHp;
    public int GetMana() => mana;
    public int GetMaxMana() => maxMana;

    public void UpdateHp(int value)
    {
        hp += value;
        ui.UpdateHpValue(hp, maxHp);
    }



    public bool ChangeMana(int value)
    {
        if (GetMana() < Mathf.Abs(value))
        {
            player.Call("throw_not_enough_mana_massage");
            return false;
        }
        mana += value;
        ui.UpdateManaValue(mana, maxMana);
        return true;
    }



    public void SetMaxMana(int value)
    {
        maxMana += value;
        ui.UpdateManaValue(mana, maxMana);
    }

    public void SetMagicDamage(int newMagicDamage)
    {
        magicDamage = newMagicDamage;
    }

    public int GetMagicDamage()
    {
        return magicDamage;
    }

    public Vector2 GetPosition()
    {
        if (player == null)
        {
            return Vector2.Zero;
        }
        return player.GlobalPosition;
    }

    public void SetPosition(Vector2 position)
    {
        player.GlobalPosition = position;
    }

    public bool IsReady()
    {
        return player != null;
    }

    public Player GetBody()
    {
        return player;
    }

    public int GetZIndex()
    {
        return player.ZIndex;
    }

    public Node2D GetWeapon()
    {
        if (weaponSlot1 == null && weaponSlot2 == null)
        {
            return null;
        }
        switch (GetWeaponCurrentSlot())
        {
            case 1: return weaponSlot1;
            case 2: return weaponSlot2;
            default: return null;
        }
    }

    public void SetWeapon(Node2D weapon)
    {
        switch (GetWeaponCurrentSlot())
        {
            case 1: weaponSlot1 = weapon; break;
            case 2: weaponSlot2 = weapon; break;
        }
    }

    public void SetWeaponCurrentSlot(int value)
    {
        weaponCurrentSlot = value;
    }

    public int GetWeaponCurrentSlot()
    {
        return weaponCurrentSlot;
    }

    public Node GetClosestObject()
    {
        return closestInteractiveObject;
    }

    public bool SetClosestObject(Node2D obj)
    {
        if (obj == null)
        {
            closestInteractiveObject = null;
            return true;
        }
        if (closestInteractiveObject == null)
        {
            closestInteractiveObject = obj;
            return true;
        }
        if ((obj.GlobalPosition - GetPosition()).Length() < (closestInteractiveObject.GlobalPosition - GetPosition()).Length())
        {
            closestInteractiveObject = obj;
            return true;
        }
        return false;
    }
    public int GetMoney()
    {
        return coins;
    }

    public void SetMoney(int value)
    {
        if (value > 0)
        {
            coins += value;
            score += value * 10;
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void SetScore(int value)
    {
        if (value <= 0)
        {
            return;
        }
        score += value;
    }

    public void Restart()
    {
        player.QueueFree();
        hp = maxHp;
        player = playerScene.Instantiate<Player>();
        Spawn();
        player.GlobalPosition = Vector2.Zero;
    }

    public void Spawn()
    {
        player = playerScene.Instantiate<Player>();
        //GlobalWorldInfo.GetWorld().AddChild(player);
    }

    public void SetSmite(bool state)
    {
        canSmite = state;
    }

    public bool GetSmite(Node2D enemy)
    {
        if (canSmite)
        {
            if ((int)enemy.Get("hp") <= 0 && enemy.GlobalPosition.DistanceTo(GetPosition()) < 0)
            {
                return true;
            }
        }
        return false;
    }
}
