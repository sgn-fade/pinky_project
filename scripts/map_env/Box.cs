using Godot;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.map_env;

public partial class Box : StaticBody2D
{
    private bool playerInArea;

    [Export] private Sprite2D buttonIndicator;

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("E") && playerInArea)
        {
            SetProcess(false);
            var box = GetNode<AnimatedSprite2D>("box");
            box.Frame = 0;
            box.Play("opening");
        }
    }
    private void OnBodyEntered(Node2D body)
    {
        ChangeState(true, body);
    }
    private void OnBodyExited(Node2D body)
    {
        ChangeState(false, body);

    }

    private void ChangeState(bool state, Node2D body)
    {
        if (body == Global.PlayerLoader.GetBody())
        {
            buttonIndicator.Visible = playerInArea = state;
        }
    }
}