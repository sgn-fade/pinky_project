using System.Threading.Tasks;
using Godot;
using Godot.Collections;
using projectpinky.scripts.Globals;
using projectpinky.scripts.particles;
using projectpinky.scripts.player;

namespace projectpinky.scripts.hands;

public partial class ShotgunHands : Hands
{
    public enum Animations
    {
        Shoot,
    }

    public override void _Process(double delta)
    {
        LookAt(GetGlobalMousePosition());
    }

}