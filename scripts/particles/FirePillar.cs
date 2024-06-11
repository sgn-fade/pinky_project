using Godot;
using System.Threading.Tasks;
using projectpinky.scripts.Enemies;
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
        if (body is Enemy enemy)
        {
            //todo enemy take damage
        }
    }
}
