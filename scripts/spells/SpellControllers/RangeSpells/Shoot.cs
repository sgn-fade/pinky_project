using Godot;
using projectpinky.scripts.Globals;
using projectpinky.scripts.hands;
using projectpinky.scripts.particles;
using projectpinky.scripts.player;

namespace projectpinky.scripts.spells.SpellControllers.RangeSpells;

public partial class Shoot : SpellController
{
    [Export] private PackedScene bullet;

    public override void _Ready()
    {
        for (int i = 0; i < 1; i++)
        {
            var bulletInstance = bullet.Instantiate<Bullet>();
            bulletInstance.GlobalPosition = GlobalPosition;
            Global.World.GetWorld().AddChild(bulletInstance);
        }
    }
    protected override void Delete()
    {
        QueueFree();
    }

    public override string GetAnim()
    {
        return ShotgunHands.Animations.Shoot.ToString();
    }
}