using Godot;
using projectpinky.scripts.drops;
using projectpinky.scripts.spells;

namespace projectpinky.scripts.weapons;
[GlobalClass]
public partial class Weapon : InventoryItem
{
    public enum Types
    {
        Melee,
        Range,
        Magic
    }
    public Cell[] activeCells = new Cell[4];

    [Export] public Texture2D Texture { get; set; }
    [Export] public Types WeaponType { get; set; } = Types.Melee;
    [Export] public PackedScene HandsScene { get; set; }
    [Export] public int Damage { get; set; }

    public Weapon()
    {
        for (var i = 0; i < 4; i++)
        {
            activeCells[i] ??= new Cell();
        }
    }
    public void SetSpell(Spell spell, int cellIndex)
    {
        activeCells[cellIndex].Spell = spell;
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