using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the application flow
/// Determines if menu or text should be shown and populates the menu with the correct entries
/// </summary>
public class ApplicationController : MonoBehaviour
{
    [SerializeField] Transform mainMenu;
    [SerializeField] Transform textScroll;
    [SerializeField] Text textDisplay;
    [SerializeField] GameObject buttonTemplate;
    [SerializeField] Button backButtonMenu;

    private TextEntry currentEntry;
    private ScrollRect scroll;

    /// <summary>
    /// Initialization: Generates the entries and the initial menu
    /// </summary>
    private void Awake()
    {
        GenerateEntries();
        CreateMenu();
        scroll = textScroll.GetComponentInChildren<ScrollRect>(true);
    }

    /// <summary>
    /// Creates the menu using the subentries of currentEntry as menu items
    /// </summary>
    private void CreateMenu()
    {
        // make sure that the menu is shown and the text view is hidden
        mainMenu.gameObject.SetActive(true);
        textScroll.gameObject.SetActive(false);
        // delete all existing buttons
        foreach(Transform child in buttonTemplate.transform.parent)
        {
            if (child != buttonTemplate.transform)
            {
                Destroy(child.gameObject);
            }
        }

        // create new menu items: one item for each subentry of currentEntry
        foreach (TextEntry entry in ((TextParentEntry)currentEntry).Subentries)
        {
            GameObject buttonObj = Instantiate(buttonTemplate, buttonTemplate.transform.parent);
            buttonObj.SetActive(true);
            Button button = buttonObj.GetComponent<Button>();
            Text buttonText = buttonObj.GetComponentInChildren<Text>();
            buttonText.text = entry.Name;
            MenuEntry menuEntry = buttonObj.GetComponent<MenuEntry>();
            menuEntry.TextEntry = entry;
        }
    }

    /// <summary>
    /// Sets up the text view so that the user can read a selected text
    /// </summary>
    private void CreateText()
    {
        mainMenu.gameObject.SetActive(false);
        textScroll.gameObject.SetActive(true);
        textDisplay.text = ((TextDisplayEntry)currentEntry).Text;
        scroll.verticalNormalizedPosition = 1;
    }

    /// <summary>
    /// Shows the menu and uses entry as the parent menu entry
    /// </summary>
    /// <param name="entry">The menu entry which will be used to display the menu</param>
    public void ShowMenu(TextParentEntry entry)
    {
        currentEntry = entry;
        if (currentEntry.Parent == null)
        {
            backButtonMenu.gameObject.SetActive(false);
        }
        else
        {
            backButtonMenu.gameObject.SetActive(true);
        }
        CreateMenu();
    }

    /// <summary>
    /// Shows the text which is stored in the given menu entry
    /// </summary>
    /// <param name="entry">The text entry determines the text content</param>
    public void ShowText(TextDisplayEntry entry)
    {
        currentEntry = entry;
        CreateText();
    }

    /// <summary>
    /// Moves back to the previous menu
    /// This is stored in the parent property of the current menu entry
    /// Also works if text is currently shown
    /// </summary>
    public void Back()
    {
        if (currentEntry.Parent != null)
        {
            ShowMenu(currentEntry.Parent);
        }
    }

    /// <summary>
    /// Generates the menu hierarchy and load the texts
    /// </summary>
    private void GenerateEntries()
    {
        TextParentEntry root = new TextParentEntry("Root", null);
        TextParentEntry macbeth = new TextParentEntry("Macbeth", root);
        root.Subentries.Add(macbeth);
        macbeth.Subentries.Add(new TextDisplayEntry("Act 1, Scene 1", macbeth, LoadText("Macbeth 1-1")));
        macbeth.Subentries.Add(new TextDisplayEntry("Act 1, Scene 2", macbeth, LoadText("Macbeth 1-2")));

        TextParentEntry hamlet = new TextParentEntry("Hamlet", root);
        root.Subentries.Add(hamlet);
        hamlet.Subentries.Add(new TextDisplayEntry("Act 1, Scene 1", hamlet, LoadText("Hamlet 1-1")));

        currentEntry = root;
    }

    /// <summary>
    /// Loads the text from a file in Unity's resources folder
    /// </summary>
    /// <param name="path">The relative path to the file (starting in the resources folder)</param>
    /// <returns>The loaded text from the given file</returns>
    private string LoadText(string path)
    {
        TextAsset txt = (TextAsset)Resources.Load(path, typeof(TextAsset));
        return txt.text;
    }
}
