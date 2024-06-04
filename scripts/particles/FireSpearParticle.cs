using Godot;
using System;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.particles;

public partial class Fireball : CharacterBody2D
{
    [Export] public int Speed = 300;

    [Export] public NodePath AreaPath;
    private Area2D _area;
    private Vector2 _velocity;

    public override void _Ready()
    {
        _area = GetNode<Area2D>(AreaPath);
        _area.Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));

        Vector2 endPosition = GetGlobalMousePosition();
        GlobalPosition = Global.Player.GetPosition();
        _velocity = endPosition - GlobalPosition;

        LookAt(GetGlobalMousePosition());
    }
    
    public override void _Process(double delta)
    {
        Velocity = _velocity.Normalized() * Speed;
        MoveAndSlide();
    }

    private void Delete() => QueueFree();

    private void OnBodyEntered(Node body)
    {
        Global.EventBus.EmitSignal("damage_to_enemy", body, 10);
        Global.EventBus.EmitSignal("push_away_enemy", body, _velocity);
        Delete();
    }
}