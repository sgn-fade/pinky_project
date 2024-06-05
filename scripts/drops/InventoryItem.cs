using Godot;

namespace projectpinky.scripts.drops;

public class InventoryItem
{
    private string dataType;
    private Texture icon;
    private Texture background;
    private bool isStackable = false;
    private object value = null;

    public InventoryItem(string newDataType, Texture newIcon, Texture newBackground = null)
    {
        dataType = newDataType;
        icon = newIcon;
        background = newBackground;
    }
}