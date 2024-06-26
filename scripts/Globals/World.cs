using System.Collections.Generic;
using Godot;
using projectpinky.scripts.ui;

namespace projectpinky.scripts.Globals;

public partial class World : Node
{
    private Node world;
    private PackedScene playerCameraScene;
    private Node cameraScene;
    private List<Node> enemies = new();

    public override void _Ready()
    {
        world = GetNode("/root/World");
        var ui = GetNode<UiCore>("/root/World/Ui");
        playerCameraScene = (PackedScene)ResourceLoader.Load("res://scenes/ui/camera_movement.tscn");
        cameraScene = playerCameraScene.Instantiate();
        world.AddChild(cameraScene);

    }

    // public void FocusCamera()
    // {
    //     cameraScene.SetView(GetCloserEnemy());
    // }

    public Node GetWorld()
    {
        return world.GetNode("location");
    }

    public void AddEnemy(Node scene)
    {
        enemies.Add(scene);
    }

    public void AddEntity(PackedScene entity)
    {
        world.AddChild(entity.Instantiate());
    }
    // private Node GetCloserEnemy()
    // {
    //     Node closerEnemy = null;
    //     float closerDistance = float.PositiveInfinity;
    //     foreach (Node enemy in enemies)
    //     {
    //         float distance = (Player.GlobalPosition - enemy.GlobalPosition).Length();
    //         if (distance < closerDistance)
    //         {
    //             closerEnemy = enemy;
    //             closerDistance = distance;
    //         }
    //     }
    //     return closerEnemy;
    // }
}