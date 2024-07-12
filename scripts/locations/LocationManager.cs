using Godot;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.locations;

public partial class LocationManager : Node2D
{
	[Export] private PackedScene dungeon;
	[Export] private PackedScene hubZone;
	[Export] private PackedScene trainZone;

	private Node location;
	private PlayerLoader player;

	public enum Locations
	{
		Dungeon,
		Hub,
		TrainRoom,
	}

	[Export] private Locations currentLocation;
	public override void _Ready()
	{
		player= Global.PlayerLoader;
		// Vector2 screenSize = new Vector2(1000, 600);
		Vector2I screenSize = DisplayServer.ScreenGetSize();
		var window = GetViewport().GetWindow();
		window.Size = screenSize;
		player.Spawn();
		ChangeLocation(GetLocation());
	}


	private PackedScene GetLocation()
	{
		switch (currentLocation)
		{
			case Locations.Dungeon : return dungeon;
			case Locations.Hub : return hubZone;
			case Locations.TrainRoom : return trainZone;
		}

		return null;
	}
	private void ChangeLocation(PackedScene newLocation)
	{
		location?.QueueFree();
		location = newLocation.Instantiate();
		AddChild(location);
	}


	private void LoadGame()
	{
		ChangeLocation(hubZone);
	}
}
