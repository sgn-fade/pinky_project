using Godot;
using projectpinky.scripts.Globals;

public partial class Spawner : Node2D
{
    [Export] private PackedScene[] _scenes;
    [Export] private int _minCount;
    [Export] private int _maxCount;
    [Export] private CollisionShape2D _collision;

    public void SpawnEntity()
    {
        var count = GD.RandRange(_minCount, _maxCount);
        for (int i = 0; i < count; i++)
        {
            var entity = _scenes[GD.Randi() % _scenes.Length].Instantiate<Node2D>();

            if (entity != null)
            {
                entity.GlobalPosition = GlobalPosition;
                if (_collision != null)
                {
                    var shape = (RectangleShape2D)_collision.Shape;
                    var shapeSize = shape.Size;
                    var x = GD.RandRange((double)-shapeSize.X / 2, (double)shapeSize.X / 2);
                    var y = GD.RandRange((double)-shapeSize.Y / 2, (double)shapeSize.Y / 2);
                    entity.GlobalPosition += new Vector2((float)x, (float)y);
                }

                Global.World.AddEntity(entity);
            }
        }

        QueueFree();
    }
}