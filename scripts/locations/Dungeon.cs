using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;
using Godot.Collections;

namespace projectpinky.scripts.locations;

public partial class Dungeon : Node2D
{
    [Export] private int _width;
    [Export] private int _height;
    [Export] private PackedScene[] _roomsVariants;
    [Export] private PackedScene _startRoom;
    [Export] private PackedScene _endRoom;
    private Room[,] _rooms;

    public override void _Ready()
    {
        _rooms = new Room[_width, _height];
        CreateRooms();
    }

    private void CreateRooms()
    {
        var currentPositionX = _width / 2;
        var currentPositionY = _height / 2;
        var direction = Vector2I.Zero;
        for (var i = 0; i < 3; i++)
        {
            direction = GenerateDirection(direction);
            PlaceRoom(currentPositionX, currentPositionY);
            currentPositionX += direction.X;
            currentPositionY += direction.Y;
        }
    }

    private void PlaceRoom(int currentPositionX, int currentPositionY)
    {
        var room = _roomsVariants[(int)(GD.Randi() % _roomsVariants.Length)].Instantiate<Room>();
        _rooms[currentPositionX, currentPositionY] = room;
        AddChild(room);

    }


    private Vector2I[] directions = { new(-1, 0), new(1, 0), new(0, -1), new(0, 1) };

    private Vector2I GenerateDirection(Vector2I previousDirection)
    {
        while (true)
        {
            var roomDirection = directions[GD.Randi() % 4];

            if (previousDirection + roomDirection == Vector2.Zero)
            {
                continue;
            }

            return roomDirection;
        }
    }
}