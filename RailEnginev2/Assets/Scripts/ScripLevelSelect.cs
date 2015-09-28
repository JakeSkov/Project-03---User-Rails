using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// @author Jake Skov
/// @date 09/25/15
/// </summary>
public class ScripLevelSelect : MonoBehaviour
{
    private Canvas[] levelPages;
    private Button[] buttons;
    private TextAsset[] levelText;


    public Button[] levelButton = new Button[8];

    public Button nextPage;
    public Button prevPage;

    public Text[] levelName = new Text[8];
    public Text[] authorName = new Text[8];
    public Text[] dateText = new Text[8];
    public Image[] levelImage = new Image[8];

    string[] levelFilePaths;
    List<string> levelData = new List<string>();
    string localPath;

    void Awake()
    {
        levelPages = FindObjectsOfType<Canvas>();
        buttons = FindObjectsOfType<Button>();
        
        //Insures that the level buttons are assigned
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].tag == "LevelButton" && i < levelButton.GetLength(0))
            {
                levelButton[i] = buttons[i];
            }
        }

        if (levelPages.Length > 1)
        {
            prevPage.interactable = true;
            nextPage.interactable = true;
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

        //Import
        StreamReader sr;
        levelFilePaths = Directory.GetFiles(Application.dataPath + "/Levels");

        //Loops to assign
        for (int i = 0; i < levelFilePaths.Length; i++)
        {
            sr = new StreamReader(levelFilePaths[i]);
            string levelInfo;

            //Sets any buttons 
            if (levelFilePaths[i] == null)
            {
                levelButton[i].interactable = false;
            }
            else
            {
                //Loops while there are occupied lines in the file
                while ((levelInfo = sr.ReadLine()) != null)
                {
                    levelData.Add(levelInfo);
                }

                //If the button exists then to corresponding data is assigned
                if (levelButton != null)
                {
                    levelName[i].text = levelData[0];
                    dateText[i].text = levelData[1];
                    authorName[i].text = "By: " + levelData[2];

                    localPath = Application.dataPath + "/" + levelName[i].text + ".txt";
                }
            }
        }
    }

    public void nextButton()
    {
        Debug.Log("next");
    }

    public void prevButton()
    {
        Debug.Log("back");
    }

    //Called whenever a buttion is clicked
    public void onClick()
    {
        ScriptModSupport.filePath = Application.dataPath + "/" + localPath + ".txt";
        Application.LoadLevel("TestScene");
    }
}
