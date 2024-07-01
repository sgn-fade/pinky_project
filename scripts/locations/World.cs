using Godot;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.locations;

public partial class World : Node2D
{
	[Export] private PackedScene dungeon;
	[Export] private PackedScene hubZone;

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
