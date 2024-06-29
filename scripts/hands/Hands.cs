using Godot;
using projectpinky.scripts.Globals;
using projectpinky.scripts.weapons;

namespace projectpinky.scripts.hands;

public abstract partial class Hands : Node2D
{
	private Weapon _playerWeapon;
	public abstract void PlayAnimation(string animationName);

	public override void _Ready()
	{
		_playerWeapon = Global.Player.GetWeapon();
	}

	public override void _Input(InputEvent @event)
	{
		foreach (var kvp in _playerWeapon.spellsButtons)
		{
			if (!Input.IsActionJustPressed(kvp.Key)) continue;

			var action = kvp.Value;
			if (action.GetReady()) action.Cast();
			return;
		}
	}
}
