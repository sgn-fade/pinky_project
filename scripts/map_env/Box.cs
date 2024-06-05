using Godot;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.map_env;

public partial class Box : StaticBody2D
{
    private bool playerInArea;

    private Sprite2D buttonIndicator;

    public override void _Ready()
    {
        buttonIndicator = GetNode<Sprite2D>("Button_picture");
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("E"))
        {
            SetProcess(false);
            var box = GetNode<AnimatedSprite2D>("box");
            box.Frame = 0;
            box.Play("opening");
        }
    }
    //todo change to public if didnt work
    private void OnBodyEntered(Node2D body)
    {
        ChangeState(false, body);
    }
    private void OnBodyExited(Node2D body)
    {
        ChangeState(false, body);

    }

    private void ChangeState(bool state, Node2D body)
    {
        if (body == Global.Player.GetBody())
        {
            buttonIndicator.Visible = playerInArea = state;
        }
    }
}