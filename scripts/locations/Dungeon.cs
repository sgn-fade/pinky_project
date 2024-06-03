using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;
using Godot.Collections;

namespace projectpinky.scripts.locations;

public partial class Dungeon : Node2D
{
    private PackedScene light = GD.Load<PackedScene>("res://scenes/Lights.tscn");
    private PackedScene goblin = GD.Load<PackedScene>("res://scenes/enemies/goblins/goblin_melee.tscn");
    private PackedScene goblinMage = GD.Load<PackedScene>("res://scenes/enemies/goblins/goblin_mage.tscn");
    private PackedScene skeleton = GD.Load<PackedScene>("res://scenes/enemies/undeads/skeleton.tscn");
    private PackedScene box = GD.Load<PackedScene>("res://scenes/box.tscn");
    private PackedScene altar = GD.Load<PackedScene>("res://scenes/altar.tscn");
    private PackedScene grass = GD.Load<PackedScene>("res://scenes/grass.tscn");
    private PackedScene modulesDrop = GD.Load<PackedScene>("res://scenes/modules_drop.tscn");
    private PackedScene fireElemental = GD.Load<PackedScene>("res://scenes/enemies/elementals/fire_elemental.tscn");
    private PackedScene portal = GD.Load<PackedScene>("res://scenes/world_env/portal.tscn");
    private PackedScene fogParticles = GD.Load<PackedScene>("res://scenes/particles/fog.tscn");

    private PackedScene room = GD.Load<PackedScene>("res://scenes/locations/base_room.tscn");
    private Array<Vector2I> array = new();

    private List<PackedScene> rooms = new();
    private int tileSize = 64;
    private int minSize = 2;
    private int maxSize = 2;
    private int roomCount;
    private TileMap tileMap;
    private BetterTerrain terrain;
    private Timer timer = new();

    public override void _Ready()
    {
        tileMap = GetNode<TileMap>("tileMap");
        terrain = new BetterTerrain(tileMap);
        CenterRoom();
        for (int i = 0; i < 3; i++)
        {
            Vector2I distance = Vector2I.Zero;
            Vector2I direction = Vector2I.Zero;
            for (int j = 0; j < 3; j++)
            {
                Node2D bRoom = room.Instantiate<Node2D>();
                AddChild(bRoom);
                direction = (Vector2I)PlaceRoom(bRoom, direction, distance).Result;
                distance = (Vector2I)bRoom.GlobalPosition;
            }
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

    private void CenterRoom()
    {
        Node2D centerRoom = room.Instantiate<Node2D>();
        AddChild(centerRoom);
        centerRoom.GlobalPosition = Vector2.Zero;
        TileMapPattern pattern = tileMap.TileSet.GetPattern(0);
        tileMap.SetPattern(1, Vector2I.Zero - pattern.GetSize() / 2, pattern);
    }

    private async Task<Vector2> PlaceRoom(Node2D newRoom, Vector2I prevDirection, Vector2 prevDistance)
    {
        Vector2I direction = GenerateDirection(prevDirection);
        Vector2I distance = (Vector2I)(prevDistance + direction * 700);
        newRoom.GlobalPosition = distance;
        await ToSignal(GetTree().CreateTimer(0.1), "timeout");
        if (newRoom.GetNode<Area2D>("room_area").HasOverlappingAreas())
        {
            return await PlaceRoom(newRoom, prevDirection, distance);
        }
        TileMapPattern pattern = tileMap.TileSet.GetPattern((int)(GD.Randi() % 8));
        Vector2I patternPosition = distance / 64 - pattern.GetSize() / 2;
        tileMap.SetPattern(1, patternPosition, pattern);
        CreateTonel(-direction, newRoom);
        return direction;
    }

    private void CreateTonel(Vector2 direction, Node2D newRoom)
    {
        FillArray(direction, newRoom);
        terrain.SetCells(1, array, 1);
        array.Clear();
    }


    private void FillArray(Vector2 direction, Node2D newRoom)
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
                array.Add((Vector2I)newRoom.GlobalPosition / 64 + new Vector2I(x * Math.Sign(xDirection), y * Math.Sign(yDirection)));
            }
        }
    }

    private Vector2I GenerateDirection(Vector2I previousDirection)
    {
        Vector2I roomDirection;
        switch (GD.Randi() % 4 + 1)
        {
            case 1:
                roomDirection = new Vector2I(-1, 0);
                break;
            case 2:
                roomDirection = new Vector2I(1, 0);
                break;
            case 3:
                roomDirection = new Vector2I(0, -1);
                break;
            case 4:
                roomDirection = new Vector2I(0, 1);
                break;
            default:
                roomDirection = Vector2I.Zero;
                break;
        }

        if (previousDirection + roomDirection == Vector2.Zero || previousDirection == roomDirection)
        {
            return GenerateDirection(previousDirection);
        }

        return roomDirection;
    }



    private void _input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("Space"))
        {
            GetTree().ReloadCurrentScene();
        }
    }
}