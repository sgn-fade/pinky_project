using System.Collections.Generic;
using Godot;
using Godot.Collections;
using projectpinky.scripts.drops;
using projectpinky.scripts.spells;
using projectpinky.scripts.ui.inventory;
using Control = Godot.Control;

namespace projectpinky.scripts.ui;

public partial class PlayerInventory : Control
{
    private PackedScene spellSlotButtonScene = GD.Load<PackedScene>("res://scenes/ui/spell_slot_button.tscn");
    private PackedScene weaponCardScene = GD.Load<PackedScene>("res://scenes/ui/weapon_card.tscn");
    private PackedScene emptyCell = GD.Load<PackedScene>("res://scenes/ui/inventory/module_cell.tscn");
    private PackedScene inventoryObject = GD.Load<PackedScene>("res://scenes/ui/inventory/inventory_slot_object.tscn");

    private PackedScene inventoryCell = GD.Load<PackedScene>("res://scenes/ui/inventory/inventory_cell.tscn");

    //
    // private Node cells;
    //

    public override void _Ready()
    {
        CreateInventoryCells();
        InventoryItem item = new FireEyeSpell().InvItem;
        AddItem(item);
        //var wand = GD.Load<PackedScene>("res://scripts/weapons/magic_weapons/old_goblins_magic_wand.gd");
        //var potion = GD.Load<PackedScene>("res://scripts/drops/potion.gd");
    }

    private void CreateInventoryCells()
    {
        for (float y = 199.5f; y < 992; y += 132)
        {
            for (float x = 1042.5f; x < 1900; x += 132)
            {
                var cell = inventoryCell.Instantiate<InventoryCell>();
                GetNode<Control>($"item_grid/cells").AddChild(cell);
                cell.GlobalPosition = new Vector2(x, y);
            }
        }
    }

    // private void FillCells()
    // {
    //     cells = Player.GetWeapon().GetCells();
    //     for (int i = 0; i < cells.Count; i++)
    //     {
    //         var currentCell = (Node2D)emptyCell.Instance();
    //         GetNode<Node>($"/root/Main/$weapon/cells").AddChild(currentCell);
    //         currentCell.Position = cells[i].Position;
    //         currentCell.Set("cell_index", i);
    //
    //         if (cells[i].Module != null)
    //         {
    //             var item = inventoryObject.Instantiate<Node2D>();
    //             item.Call("set_data", cells[i].Module.InventoryItem);
    //             item.Position = currentCell.Position;
    //             GetNode<Node>($"/root/Main/$item_grid/items").AddChild(item);
    //             item.Call("set_cell", currentCell);
    //             item.Visible = false;
    //             currentCell.Call("restore_object", item);
    //             //EventBus.EmitSignal("set_spell_icon_to_game", cells[i].Module, cells[i].Button);
    //         }
    //     }
    // }

    private void RemoveAllCells()
    {
        //EventBus.EmitSignal("clear_spell_icons");
        foreach (Node child in GetNode<Control>($"weapon/cells").GetChildren())
        {
            child.QueueFree();
        }

        foreach (Node child in GetNode<Control>($"item_grid/items").GetChildren())
        {
            if ((string)child.Get("current_cell.slot_type") == "spell")
            {
                child.QueueFree();
            }
        }
    }

    private void AddItem(InventoryItem item)
    {
        foreach (var node in GetNode<Control>($"item_grid/cells").GetChildren())
        {
            var cell = (InventoryCell)node;
            if (cell.IsEmpty())
            {
                var obj = inventoryObject.Instantiate<InventoryCellObject>();
                obj.SetData(item);
                GetNode<Control>($"item_grid/items").AddChild(obj);
                obj.SetCell(cell);
                cell.SetObject(obj);
                return;
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
            var child = (Node2D)node;
            if ((string)child.Get("current_cell.slot_type") == type)
            {
                child.Visible = false;
            }
            else
            {
                child.Visible = true;
            }
        }
    }
}