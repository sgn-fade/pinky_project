using System.Collections.Generic;
using Godot;
using projectpinky.scripts.drops;
using projectpinky.scripts.player;
using projectpinky.scripts.ui;
using projectpinky.scripts.weapons;

namespace projectpinky.scripts.Globals;

public partial class PlayerLoader : Node
{

	private Node2D closestObject;

	private PackedScene playerScene = (PackedScene)ResourceLoader.Load("res://scenes/main_character.tscn");
	public List<InventoryItem> playerInventory = new();
	public UiCore View;
	private Player player;
	private EventBus eventBus = Global.EventBus;


	public override void _Ready()
	{
		View = GetNode<UiCore>("/root/World/Ui");
	}

	public Player GetBody() => player;


    public Vector2 GetPosition()
    {
        return player?.GlobalPosition ?? Vector2.Zero;
    }
    public void SetPosition(Vector2 position)
    {
        player.GlobalPosition = position;
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