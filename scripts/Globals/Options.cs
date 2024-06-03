using System.Collections.Generic;
using Godot;

namespace projectpinky.scripts.Globals;

public class Options : Node
{
    public static Dictionary<string, string> ButtonsBinds = new()
    {
        { "slot1", "X" },
        { "slot2", "Q" },
        { "slot3", "mouse_left_button" },
        { "slot4", "mouse_right_button" },
        { "slot5", "R" },
        { "slot6", "C" }
    };
}