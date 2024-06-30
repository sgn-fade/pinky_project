using System.Collections.Generic;
using Godot;
using projectpinky.scripts.Globals;
using projectpinky.scripts.spells;
using projectpinky.scripts.weapons;

namespace projectpinky.scripts.hands;

public abstract partial class Hands : Node2D
{
    private Weapon _playerWeapon;

    public Dictionary<string, Spell> spellsButtons = new();
    public abstract void PlayAnimation(string animationName);

    public override void _Ready()
    {
        foreach (var button in Options.ButtonsBinds)
        {
            spellsButtons.Add(button.Value, null);
        }

        _playerWeapon = Global.Player.GetWeapon();
    }

    public override void _Input(InputEvent @event)
    {
        foreach (var kvp in spellsButtons)
        {
            if (Input.IsActionJustPressed(kvp.Key) && kvp.Value != null)
            {
                kvp.Value.Cast();
                return;
            }
        }
    }
}