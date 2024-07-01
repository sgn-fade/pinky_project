using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;
using Godot.Collections;

namespace projectpinky.scripts.locations;

public partial class Dungeon : Node2D
{
    private Array<Vector2I> array = new();

    private int tileSize = 64;
    [Export] private TileMap tileMap;
    private BetterTerrain terrain;

    [Export] private int width;
    [Export] private int height;
    private bool[,] matrixPath;
    public override void _Ready()
    {
        matrixPath = new bool[width, height];
        var currentPositionX = width / 2;
        var currentPositionY = height / 2;
        matrixPath[currentPositionX, currentPositionY] = true;

        terrain = new BetterTerrain(tileMap);
        CenterRoom();
        for (int i = 0; i < 3; i++)
        {
            CreateRooms();
        }
        Vector2 size = tileMap.GetUsedRect().Size;
        for (int x = -(int)size.X / 2; x < size.X / 2; x++)
        {
            for (int y = -(int)size.Y / 2; y < size.Y / 2; y++)
            {
                if (tileMap.GetCellSourceId(1, new Vector2I(x, y)) == 3)
                {
                    terrain.UpdateTerrainCell(1, new Vector2I(x, y), false);
                }
            }
        }
    }

    private void CreateRooms()
    {
        var position = Vector2I.Zero;
        var direction = Vector2I.Zero;
        for (var i = 0; i < 3; i++)
        {
            direction = GenerateDirection(direction);
            position += direction * 700;
            PlaceRoom(direction, position);
        }
    }
    private void CenterRoom()
    {
        var pattern = tileMap.TileSet.GetPattern(0);
        tileMap.SetPattern(1, Vector2I.Zero - pattern.GetSize() / 2, pattern);
    }

    private void PlaceRoom(Vector2I direction, Vector2I position)
    {
        var pattern = tileMap.TileSet.GetPattern((int)(GD.Randi() % 8));
        var patternPosition = position / 64 - pattern.GetSize() / 2;
        tileMap.SetPattern(1, patternPosition, pattern);
        CreateTunnel(-direction, position);
    }

    private void CreateTunnel(Vector2 direction, Vector2I position)
    {
        FillArray(direction, position);
        terrain.SetCells(1, array, 1);
        array.Clear();
    }


    private void FillArray(Vector2 direction, Vector2I position)
    {
        int yDirection = 0;
        int xDirection = 0;

        if (direction.X != 0)
        {
            xDirection = (int)direction.X;
            yDirection = 1;
        }
        if (direction.Y != 0)
        {
            yDirection = (int)direction.Y;
            xDirection = 1;
        }
        for (int y = 1; y < 1 + Math.Abs(direction.Y) * 10; y++)
        {
            for (int x = 1; x < 1 + Math.Abs(direction.X) * 10; x++)
            {
                array.Add(position / 64 + new Vector2I(x * Math.Sign(xDirection), y * Math.Sign(yDirection)));
            }
        }
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