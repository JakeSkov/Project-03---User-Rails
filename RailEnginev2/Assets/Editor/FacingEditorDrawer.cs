using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomPropertyDrawer(typeof(ScriptFacings))]
public class FacingEditorDrawer : PropertyDrawer {

	bool movementShow = false;
    ScriptMovements waypointScript;
    float extraHeight = 60f;
    float displaySize = 20f;
    float numDisplays = 0f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        //-------------------------------------------

        SerializedProperty facingType = property.FindPropertyRelative("facingType");
        SerializedProperty targets = property.FindPropertyRelative("targets");
        SerializedProperty rotationSpeed = property.FindPropertyRelative("rotationSpeed");
        SerializedProperty lockTimes = property.FindPropertyRelative("lockTimes");
        SerializedProperty facingTime = property.FindPropertyRelative("facingTime");
        SerializedProperty showInEditor = property.FindPropertyRelative("showInEditor");

        //===========================================
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label) + (extraHeight);
    }
}
