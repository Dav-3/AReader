using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextParentEntry : TextEntry
{
    public TextParentEntry(string name, TextParentEntry parent) : base(name, parent)
    {
        Subentries = new List<TextEntry>();
    }

    public List<TextEntry> Subentries
    {
        get; set;
    }
}
