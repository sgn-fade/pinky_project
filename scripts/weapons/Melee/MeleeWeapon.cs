using System;
using Godot;
using projectpinky.scripts.spells;

namespace projectpinky.scripts.weapons.melee;

public class MeleeWeapon : Weapon
{
    public int ComboCount { get; set; }

    public MeleeWeapon()
    {
        Type = Types.Melee;
    }
}