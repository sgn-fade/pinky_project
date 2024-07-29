using Godot;
using projectpinky.scripts.Globals;

//chance to spawn

public partial class Spawner : Node2D
{
    [Export] private PackedScene[] _scenes;

    public override void _Ready()
    {
        var entity = _scenes[GD.Randi() % _scenes.Length].Instantiate<Node2D>();
        if (entity != null)
        {
            entity.GlobalPosition = GlobalPosition;
            Global.World.AddEntity(entity);
        }
        
        QueueFree();
    }
}