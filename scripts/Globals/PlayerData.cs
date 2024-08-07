using System.Collections.Generic;
using Godot;
using projectpinky.scripts.drops;
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
	private Node2D closestObject;
	private int magicDamage = 1;
	public float DashCooldown { get; set; } = 4f;

	private PackedScene playerScene = (PackedScene)ResourceLoader.Load("res://scenes/main_character.tscn");
	public List<InventoryItem> playerInventory = new();
	public UiCore View;
	private Player player;
	private Weapon weapon;
	private EventBus eventBus = Global.EventBus;

	public delegate void PlayerHpChanged(int hp, int maxHp);
	public static event PlayerHpChanged playerHpChanged;

	public delegate void PlayerManaChanged(int mana, int maxMana);
	public static event PlayerManaChanged playerManaChanged;

	public override void _Ready()
	{
		View = GetNode<UiCore>("/root/World/Ui");
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

	/// returns true if hp is greater than 0
	public bool SetHp(int value)
	{
		if ((hp += value) <= 0) return false;
		playerHpChanged?.Invoke(mana, maxMana);
		return true;

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
    public void SetMagicDamage(int newMagicDamage)
    {
        magicDamage = newMagicDamage;
    }
    public Vector2 GetPosition()
    {
        return player?.GlobalPosition ?? Vector2.Zero;
    }
    public void SetPosition(Vector2 position)
    {
        player.GlobalPosition = position;
    }
    public void SetWeapon(Weapon newWeapon)
    {
        weapon = newWeapon;
    }
    public Node GetClosestObject() => closestObject;
    public bool SetClosestObject(Node2D obj)
    {
        if (
            obj == null
            || closestObject == null
            || obj.GlobalPosition - GetPosition() < closestObject.GlobalPosition - GetPosition()
            )
        {
            closestObject = obj;
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
       SetPosition(Vector2.Zero);
    }
    public void Spawn()
    {
        Global.World.AddEntity(playerScene);
    }

	public void AddItem(InventoryItem item)
	{
		playerInventory.Add(item);
	}

	public void RemoveItemFromPlayer(InventoryItem item)
	{
		playerInventory.Remove(item);
	}
}
