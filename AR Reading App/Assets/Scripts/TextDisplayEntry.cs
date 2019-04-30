using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDisplayEntry : TextEntry
{
    public TextDisplayEntry(string name, TextParentEntry parent, string text) : base(name, parent)
    {
        Text = text;
    }

    public string Text
    {
        get; set;
    }
}
