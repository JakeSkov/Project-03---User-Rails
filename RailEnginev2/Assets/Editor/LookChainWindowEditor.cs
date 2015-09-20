using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

/// <summary>
/// @author Mike Dobson
/// </summary>

public class LookChainWindowEditor : EditorWindow {

    public static Object selectedObject;
    SerializedProperty target;

    public static void Show(SerializedProperty pProperty)
    {
        EditorWindow window = EditorWindow.GetWindow(typeof(LookChainWindowEditor));
        (window as LookChainWindowEditor).target = pProperty;
    }

    //on the editor Window
    void OnGUI()
    {
        //minimum size for the display
        minSize = new Vector2(200, 200);

        //serialize properties to edit
        SerializedProperty targets = target.FindPropertyRelative("targets");
        SerializedProperty rotationSpeed = target.FindPropertyRelative("rotationSpeed");
        SerializedProperty lockTimes = target.FindPropertyRelative("lockTimes");
        SerializedProperty targetSize = target.FindPropertyRelative("targetSize");

        //initialize arrays to value needed
        targets.arraySize = targetSize.intValue;
        rotationSpeed.arraySize = targetSize.intValue + 1;
        lockTimes.arraySize = targetSize.intValue;

        //local variables
        Rect FacingDrawerDisplay;
        float displayHeight = 16f;
        float displayHeightDif = 18f;
        float offsetX = 5;
        float offsetY = 5;

        for(int i = 0; i < targetSize.intValue; i++)
        {
            //display block for look at
            FacingDrawerDisplay = new Rect(offsetX, offsetY, 70f, displayHeight);
            offsetX += 70f;
            EditorGUI.LabelField(FacingDrawerDisplay, "Look at");
            //units
            FacingDrawerDisplay = new Rect(offsetX, offsetY, position.width, displayHeight);
            offsetX = position.x;
            offsetY += displayHeightDif;
            EditorGUI.ObjectField(FacingDrawerDisplay, targets.GetArrayElementAtIndex(i), GUIContent.none);

            //display block for rotate time and lock time
            //rotate over
            FacingDrawerDisplay = new Rect(offsetX, offsetY, 160f, displayHeight);
            offsetX += 145f;
            EditorGUI.LabelField(FacingDrawerDisplay, "Rotate to target over");
            //time
            FacingDrawerDisplay = new Rect(offsetX, offsetY, 35f, displayHeight);
            offsetX += 35f;
            EditorGUI.PropertyField(FacingDrawerDisplay, rotationSpeed.GetArrayElementAtIndex(i), GUIContent.none);
            //units
            FacingDrawerDisplay = new Rect(offsetX, offsetY, 55f, displayHeight);
            offsetX = position.x;
            offsetY += displayHeightDif;
            EditorGUI.LabelField(FacingDrawerDisplay, "secs");

            //Lock for
            FacingDrawerDisplay = new Rect(offsetX, offsetY, 140f, displayHeight);
            offsetX += 125f;
            EditorGUI.LabelField(FacingDrawerDisplay, "Lock on target for");

            //time
            FacingDrawerDisplay = new Rect(offsetX, offsetY, 35f, displayHeight);
            offsetX += 35f;
            EditorGUI.PropertyField(FacingDrawerDisplay, lockTimes.GetArrayElementAtIndex(i), GUIContent.none);
            //units
            FacingDrawerDisplay = new Rect(offsetX, offsetY, 50f, displayHeight);
            offsetX = position.x;
            offsetY += displayHeightDif * 2;
            EditorGUI.LabelField(FacingDrawerDisplay, "secs.");
        }

        //display block for return time
        //return for
        FacingDrawerDisplay = new Rect(offsetX, offsetY, 190f, displayHeight);
        offsetX += 175f;
        EditorGUI.LabelField(FacingDrawerDisplay, "Rotate back to origin over");
        //time
        FacingDrawerDisplay = new Rect(offsetX, offsetY, 40f, displayHeight);
        offsetX += 40f;
        EditorGUI.PropertyField(FacingDrawerDisplay, rotationSpeed.GetArrayElementAtIndex(targetSize.intValue), GUIContent.none);
        //units
        FacingDrawerDisplay = new Rect(offsetX, offsetY, 65f, displayHeight);
        offsetX = position.x;
        EditorGUI.LabelField(FacingDrawerDisplay, "secs.");

        target.serializedObject.ApplyModifiedProperties();
    }
}
