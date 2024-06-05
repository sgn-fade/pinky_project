using Godot;
using projectpinky.scripts.Globals;
using projectpinky.scripts.weapons;

namespace projectpinky.scripts.ui.inventory;

public partial class WeaponCell : InventoryCell
{
    // Сигналы
    [Signal]
    public delegate void ButtonPressed();


    public override void _Ready()
    {
        SlotType = "weapon";
    }

    public new void SetObject(InventoryCellObject newObject)
    {
        if (newObject.DataType == "weapon")
        {
            @object = newObject;
            Empty = false;
            //todo event bus
            //EventBus.EmitSignal("switch_hands_stance", newObject.Data);
            Global.Player.SetWeapon((Weapon)newObject.Data);
            //todo event bus
            //EmitSignal("SetWeaponToUI", newObject.Data);
        }
    }

    public new void _OnCellAreaEntered(Area2D area)
    {
        if (area.Name == "object" && area.GetParent<InventoryCellObject>().DataType == "weapon")
        {
            area.GetParent<InventoryCellObject>().SetTargetCell(this);
        }
    }

    public new void Clear()
    {
        Empty = true;
        @object = null;
        //todo event bus
        //EventBus.EmitSignal("switch_hands_stance", null);
        Global.Player.SetWeapon(null);
    }

    public void _OnTextureButtonPressed()
    {
        EmitSignal("ButtonPressed");
    }
}