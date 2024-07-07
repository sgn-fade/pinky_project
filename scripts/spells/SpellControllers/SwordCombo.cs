using System.Collections.Generic;
using Godot;
using projectpinky.scripts.Enemies;
using projectpinky.scripts.Globals;
using projectpinky.scripts.particles;
using projectpinky.scripts.player;

namespace projectpinky.scripts.spells.SpellControllers;

public partial class SwordCombo : SpellController
{
    protected override void Delete() => QueueFree();
    public override string GetAnim()
    {
        throw new System.NotImplementedException();
    }
}