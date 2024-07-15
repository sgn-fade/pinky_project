using Godot;
using projectpinky.scripts.Globals;
using projectpinky.scripts.hands;
using projectpinky.scripts.particles;
using projectpinky.scripts.player;

namespace projectpinky.scripts.spells.SpellControllers.RangeSpells;

public partial class Shoot : SpellController
{
    [Export] private PackedScene _bullet;
    [Export] public int NumberOfBullets{ get; set; }

    public override void _Ready()
    {
        for (int i = 0; i < NumberOfBullets; i++)
        {
            var bulletInstance = _bullet.Instantiate<Bullet>();
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