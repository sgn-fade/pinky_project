using System;
using Godot;
using projectpinky.scripts.spells;
using projectpinky.scripts.spells.melee_spells;

namespace projectpinky.scripts.weapons.melee;

public partial class MeleeWeapon : Weapon
{
    public int ComboCount { get; set; }

    public override void _Ready()
    {
        base._Ready();
        Type = "melee";
        AddBaseSpell(new SwordHitBase());
    }
}