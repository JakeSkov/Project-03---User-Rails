using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class LookChainWindowEditor : EditorWindow {

    public static Object selectedObject;
    SerializedProperty target;

    public static void Show(SerializedProperty pProperty)
    {
        EditorWindow window = EditorWindow.GetWindow(typeof(LookChainWindowEditor));
        (window as LookChainWindowEditor).target = pProperty;
    }

    //public static void Init(SerializedProperty Targets, SerializedProperty RotationSpeed, SerializedProperty LockTimes)
    //{
    //    LookChainWindowEditor window = (LookChainWindowEditor)EditorWindow.GetWindow(typeof(EngineWindowEditor));
    //    window.Show();

    //    selectedObject = Selection.activeObject;
    //}

    void OnGUI()
    {
        minSize = new Vector2(150, 150);

        SerializedProperty targets = target.FindPropertyRelative("targets");
        SerializedProperty rotationSpeed = target.FindPropertyRelative("rotationSpeed");
        SerializedProperty lockTimes = target.FindPropertyRelative("lockTimes");
        SerializedProperty targetSize = target.FindPropertyRelative("targetSize");

        targets.arraySize = targetSize.intValue;
        rotationSpeed.arraySize = targetSize.intValue + 1;
        lockTimes.arraySize = targetSize.intValue;

        Rect FacingDrawerDisplay;
        float offsetX = 5;
        float offsetY = 5;

        for(int i = 0; i < targetSize.intValue; i++)
        {
            //display block for look at
            FacingDrawerDisplay = new Rect(offsetX, offsetY, 70f, 15f);
            offsetX += 70f;
            EditorGUI.LabelField(FacingDrawerDisplay, "Look at");
            //units
            FacingDrawerDisplay = new Rect(offsetX, offsetY, position.width, 15f);
            offsetX = position.x;
            offsetY += 17f;
            targets.InsertArrayElementAtIndex(i);
            EditorGUI.ObjectField(FacingDrawerDisplay, targets.GetArrayElementAtIndex(i), GUIContent.none);

            //display block for rotate time and lock time
            //rotate over
            FacingDrawerDisplay = new Rect(offsetX, offsetY, 160f, 15f);
            offsetX += 145f;
            EditorGUI.LabelField(FacingDrawerDisplay, "Rotate to target over");
            //time
            FacingDrawerDisplay = new Rect(offsetX, offsetY, 35f, 15f);
            offsetX += 35f;
            EditorGUI.PropertyField(FacingDrawerDisplay, rotationSpeed.GetArrayElementAtIndex(i), GUIContent.none);
            //units, lock for
            FacingDrawerDisplay = new Rect(offsetX, offsetY, 135f, 15f);
            offsetX += 125f;
            EditorGUI.LabelField(FacingDrawerDisplay, "secs, and lock for");
            //time
            FacingDrawerDisplay = new Rect(offsetX, offsetY, 35f, 15f);
            offsetX += 35f;
            EditorGUI.PropertyField(FacingDrawerDisplay, lockTimes.GetArrayElementAtIndex(i), GUIContent.none);
            //units
            FacingDrawerDisplay = new Rect(offsetX, offsetY, 50f, 15f);
            offsetX = position.x;
            offsetY += 17f;
            EditorGUI.LabelField(FacingDrawerDisplay, "secs.");
            //EditorGUILayout.LabelField("Waypoint " + i);
            //EditorGUILayout.PropertyField(targets.GetArrayElementAtIndex(i));
            //EditorGUILayout.PropertyField(rotationSpeed.GetArrayElementAtIndex(i));
            //EditorGUILayout.PropertyField(lockTimes.GetArrayElementAtIndex(i));
            //EditorGUILayout.LabelField("");
        }
        //display block for return time
        //return for
        FacingDrawerDisplay = new Rect(offsetX, offsetY, 190f, 15f);
        offsetX += 175f;
        EditorGUI.LabelField(FacingDrawerDisplay, "Rotate back to origin over");
        //time
        FacingDrawerDisplay = new Rect(offsetX, offsetY, 35f, 15f);
        offsetX += 40f;
        EditorGUI.PropertyField(FacingDrawerDisplay, rotationSpeed.GetArrayElementAtIndex(targetSize.intValue), GUIContent.none);
        //units
        FacingDrawerDisplay = new Rect(offsetX, offsetY, 65f, 15f);
        offsetX = position.x;
        EditorGUI.LabelField(FacingDrawerDisplay, "secs.");
        //EditorGUILayout.LabelField("Rotate back");
        //EditorGUILayout.PropertyField(rotationSpeed.GetArrayElementAtIndex(targetSize.intValue));

        target.serializedObject.ApplyModifiedProperties();
    }
}
