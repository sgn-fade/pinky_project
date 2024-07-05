using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;
using Godot.Collections;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.locations;

public partial class Dungeon : Node2D
{
    private PlayerData _player = Global.Player;
    [Export] private int _width;
    [Export] private PackedScene[] _roomsVariants;
    [Export] private PackedScene _startRoom;
    [Export] private PackedScene _endRoom;
    private Room currentRoom;

    public override void _Ready()
    {
        CreateStartRoom();
    }


    private void RoomOnChangeRoom()
    {
        currentRoom.ChangeRoom -= RoomOnChangeRoom;
        RemoveChild(currentRoom);
        currentRoom = _roomsVariants[GD.Randi()%_roomsVariants.Length].Instantiate<Room>();
        _player.SetPosition(currentRoom.GetSpawnPosition());
        AddChild(currentRoom);
        currentRoom.ChangeRoom += RoomOnChangeRoom;
    }

    private void CreateStartRoom()
    {
        currentRoom = _startRoom.Instantiate<Room>();
        currentRoom.ChangeRoom += RoomOnChangeRoom;
        AddChild(currentRoom);
    }

}