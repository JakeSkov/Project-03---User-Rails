using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class EngineWindowEditor : EditorWindow {

    public static Object selectedObject;

    

    public static void Init()
    {
        EngineWindowEditor window = (EngineWindowEditor)EditorWindow.GetWindow(typeof(EngineWindowEditor));
        window.Show();

        selectedObject = Selection.activeObject;
        
    }

	void OnGUI()
    {
        minSize = new Vector2(750, 500);
    }
}
