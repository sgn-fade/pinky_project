using Godot;
using Godot.Collections;

namespace projectpinky.scripts.locations;

public partial class BaseRoomDungeon : Node2D
{
    private Array<Vector2I> _array = new Array<Vector2I>();

    public Vector2 GetSize()
    {
        return GetNode<RectangleShape2D>("room_area/size_rect").GetRect().Size;
    }

    public Vector2 GetCenter()
    {
        return GlobalPosition;
    }

    public void CreateTonel(Vector2 direction)
    {
        FillArray(direction);
        GetNode<TileMap>("tileset").SetCellsTerrainConnect(0, _array, 0, 0);
    }

    private void FillArray(Vector2 direction)
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

        for (int y = 1; y < 2 + Mathf.Abs((int)direction.Y) * 10; y++)
        {
            for (int x = 1; x < 2 + Mathf.Abs((int)direction.X) * 10; x++)
            {
                _array.Add(new Vector2I((x - 1) * Mathf.Sign(xDirection), (y - 1) * Mathf.Sign(yDirection)));
            }
        }
    }
}