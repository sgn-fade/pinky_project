using System.Collections.Generic;
using Godot;
using projectpinky.scripts.drops;
using projectpinky.scripts.Globals;
using projectpinky.scripts.ui.inventory;
using projectpinky.scripts.weapons;
using Control = Godot.Control;

namespace projectpinky.scripts.ui;

public partial class PlayerInventory : Control
{
    [Export] private PackedScene spellCell;
    [Export] private PackedScene inventoryObject;


    public override void _Ready()
    {
        VisibilityChanged += FillCells;
        WeaponCell.weaponChanged += OnWeaponChanged;
    }

    public override void _ExitTree()
    {
        WeaponCell.weaponChanged -= OnWeaponChanged;
    }

    private void FillCells()
    {
        var playerInventory = Global.PlayerLoader.playerInventory;
        var cellsParent = GetNode<GridContainer>($"item_grid/Grid");

        for (int i = 0; i < playerInventory.Count; i++)
        {
            var cell = cellsParent.GetChild<InventoryCell>(i);

            if (cell.Empty)
            {
                var obj = inventoryObject.Instantiate<InventorySlotObject>();
                var item = playerInventory[i];
                obj.SetData(item);
                cell.SetObject(obj);
            }
        }
    }

    private void OnShowWeaponPressed()
    {
        GetNode<Control>("default_left_side").Visible = false;
        GetNode<Control>("weapon").Visible = true;
    }

    private void OnWeaponBack()
    {
        GetNode<Control>("default_left_side").Visible = true;
        GetNode<Control>("weapon").Visible = false;
    }
    private void FillSpellCells(Weapon weapon)
    {
        var childrenCells = GetNode("weapon/cells").GetChildren();

        foreach (var cell in weapon.activeCells)
        {
            var childrenCell = (SpellCell)childrenCells[cell.Index];

            if (cell.Spell != null)
            {
                var slotObject = inventoryObject.Instantiate<InventorySlotObject>();
                slotObject.SetData(cell.Spell);
                childrenCell.RestoreObject(slotObject);
                //EventBus.EmitSignal("set_spell_icon_to_game", cells[i].Module, cells[i].Button);
            }
        }
    }
    private void RemoveAllCells()
    {
        foreach (var child in GetNode<Control>($"weapon/cells").GetChildren())
        {
            if(child is SpellCell spellSlot) spellSlot.Clear();
        }
    }

    private void OnWeaponChanged(Weapon weapon)
    {
        RemoveAllCells();

        if (weapon != null)
        {
            FillSpellCells(weapon);
        }
    }


    private void _OnBackArrowPressed()
    {
        GetNode<Node2D>("/root/Main/$weapon").Visible = false;
        HideObjects("spell");
        GetNode<Node2D>("/root/Main/$default_right_side").Visible = true;
    }

    private void HideObjects(string type)
    {
        foreach (var node in GetNode<Node2D>("/root/Main/$item_grid/items").GetChildren())
        {
            ((Node2D)node).Visible = (string)node.Get("current_cell.slot_type") != type;
        }
    }
}