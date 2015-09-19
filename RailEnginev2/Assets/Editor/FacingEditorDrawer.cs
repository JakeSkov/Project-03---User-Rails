using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomPropertyDrawer(typeof(ScriptFacings))]
public class FacingEditorDrawer : PropertyDrawer {

    float extraHeight = 60f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        //-------------------------------------------
        //common variables
        SerializedProperty facingType = property.FindPropertyRelative("facingType");
        //look at target and look chain variables
        SerializedProperty targets = property.FindPropertyRelative("targets");
        SerializedProperty rotationSpeed = property.FindPropertyRelative("rotationSpeed");
        SerializedProperty lockTimes = property.FindPropertyRelative("lockTimes");
        //wait variable
        SerializedProperty facingTime = property.FindPropertyRelative("facingTime");
        //inspector only variables
        SerializedProperty showInEditor = property.FindPropertyRelative("showInEditor");

        SerializedProperty targetsSize = property.FindPropertyRelative("targetSize");

        //local variables
        float offsetX = position.x;
        float offsetY = position.y;

        //display for facing type
        Rect FacingDrawerDisplay = new Rect(offsetX, offsetY, position.width, 15f);
        offsetY += 17f;
        EditorGUI.PropertyField(FacingDrawerDisplay, facingType);

        //switch on facing type
        switch(facingType.enumValueIndex)
        {
            
            case (int)FacingTypes.LOOKAT:
                //display block for look at
                FacingDrawerDisplay = new Rect(offsetX, offsetY, 70f, 15f);
                offsetX += 70f;
                EditorGUI.LabelField(FacingDrawerDisplay, "Look at");
                //units
                FacingDrawerDisplay = new Rect(offsetX, offsetY, position.width, 15f);
                offsetX = position.x;
                offsetY += 17f;
                targets.InsertArrayElementAtIndex(0);
                EditorGUI.ObjectField(FacingDrawerDisplay, targets.GetArrayElementAtIndex(0), GUIContent.none);

                //display block for rotate time and lock time
                //rotate over
                FacingDrawerDisplay = new Rect(offsetX, offsetY, 160f, 15f);
                offsetX += 145f;
                EditorGUI.LabelField(FacingDrawerDisplay, "Rotate to target over");
                //time
                FacingDrawerDisplay = new Rect(offsetX, offsetY, 50f, 15f);
                offsetX += 35f;
                rotationSpeed.InsertArrayElementAtIndex(0);
                EditorGUI.PropertyField(FacingDrawerDisplay, rotationSpeed.GetArrayElementAtIndex(0), GUIContent.none);
                //units, lock for
                FacingDrawerDisplay = new Rect(offsetX, offsetY, 135f, 15f);
                offsetX += 125f;
                EditorGUI.LabelField(FacingDrawerDisplay, "secs, and lock for");
                //time
                FacingDrawerDisplay = new Rect(offsetX, offsetY, 50f, 15f);
                offsetX += 35f;
                lockTimes.InsertArrayElementAtIndex(0);
                EditorGUI.PropertyField(FacingDrawerDisplay, lockTimes.GetArrayElementAtIndex(0), GUIContent.none);
                //units
                FacingDrawerDisplay = new Rect(offsetX, offsetY, 50f, 15f);
                offsetX = position.x;
                offsetY += 17f;
                EditorGUI.LabelField(FacingDrawerDisplay, "secs.");

                //display block for return time
                //return for
                FacingDrawerDisplay = new Rect(offsetX, offsetY, 190f, 15f);
                offsetX += 175f;
                EditorGUI.LabelField(FacingDrawerDisplay, "Rotate back to origin over");
                //time
                FacingDrawerDisplay = new Rect(offsetX, offsetY, 50f, 15f);
                offsetX += 40f;
                rotationSpeed.InsertArrayElementAtIndex(1);
                EditorGUI.PropertyField(FacingDrawerDisplay, rotationSpeed.GetArrayElementAtIndex(1), GUIContent.none);
                //units
                FacingDrawerDisplay = new Rect(offsetX, offsetY, 65f, 15f);
                offsetX = position.x;
                EditorGUI.LabelField(FacingDrawerDisplay, "secs.");
                break;

            case (int)FacingTypes.LOOKCHAIN:
                FacingDrawerDisplay = new Rect(offsetX, offsetY, 150f, 15f);
                offsetX += 135f;
                EditorGUI.LabelField(FacingDrawerDisplay, "Amount of targets:");

                FacingDrawerDisplay = new Rect(offsetX, offsetY, 50f, 15f);
                offsetX = position.x;
                EditorGUI.PropertyField(FacingDrawerDisplay, targetsSize, GUIContent.none);

                if(GUILayout.Button("Edit Look Chain"))
                {
                    LookChainWindowEditor.Show(property);
                }
                //for (int i = 0; i < targetsSize.intValue; i++ )
                //{
                //    targets.InsertArrayElementAtIndex(i);
                //    rotationSpeed.InsertArrayElementAtIndex(i);
                //    lockTimes.InsertArrayElementAtIndex(i);
                //}
                //rotationSpeed.InsertArrayElementAtIndex(targetsSize.intValue);

                    break;
            case (int)FacingTypes.WAIT:
                //display block for wait time to next facing
                FacingDrawerDisplay = new Rect(offsetX, offsetY, 70f, 15f);
                offsetX += 60f;
                EditorGUI.LabelField(FacingDrawerDisplay, "Wait for");

                FacingDrawerDisplay = new Rect(offsetX, offsetY, 50f, 15f);
                offsetX += 40f;
                EditorGUI.PropertyField(FacingDrawerDisplay, facingTime, GUIContent.none);

                FacingDrawerDisplay = new Rect(offsetX, offsetY, 200f, 15f);
                offsetX = position.x;
                EditorGUI.LabelField(FacingDrawerDisplay, "seconds before next facing.");
                break;
        }

        //===========================================
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label) + (extraHeight);
    }
}
