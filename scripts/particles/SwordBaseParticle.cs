using Godot;
using System.Threading.Tasks;
using projectpinky.scripts.Enemies;
using projectpinky.scripts.Globals;

public partial class SwordBaseParticle : Node2D
{
    public override void _Ready()
    {
        LookAt(GetGlobalMousePosition());
    }

    private void OnArea2DBodyEntered(Node2D body)
    {
        if (body is Enemy enemy)
        {
            enemy.TakeDamage(10);
        }
    }
}