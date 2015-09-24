using UnityEngine;
using System.Collections;
using UnityEditor;

/// <summary>
/// Author: Matt Gipson
/// Contact: Deadwynn@gmail.com
/// Domain: www.livingvalkyrie.com
/// 
/// Description: ExportWindow 
/// </summary>
public class ExportWindow : EditorWindow {
    #region Fields

    #endregion

    void OnGUI() {
        EditorGUILayout.LabelField("Level Information");
        string name = EditorGUILayout.TextField("enter Level name");
        string author = EditorGUILayout.TextField( "enter author name" );
        if (GUILayout.Button("Export")) {
            ExportWaypoints.Export(author, name);
        }
    }

}