using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class ScriptLevelButton : MonoBehaviour 
{
    public Text levelName;
    public Text authorName;
    public Text dateText;
    public Image levelImage;

    TextAsset[] levelFile;

	// Use this for initialization
	void Start () 
    {
        StreamReader sr;
        levelFile = FindObjectsOfType<TextAsset>();
        int lineNum = 0;

        for (int i = 0; i < ScripLevelSelect.levelButton.Length; i++)
        {
            sr = new StreamReader("/" + levelFile[i]);

            string levelInfo = sr.ReadLine();
            lineNum++;

            if (lineNum == 1)
            {
                levelName.text = levelInfo;
            }
            if (lineNum == 2)
            {
                dateText.text = levelInfo;
            }
            if (lineNum == 3)
            {
                authorName.text = levelInfo;
            }
        }
	}
    
}
