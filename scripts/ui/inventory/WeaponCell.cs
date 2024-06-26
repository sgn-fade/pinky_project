using Godot;
using projectpinky.scripts.Globals;
using projectpinky.scripts.weapons;

namespace projectpinky.scripts.ui.inventory;

public partial class WeaponCell : InventoryCell
{
    [Signal]
    public delegate void ButtonPressedEventHandler();


    public override void _Ready()
    {
        SlotType = "weapon";
    }

    public new void SetObject(InventorySlotObject newObject)
    {
        if (newObject.DataType == "weapon")
        {
            Object = newObject;
            Empty = false;
            //todo event bus
            //EventBus.EmitSignal("switch_hands_stance", newObject.Data);
            Global.Player.SetWeapon((Weapon)newObject.Data);
            //todo event bus
            //EmitSignal("SetWeaponToUI", newObject.Data);
        }
    }

    public void _OnCellAreaEntered(Area2D area)
    {
        if (area.Name == "object" && area.GetParent<InventorySlotObject>().DataType == "weapon")
        {
            area.GetParent<InventorySlotObject>().SetTargetCell(this);
        }
    }

    public new void Clear()
    {
        Empty = true;
        Object = null;
        //todo event bus
        //EventBus.EmitSignal("switch_hands_stance", null);
        Global.Player.SetWeapon(null);
    }

    public void _OnTextureButtonPressed()
    {
        EmitSignal("ButtonPressed");
    }
}