using Godot;
using projectpinky.scripts.Globals;
using projectpinky.scripts.player;

namespace projectpinky.scripts.map_env;

public partial class Grass : StaticBody2D
{
    public override void _Ready()
    {
        GetNode<Sprite2D>("Sprite").Frame = (int)(GD.Randi() % 6);
    }

    public void _OnVisibleScreenEntered()
    {
        ZIndex = (int)GlobalPosition.Y;
        Visible = true;
    }

    public void _OnVisibleScreenExited()
    {
        Visible = false;
    }
}