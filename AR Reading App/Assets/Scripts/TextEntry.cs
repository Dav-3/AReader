using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A menu entry
/// </summary>
public abstract class TextEntry
{
    /// <summary>
    /// Creates a new menu entry without a parent
    /// </summary>
    /// <param name="name">The label of the menu entry</param>
    public TextEntry(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Creates a new menu entry
    /// </summary>
    /// <param name="name">The label of the menu entry</param>
    /// <param name="parent">The parent of the menu entry</param>
    public TextEntry(string name, TextParentEntry parent) : this(name)
    {
        Parent = parent;
    }

    /// <summary>
    /// The label of hte menu entry
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The parent menu entry of this entry
    /// </summary>
    public TextParentEntry Parent { get; private set; }
}
