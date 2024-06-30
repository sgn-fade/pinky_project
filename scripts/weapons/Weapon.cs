using System.Collections.Generic;
using Godot;
using projectpinky.scripts.drops;
using projectpinky.scripts.Globals;
using projectpinky.scripts.spells;

namespace projectpinky.scripts.weapons;

public class Weapon
{
    public WeaponData WeaponData { get; set; }
    private Cell[] cells = new Cell[8];


	public Cell[] GetCells()
	{
		return cells;
	}


    public Weapon(WeaponData data)
    {
        WeaponData = data;
		for (var i = 0; i < 4; i++)
		{
			var index = GD.Randi() % cells.Length;
			cells[index] ??= new Cell();
		}
	}


    public void AddSpell(Spell spell, int cellIndex)
    {
        cells[cellIndex].Spell = spell;
    }

    public void RemoveSpell(int cellIndex)
    {
        cells[cellIndex].Spell = null;
    }

    private void SwapSpells(int firstSlot, int secondSlot)
    {
        (cells[firstSlot].Spell, cells[secondSlot].Spell) = (cells[secondSlot].Spell, cells[firstSlot].Spell);
    }


    public class Cell
    {
        public int Index { get; set; }
        public Spell Spell{ get; set; }
    }
}
