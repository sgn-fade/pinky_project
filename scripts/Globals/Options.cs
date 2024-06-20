using System.Collections.Generic;
using Godot;

namespace projectpinky.scripts.Globals;

public partial class Options : Node
{
    public static readonly Dictionary<int, string> ButtonsBinds = new()
    {
        { 0, "X" },
        { 1, "Q" },
        { 2, "LMB" },
        { 3, "RMB" },
        { 4, "R" },
        { 5, "C" }
    };
}