using Godot;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.locations;

public partial class World : Node2D
{
	private PackedScene dungeon = GD.Load<PackedScene>("res://scenes/locations/dungeon.tscn");
	private PackedScene hubZone = GD.Load<PackedScene>("res://scenes/locations/hub_zone.tscn");

	private Node location;

	private PlayerData player = Global.Player;
	public override void _Ready()
	{
		player = Global.Player;
		// Vector2 screenSize = new Vector2(1000, 600);
		Vector2I screenSize = DisplayServer.ScreenGetSize();
		var window = GetViewport().GetWindow();
		window.Size = screenSize;
		player.Spawn();
		GoToHub();
		//todo event bus
		// EventBus.Connect("generate_dungeon", new Callable(this, "GenerateDungeon"));
		// EventBus.Connect("load_game", new Callable(this, "LoadGame"));
		// EventBus.Connect("go_to_hub", new Callable(this, "GoToHub"));
		// EventBus.EmitSignal("load_game");
	}

	private void GenerateDungeon()
	{
		location?.QueueFree();
		location = dungeon.Instantiate  ();
		AddChild(location);
		// location.Call("generate_dungeon");
	}

	private void GoToHub()
	{
		location?.QueueFree();
		location = hubZone.Instantiate();
		AddChild(location);
		player.SetPosition(Vector2.Zero);
	}

	private void LoadGame()
	{
		GoToHub();
	}
}
