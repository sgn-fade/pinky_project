using System.Collections.Generic;
using Godot;
using projectpinky.scripts.Globals;
using projectpinky.scripts.spells;
using projectpinky.scripts.weapons;

namespace projectpinky.scripts.hands;

public abstract partial class Hands : Node2D
{
    private Weapon _playerWeapon;

    public Dictionary<string, Spell> spellsButtons;
    public abstract void PlayAnimation(string animationName);

    public override void _Ready()
    {
        spellsButtons = new Dictionary<string, Spell>();
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
            if (!Input.IsActionJustPressed(kvp.Key)) continue;

			var action = kvp.Value;
			if (action.GetReady()) action.Cast();
			return;
		}
	}
}
