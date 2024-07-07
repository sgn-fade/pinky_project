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
    [Export] PackedScene weaponCardScene = GD.Load<PackedScene>("res://scenes/ui/weapon_card.tscn");
    [Export] private PackedScene spellCell;
    [Export] private PackedScene inventoryObject;

    private Weapon.Cell[] cells;

    public override void _Ready()
    {
        VisibilityChanged += FillCells;
        //var wand = GD.Load<PackedScene>("res://scripts/weapons/magic_weapons/old_goblins_magic_wand.gd");
        //var potion = GD.Load<PackedScene>("res://scripts/drops/potion.gd");
    }

    private void FillCells()
    {
        var playerInventory = Global.Player.playerInventory;
        var cellsParent = GetNode<GridContainer>($"item_grid/cells/Grid");

        for (int i = 0; i < playerInventory.Count; i++)
        {
            var cell = cellsParent.GetChild<InventoryCell>(i);

            if (cell.Empty)
            {
                var obj = inventoryObject.Instantiate<InventorySlotObject>();
                var item = playerInventory[i];
                obj.SetData(item);
                cell.AddChild(obj);
                obj.SetCell(cell);
                cell.SetObject(obj);
            }
        }
    }

    private void FIllSpellCells()
    {
        //cells = Global.Player.GetWeapon().GetCells();
        var childrenCells = GetNode("weapon/cell").GetChildren();

        foreach (var cell in cells)
        {
            var childrenCell = (SpellCell)childrenCells[cell.Index];
            childrenCell.Visible = true;

            if (cell.Spell != null)
            {
                var item = inventoryObject.Instantiate<InventorySlotObject>();
                item.SetData(cell.Spell.Data);
                item.Position = childrenCell.Position;
                GetNode<Node>($"/root/Main/$item_grid/items").AddChild(item);
                item.SetCell(childrenCell);
                item.Visible = false;
                childrenCell.RestoreObject(item);
                //EventBus.EmitSignal("set_spell_icon_to_game", cells[i].Module, cells[i].Button);
            }
        }
    }
    private void RemoveAllCells()
    {
        //EventBus.EmitSignal("clear_spell_icons");
        foreach (var child in GetNode<Control>($"weapon/cells").GetChildren())
        {
            child.QueueFree();
        }

        foreach (var child in GetNode<Control>($"item_grid/items").GetChildren())
        {
            if ((string)child.Get("current_cell.slot_type") == "spell")
            {
                child.QueueFree();
            }
        }
    }

    private void _OnWeaponCellButtonPressed()
    {
        GetNode<Node2D>("/root/Main/$default_right_side").Visible = false;
        HideObjects("weapon");
        GetNode<Node2D>("/root/Main/$weapon").Visible = true;
    }

    private void _OnWeaponCellSetWeaponToUI(Node weapon)
    {
        //GetNode<Node>("/root/Main/$weapon/weapon_rarity_bg").Call("show_weapon", Player.GetWeapon());
        if (weapon == null)
        {
            RemoveAllCells();
            return;
        }
        //FillCells();
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