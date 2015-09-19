using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomPropertyDrawer(typeof(ScriptEffects))]
public class EffectsEditorDrawer : PropertyDrawer {

    bool movementShow = false;
    ScriptMovements waypointScript;
    float extraHeight = 60f;
    float displaySize = 20f;
    float numDisplays = 0f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        //-------------------------------------------

        SerializedProperty effectType = property.FindPropertyRelative("effectType");
        SerializedProperty effectTime = property.FindPropertyRelative("effectTime");
        SerializedProperty fadeInTime = property.FindPropertyRelative("fadeInTime");
        SerializedProperty fadeOutTime = property.FindPropertyRelative("fadeOutTime");
        SerializedProperty imageScale = property.FindPropertyRelative("imageScale");
        SerializedProperty magnitude = property.FindPropertyRelative("magnitude");
        SerializedProperty showInEditor = property.FindPropertyRelative("showInEditor");


        //===========================================
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label) + (extraHeight);
    }
}
