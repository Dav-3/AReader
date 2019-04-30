using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A menu entry which leads to the text view
/// </summary>
public class TextDisplayEntry : TextEntry
{
    /// <summary>
    /// Creates a new menu entry which leads to text
    /// </summary>
    /// <param name="name">The menu entry's label</param>
    /// <param name="parent">The parent menu entry</param>
    /// <param name="text">The text which will be displayed in the text view if the user clicks on this menu entry</param>
    public TextDisplayEntry(string name, TextParentEntry parent, string text) : base(name, parent)
    {
        Text = text;
    }

    /// <summary>
    /// The text which will be displayed in the text view if the user clicks on this menu entry
    /// </summary>
    public string Text
    {
        get; set;
    }
}
