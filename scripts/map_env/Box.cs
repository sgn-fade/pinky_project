using Godot;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.map_env;

public partial class Box : StaticBody2D
{
    [Export] private Sprite2D buttonIndicator;

    public void Interact()
    {
        var box = GetNode<AnimatedSprite2D>("box");
        box.Frame = 0;
        box.Play("opening");
    }

    public void PlayerEntered()
    {
        buttonIndicator.Visible = true;
    }

    public void PlayerExit()
    {
        buttonIndicator.Visible = false;
    }
}