using Godot;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.player;

public partial class Camera : CharacterBody2D
{
    private Node2D focusedObject;
    private PlayerData player;
    private Camera2D camera;
    public override void _Ready()
    {
        player = Global.Player;
        camera = GetNode<Camera2D>("cam");
    }

    public override void _Process(double delta)
    {
        if (focusedObject != null)
        {
            Velocity = (player.GetPosition() + (focusedObject.GlobalPosition - player.GetPosition()) / 4 -
                        GlobalPosition) * 4;
        }
        else
        {
            Velocity = (player.GetPosition() - GlobalPosition) * 4;
        }
        MoveAndSlide();
        UpdateCamera();
    }

    private void UpdateCamera()
    {
        if (Input.IsActionJustReleased("wheel_up"))
        {
            camera.Zoom = new Vector2(camera.Zoom.X - 0.05f, camera.Zoom.Y - 0.05f);
        }
        if (Input.IsActionJustReleased("wheel_down"))
        {
            camera.Zoom = new Vector2(camera.Zoom.X + 0.05f, camera.Zoom.Y + 0.05f);
        }
    }

    public void SetView(Node2D obj)
    {
        if (focusedObject != null)
        {
            focusedObject.Call("set_focused", false);
            focusedObject = null;
            return;
        }
        if (obj != null)
        {
            obj.Call("set_focused", true);
            focusedObject = obj;
        }
    }
}