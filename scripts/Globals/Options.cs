using System.Collections.Generic;
using Godot;

namespace projectpinky.scripts.Globals;

public partial class Options : Node
{
    public static readonly Dictionary<int, string> ButtonsBinds = new()
    {
        { 0, "X" },
        { 1, "Q" },
        { 2, "R" },
        { 3, "C" },
    };
}