using Godot;
using projectpinky.scripts.Globals;
using projectpinky.scripts.player;

namespace projectpinky.scripts.map_env;

public class Grass : StaticBody2D
{
    private PlayerData player;
    public override void _Ready()
    {
        player = GetNode<PlayerData>("/root/PlayerData");
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

    public void _OnArea2DBodyEntered(Node2D body)
    {
        if (body.Name != "grass")
        {
            if (player.GetPosition().X - GlobalPosition.X > 0)
            {
                GetNode<AnimationPlayer>("anim").Play("left");
            }
            else
            {
                GetNode<AnimationPlayer>("anim").Play("right");
            }
        }
    }
}