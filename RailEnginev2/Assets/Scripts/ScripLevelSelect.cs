using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class ScripLevelSelect : MonoBehaviour 
{
    private Canvas[] levelPages;
    private Button[] buttons;
    private TextAsset[] levelText;

    public static Button[] levelButton = new Button[8];

    public Button nextPage;
    public Button prevPage;

	void Awake () 
    {
        levelPages = FindObjectsOfType<Canvas>();
        buttons = FindObjectsOfType<Button>();

        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].tag == "LevelButton" && i < levelButton.GetLength(0))
            {
                levelButton[i] = buttons[i];
                Debug.Log(levelButton.Length);
            }
        }

        if (levelPages.Length > 1)
        {
            Debug.Log("Wat?");
        }
        else
        {
            prevPage.interactable = false;
            nextPage.interactable = false;
        }

        for (int i = 0; i > levelButton.Length; i++)
        {
            if (levelButton[i] == null)
            {
                levelButton[i].interactable = false;
            }
        }
	}
}
