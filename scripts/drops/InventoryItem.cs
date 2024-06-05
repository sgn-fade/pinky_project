using Godot;

namespace projectpinky.scripts.drops;

public class InventoryItem
{
    public string DataType { get; set; }
    public object Data { get; set; }
    public Texture2D Icon { get; set; }
    public Texture2D Background { get; set; }
    private bool isStackable = false;
    private object value = null;

    public InventoryItem(object data, string newDataType, Texture2D newIcon, Texture2D newBackground = null)
    {
        Data = data;
        DataType = newDataType;
        Icon = newIcon;
        Background = newBackground;
    }
}