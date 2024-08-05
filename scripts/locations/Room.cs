using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using projectpinky.scripts.player;

public partial class Room : Node2D
{
    [Signal]
    public delegate void ChangeRoomEventHandler();

    [Export]private Marker2D _playerSpawnPosition;

    public Vector2 GetSpawnPosition() => _playerSpawnPosition.Position;

    private void OnExitEntered(Node2D body)
    {
        if (body is Player) EmitSignal(SignalName.ChangeRoom);
    }
}
