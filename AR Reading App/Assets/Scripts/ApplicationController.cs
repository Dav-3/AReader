using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplicationController : MonoBehaviour
{
    [SerializeField] Transform mainMenu;
    [SerializeField] Transform textScroll;
    [SerializeField] Text textDisplay;
    [SerializeField] GameObject buttonTemplate;
    [SerializeField] Button backButtonMenu;

    private TextEntry currentEntry;
    private ScrollRect scroll;


    private void Awake()
    {
        GenerateEntries();
        CreateMenu();
        scroll = textScroll.GetComponentInChildren<ScrollRect>(true);
    }

    private void CreateMenu()
    {
        mainMenu.gameObject.SetActive(true);
        textScroll.gameObject.SetActive(false);
        foreach(Transform child in buttonTemplate.transform.parent)
        {
            if (child != buttonTemplate.transform)
            {
                Destroy(child.gameObject);
            }
        }

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

    private void CreateText()
    {
        mainMenu.gameObject.SetActive(false);
        textScroll.gameObject.SetActive(true);
        textDisplay.text = ((TextDisplayEntry)currentEntry).Text;
        scroll.verticalNormalizedPosition = 1;
    }

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

    public void ShowText(TextDisplayEntry entry)
    {
        currentEntry = entry;
        CreateText();
    }

    public void Back()
    {
        if (currentEntry.Parent != null)
        {
            ShowMenu(currentEntry.Parent);
        }
    }

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

    private string LoadText(string path)
    {
        TextAsset txt = (TextAsset)Resources.Load(path, typeof(TextAsset));
        return txt.text;
    }
}
