using System.Threading.Tasks;
using Godot;
using Godot.Collections;
using projectpinky.scripts.Globals;
using projectpinky.scripts.particles;
using projectpinky.scripts.player;

namespace projectpinky.scripts.hands;

public partial class ShotgunHands : Hands
{
    [Export] private PackedScene _bullet;
    [Export] public int NumberOfBullets{ get; set; }
    public enum Animations
    {
        Shoot,
    }

    public override void _Process(double delta)
    {
        LookAt(GetGlobalMousePosition());
    }

    protected override void LeftClickSpell()
    {
        PlayAnimation(Animations.Shoot.ToString());
    }

    public void Shoot()
    {
        for (int i = 0; i < NumberOfBullets; i++)
        {
            var bulletInstance = _bullet.Instantiate<Bullet>();
            bulletInstance.GlobalPosition = GlobalPosition;
            Global.World.GetWorld().AddChild(bulletInstance);
        }
    }

    protected override void RightClickSpell()
    {
        //TODO hook
    }
}