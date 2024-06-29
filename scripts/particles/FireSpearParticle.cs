using Godot;
using System;
using projectpinky.scripts.Enemies;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.particles;

public partial class FireSpearParticle : SpellController
{
    [Export] public int Speed = 300;


    public override void _Ready()
    {

        Vector2 endPosition = GetGlobalMousePosition();
        GlobalPosition = Global.Player.GetPosition();
        Velocity = (endPosition - GlobalPosition).Normalized() * Speed;

        LookAt(GetGlobalMousePosition());
    }
    
    public override void _Process(double delta)
    {
        MoveAndSlide();
    }

    protected override void Delete() => QueueFree();

    private void OnBodyEntered(Node2D body)
    {
        if (body is Enemy enemy)
        {
            enemy.TakeDamage(Damage);
        }
        Delete();
    }
}