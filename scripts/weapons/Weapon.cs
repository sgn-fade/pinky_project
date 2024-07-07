using System.Collections.Generic;
using Godot;
using projectpinky.scripts.drops;
using projectpinky.scripts.Globals;
using projectpinky.scripts.spells;

namespace projectpinky.scripts.weapons;

public class Weapon
{
    public WeaponData WeaponData { get; set; }
    private Cell[] activeCells = new Cell[6];


	public Cell[] GetCells()
	{
		return activeCells;
	}


    public Weapon(WeaponData data)
    {
        WeaponData = data;
		for (var i = 0; i < 4; i++)
		{
			var index = GD.Randi() % activeCells.Length;
			activeCells[index] ??= new Cell();
		}
	}


    public void AddSpell(Spell spell, int cellIndex)
    {
        activeCells[cellIndex].Spell = spell;
    }

    public void RemoveSpell(int cellIndex)
    {
        activeCells[cellIndex].Spell = null;
    }

    private void SwapSpells(int firstSlot, int secondSlot)
    {
        (activeCells[firstSlot].Spell, activeCells[secondSlot].Spell) = (activeCells[secondSlot].Spell, activeCells[firstSlot].Spell);
    }


    public class Cell
    {
        public int Index { get; set; }
        public Spell Spell{ get; set; }
    }
}
