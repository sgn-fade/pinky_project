using System.Collections.Generic;
using Godot;
using projectpinky.scripts.drops;
using projectpinky.scripts.player;
using projectpinky.scripts.ui;
using projectpinky.scripts.weapons;

namespace projectpinky.scripts.Globals;

public partial class PlayerLoader : Node2D
{

	private int coins;
	private Node2D closestObject;
	private int magicDamage = 1;
	public float DashCooldown { get; set; } = 4f;

	private PackedScene playerScene = (PackedScene)ResourceLoader.Load("res://scenes/main_character.tscn");
	public List<InventoryItem> playerInventory = new();
	public UiCore View;
	private Player player;
	private EventBus eventBus = Global.EventBus;


	public override void _Ready()
	{
		View = GetNode<UiCore>("/root/World/Ui");
	}



	public int GetMoney() => coins;
	public int GetMagicDamage() => magicDamage;
	public Player GetBody() => player;
	public int GetZIndex() => player.ZIndex;


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
        Spawn();
		SetPosition(Vector2.Zero);
    }
    public void Spawn()
    {
	    player = playerScene.Instantiate<Player>();
        Global.World.AddEntity(player);
        View.SetProcess(true);
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
