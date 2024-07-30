using Godot;
using projectpinky.scripts.drops;
using projectpinky.scripts.Globals;
using projectpinky.scripts.spells;
using projectpinky.scripts.weapons;

namespace projectpinky.scripts.ui.inventory;

public partial class WeaponCell : InventoryCell
{
	public delegate void WeaponChanged(Weapon weapon);

	public static event WeaponChanged weaponChanged;

	public override void _Ready()
	{
		SlotType = InventoryItem.DataTypes.Weapon;
	}

	public override void _EnterTree()
	{
		SpellCell.spellChanged += OnSpellChanged;
	}

	public override void _ExitTree()
	{
	   SpellCell.spellChanged -= OnSpellChanged;
	}

	private void OnSpellChanged(Spell spell, int cellIndex)
	{
		((Weapon)Object.Data).SetSpell(spell, cellIndex);
	}

	public override void SetObject(InventorySlotObject newObject)
	{
		base.SetObject(newObject);
		weaponChanged?.Invoke((Weapon)Object.Data);
	}


	public override void Clear()
	{
		base.Clear();
		weaponChanged?.Invoke(null);
	}
}
