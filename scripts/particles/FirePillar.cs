using Godot;
using System.Threading.Tasks;
using projectpinky.scripts.Enemies;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.particles;

public partial class FirePillar : SpellController
{
    public override void _Ready()
    {
        base._Ready();
        GlobalPosition = GetGlobalMousePosition();
    }

    protected override void Delete() => QueueFree();


    private void OnDamageAreaBodyEntered(Node body)
    {
        if (body is Enemy enemy)
        {
            enemy.TakeDamage(Damage);
        }
    }
}