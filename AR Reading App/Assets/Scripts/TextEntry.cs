using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TextEntry
{
    public TextEntry(string name)
    {
        Name = name;
    }

    public TextEntry(string name, TextParentEntry parent) : this(name)
    {
        Parent = parent;
    }

    public string Name { get; set; }

    public TextParentEntry Parent { get; private set; }
}
