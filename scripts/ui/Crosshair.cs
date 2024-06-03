using Godot;

namespace projectpinky.scripts.ui;

public partial class Crosshair : CharacterBody2D
{
    public override void _Process(double delta)
    {
        GlobalPosition = GetGlobalMousePosition();
    }

    public void ChangeCrosshair(string type)
    {
        var part = GetNode<CpuParticles2D>("part");
        var sprite = GetNode<AnimatedSprite2D>("Sprite2D");

        part.Emitting = type == "ui";

        sprite.Play(type);
    }
}