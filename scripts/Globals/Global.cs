using Godot;
using projectpinky.scripts.player;

namespace projectpinky.scripts.Globals;

public partial class Global : Node
{
    public static PlayerLoader PlayerLoader{get; private set; }
    public static EventBus EventBus{get;private set; }
    public static Options Options{get;private set; }
    public static World World{get;private set; }

    public override void _Ready()
    {
        World = GetNode<World>("/root/WorldInfo");
        PlayerLoader = GetNode<PlayerLoader>("/root/PlayerLoader");
        EventBus = GetNode<EventBus>("/root/EventBus");
        Options = GetNode<Options>("/root/Options");
    }
}