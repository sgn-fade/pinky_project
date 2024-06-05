using Godot;

namespace projectpinky.scripts.Globals;

public partial class Global : Node
{
    public static PlayerData Player{get; private set; }
    public static EventBus EventBus{get;private set; }
    public static Options Options{get;private set; }
    public static GlobalWorldInfo GlobalWorldInfo{get;private set; }

    public override void _Ready()
    {
        GlobalWorldInfo = GetNode<GlobalWorldInfo>("/root/GlobalWorldInfo");
        Player = GetNode<PlayerData>("/root/PlayerData");
        EventBus = GetNode<EventBus>("/root/EventBus");
        Options = GetNode<Options>("/root/Options");

    }
}