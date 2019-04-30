using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A menu entry with sub entries
/// </summary>
public class TextParentEntry : TextEntry
{
    /// <summary>
    /// Creates a new menu entry with children
    /// </summary>
    /// <param name="name">The label of the menu entry</param>
    /// <param name="parent">The parent of this menu entry</param>
    public TextParentEntry(string name, TextParentEntry parent) : base(name, parent)
    {
        Subentries = new List<TextEntry>();
    }

    /// <summary>
    /// The sub entries which should be displayed if this menu entry is clicked
    /// </summary>
    public List<TextEntry> Subentries
    {
        get; set;
    }
}
