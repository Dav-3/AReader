using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuEntry : MonoBehaviour
{
    [SerializeField] private ApplicationController appController;

    public TextEntry TextEntry { get; set; }
    public bool IsDisplayElement
    {
        get
        {
            return TextEntry.GetType() == typeof(TextDisplayEntry);
        }
    }

    public void OnClick()
    {
        if (TextEntry != null)
        {
            if (IsDisplayElement)
            {
                appController.ShowText((TextDisplayEntry)TextEntry);
            }
            else
            {
                appController.ShowMenu((TextParentEntry)TextEntry);
            }
        }
    }
}
