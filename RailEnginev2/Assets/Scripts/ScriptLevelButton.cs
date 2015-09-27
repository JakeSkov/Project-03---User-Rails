using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// @author Jake Skov
/// @date 09/27/15
/// @desc Defines behavoir for level buttons
/// </summary>
public class ScriptLevelButton : MonoBehaviour
{
    public Text levelName;
    public Text authorName;
    public Text dateText;
    public Image levelImage;

    string[] levelFilePaths;

    // Use this for initialization
    void Start()
    {
        StreamReader sr;
        levelFilePaths = Directory.GetFiles(Application.dataPath + "/levels/");

        //Loops to assign 
        Debug.Log(levelFilePaths.Length);
        for (int i = 0; i < levelFilePaths.Length; i++)
        {
            sr = new StreamReader(levelFilePaths[i]);

            string levelInfo;

            List<string> levelData = new List<string>();

            for (int j = 0; j < ScripLevelSelect.levelButton.Length; j++)
            {
                if (levelFilePaths[i] == null)
                {
                    ScripLevelSelect.levelButton[j].interactable = false;
                }
                else
                {
                    while ((levelInfo = sr.ReadLine()) != null)
                    {
                        levelData.Add(levelInfo);
                    }
                    levelName = ScripLevelSelect.levelButton[j].GetComponentInChildren<Text>();
                    dateText = ScripLevelSelect.levelButton[j].GetComponentInChildren<Text>();
                    authorName = ScripLevelSelect.levelButton[j].GetComponentInChildren<Text>();

                    levelName.text = levelData[0];
                    dateText.text = levelData[1];
                    authorName.text = levelData[2];
                }
            }
        }
    }

    //Called whenever a buttion is clicked
    public void onClick()
    {
        FileInfo input = new FileInfo(Application.dataPath + "/" + levelName.text + ".txt");
        Debug.Log(input.ToString());
        //input.Replace(Application.dataPath + "/waypoints.txt", Application.dataPath + "/erg.txt");

        //Application.LoadLevel("TestScene");
    }
}
