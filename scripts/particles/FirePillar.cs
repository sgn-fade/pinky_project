using Godot;
using System.Threading.Tasks;
using projectpinky.scripts.Globals;
namespace projectpinky.scripts.particles;
public partial class FirePillar : CharacterBody2D
{


    public override void _Ready()
    {
        GlobalPosition = GetGlobalMousePosition();
    }

    private void OnDamageAreaBodyEntered(Node body)
    {
        //todo event bus
        Global.EventBus.EmitSignal("damage_to_enemy", body, 8, "burn");
    }

    private void OnPullsTgAreaBodyEntered(Node body)
    {
        Global.EventBus.EmitSignal("pulls_body", body, GlobalPosition);
    }
}
