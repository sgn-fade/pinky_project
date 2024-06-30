using Godot;
using System;

public class Skeleton : Undead
{
    private PackedScene skeletDrop = GD.Load<PackedScene>("res://scenes/drops/skeleton_drop.tscn");
    private AnimatedSprite sprite;
    private Area2D body;

    public override void _Ready()
    {
        sprite = GetNode<AnimatedSprite>("body/Sprite2D");
        body = GetNode<Area2D>("body");
        hp = 15;
        hpBar.MaxValue = hp * 10;
        hpBar.Value = hp * 10;
        whiteAnimationBar.MaxValue = hp * 10;
        whiteAnimationBar.Value = hp * 10;
        speed = 35;
        enemyDamage = 4;
        body.Connect("body_entered", this, nameof(SkeletonAttack));
    }

    public override void _Process(float delta)
    {
        if (GlobalPosition.DistanceTo(Player.GetPosition()) < 100)
        {
            SetDirection(Player.GetPosition() - GlobalPosition);
            Move();
            sprite.Play("idle");
        }
        else
        {
            sprite.Play("idle");
        }
    }

    private void SkeletonAttack()
    {
        if (GlobalPosition.DistanceTo(Player.GetPosition()) < 10)
        {
            Vector2 playerOffsetDir = -(GlobalPosition - Player.GetPosition()).Normalized();
            EventBus.EmitSignal("player_take_damage", playerOffsetDir, 4);
        }
    }

    private void SpawnDrop()
    {
        Random random = new Random();
        Node2D modulesDrops = modulesDrop.Instance<Node2D>();
        GlobalWorldInfo.GetWorld3D().AddChild(modulesDrops);
        modulesDrops.GlobalPosition = GlobalPosition;
        modulesDrops.ZIndex = ZIndex;
        for (int i = 0; i < random.Next(1, 5); i++)
        {
            Node2D bones = skeletDrop.Instance<Node2D>();
            GlobalWorldInfo.GetWorld3D().AddChild(bones);
            bones.RotationDegrees = random.Next(0, 180);
            bones.GlobalPosition = GlobalPosition + new Vector2(random.Next(-5, 6), random.Next(-5, 6));
            bones.ZIndex = ZIndex;
        }
    }
}