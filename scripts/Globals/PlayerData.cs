using Godot;
using projectpinky.scripts.player;
using projectpinky.scripts.ui;
using projectpinky.scripts.weapons;

namespace projectpinky.scripts.Globals;

public partial class PlayerData : Node2D
{
    private int hp = 50;
    private int mana = 30;
    private int maxHp = 50;
    private int maxMana = 30;
    private int coins;
    private bool canSmite;
    private Node2D closestInteractiveObject;
    private int magicDamage = 1;

    [Export] private PackedScene playerScene = (PackedScene)ResourceLoader.Load("res://scenes/main_character.tscn");
    [Export] private PackedScene book = (PackedScene)ResourceLoader.Load("res://scripts/weapons/magic_weapons/fire_book_tome_1.gd");

    private UiCore ui;
    private Player player;
    private Weapon weapon;
    private EventBus eventBus = Global.EventBus;

    public override void _Ready()
    {
        ui = GetNode<UiCore>("/root/World/Ui");
        SetWeapon(book.Instantiate<Weapon>());
    }

    public void SetState(Player.States state)
    {
        player.SetState(state);
    }

    public Player.States GetState()
    {
        return player.GetState();
    }

    public int GetHp() => hp;
    public int GetMaxHp() => maxHp;
    public int GetMana() => mana;
    public int GetMaxMana() => maxMana;
    public int GetMoney() => coins;
    public int GetMagicDamage() => magicDamage;
    public bool IsReady() => player != null;
    public Player GetBody() => player;
    public int GetZIndex() => player.ZIndex;
    public Weapon GetWeapon() => weapon;

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

    public void SetWeapon(Weapon newWeapon)
    {
        weapon = newWeapon;
    }



    public Node GetClosestObject() => closestInteractiveObject;

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


    public void PlayAnimation(string animationName)
    {
        player.GetHands().PlayAnimation(animationName);
    }

    public void SetMoney(int value)
    {
        if (value > 0)
        {
            coins += value;
        }
    }



    public void Restart()
    {
        player.QueueFree();
        hp = maxHp;
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
