using Godot;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.player;

public partial class Camera : CharacterBody2D
{
    private PlayerData player;
    [Export] private Camera2D camera;

    public override void _Ready()
    {
        player = Global.Player;
    }

    public override void _Process(double delta)
    {
        Velocity = (player.GetPosition() - GlobalPosition) * 4;
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
}