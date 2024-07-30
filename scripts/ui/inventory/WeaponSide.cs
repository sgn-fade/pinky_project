using Godot;
using System;
using projectpinky.scripts.ui.inventory;

public partial class WeaponSide : Control
{
    private void OnShowWeaponPressed()
    {
        Visible = true;
        CellsSwitch(true);
    }

    private void OnWeaponBack()
    {
        Visible = false;
        CellsSwitch(false);
    }

    private void CellsSwitch(bool state)
    {
        var childrenCells = GetNode("cells").GetChildren();

        foreach (var cell in childrenCells)
        {
            if (cell is SpellCell spellCell)
            {
                spellCell.SwitchMonitoring(state);
            }
        }
    }
}
