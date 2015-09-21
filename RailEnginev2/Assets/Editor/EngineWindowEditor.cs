using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// @author Mike Dobson
/// </summary>

public class EngineWindowEditor : EditorWindow {



    List<ScriptMovements> movements;
    List<ScriptEffects> effects;
    List<ScriptFacings> facings;
    ScriptEngine engine;

    //local Variables
    int movementFocus = 0;
    int effectFocus = 0;
    int facingFocus = 0;

    void OnFocus()
    {
        engine = GameObject.FindWithTag("Player").GetComponent<ScriptEngine>();
        movements = engine.movements;
        effects = engine.effects;
        facings = engine.facings;

        if (effects.Count <= 0)
        {
            effects.Add(new ScriptEffects());
            effectFocus = 0;
        }
        if (facings.Count <= 0)
        {
            facings.Add(new ScriptFacings());
            facingFocus = 0;
        }
        if (movements.Count <= 0)
        {
            movements.Add(new ScriptMovements());
            movementFocus = 0;
        }

//		Debug.Log ("~~~ON FOCUS MOVEMENT COUNT~~~\n" +
//		           "\tENGINE: " + engine.movements.Count +
//		           "\t\tWINDOW: " + movements.Count);
    }

    void OnGUI()
    {
        minSize = new Vector2(750, 300);
        //display variables
        float offsetX = 5f;
        float offsetY = 10f;
        const float DISPLAY_HEIGHT = 17F;
        const float DISPLAY_DIF = 20F;
        const float ELEMENT_DISPLAY = 205F;
        GUI.color = Color.white;

        //Error Block
		if (movementFocus >= movements.Count)
			movementFocus = 0;
		if (effectFocus >= effects.Count)
			effectFocus = 0;
		if (facingFocus >= facings.Count)
			facingFocus = 0;

        //----------------------------------
        //Display block for movement
        //Movement Square
        //top
        Vector2 movementPoint1 = new Vector2(-110, 6);
        Vector2 movementPoint2 = new Vector2(117, 6);
        Drawing.DrawLine(movementPoint1, movementPoint2, Color.black, 1f, true);
        //right
        Vector2 movementPoint3 = new Vector2(230, 7);
        Vector2 movementPoint4 = new Vector2(230, 180);
        Drawing.DrawLine(movementPoint3, movementPoint4, Color.black, 1f, true);
        //bottom
        Vector2 movementPoint5 = new Vector2(-110, 177);
        Vector2 movementPoint6 = new Vector2(117, 177);
        Drawing.DrawLine(movementPoint5, movementPoint6, Color.black, 1f, true);
        //left
        Vector2 movementPoint7 = new Vector2(3, 7);
        Vector2 movementPoint8 = new Vector2(3, 180);
        Drawing.DrawLine(movementPoint7, movementPoint8, Color.black, 1f, true);

        //Movement element info
        Rect windowDisplay = new Rect(offsetX, offsetY, ELEMENT_DISPLAY, DISPLAY_HEIGHT);
        offsetX += 10f;
        offsetY += DISPLAY_DIF;
        EditorGUI.LabelField(windowDisplay, "Movement " + (movementFocus + 1));
        
        //movement type
        windowDisplay = new Rect(offsetX, offsetY, 100f, DISPLAY_HEIGHT);
        offsetX += 100f;
        EditorGUI.LabelField(windowDisplay, "Movement Type");
        windowDisplay = new Rect(offsetX, offsetY, 110f, DISPLAY_HEIGHT);
        offsetY += DISPLAY_DIF;
        offsetX = 15f;
        movements[movementFocus].moveType = (MovementTypes)EditorGUI.EnumPopup(windowDisplay, movements[movementFocus].moveType);
        
        //movement time
        windowDisplay = new Rect(offsetX, offsetY, 120f, DISPLAY_HEIGHT);
        offsetX += 120f;
        EditorGUI.LabelField(windowDisplay, "Move to target over");
        windowDisplay = new Rect(offsetX, offsetY, 50f, DISPLAY_HEIGHT);
        offsetX += 50f;
        movements[movementFocus].movementTime = EditorGUI.FloatField(windowDisplay, movements[movementFocus].movementTime);
        windowDisplay = new Rect(offsetX, offsetY, 45f, DISPLAY_HEIGHT);
        offsetY += DISPLAY_DIF;
        offsetX = 15f;
        EditorGUI.LabelField(windowDisplay, "secs");

        switch(movements[movementFocus].moveType)
        {
            case MovementTypes.BEZIER:
                //display waypoint
                offsetY += DISPLAY_DIF;
                windowDisplay = new Rect(offsetX, offsetY, 80f, DISPLAY_HEIGHT);
                offsetX += 80f;
                EditorGUI.LabelField(windowDisplay, "Curve Target");
                windowDisplay = new Rect(offsetX, offsetY, 135f, DISPLAY_HEIGHT);
                offsetY -= DISPLAY_DIF;
                offsetX = 15f;
                movements[movementFocus].curveWaypoint = (GameObject) EditorGUI.ObjectField(windowDisplay, GUIContent.none, movements[movementFocus].curveWaypoint, typeof(GameObject));
                goto case MovementTypes.STRAIGHT;
            case MovementTypes.STRAIGHT:
                //display curve point
                windowDisplay = new Rect(offsetX, offsetY, 80f, DISPLAY_HEIGHT);
                offsetX += 80f;
                EditorGUI.LabelField(windowDisplay, "End Target");
                windowDisplay = new Rect(offsetX, offsetY, 135f, DISPLAY_HEIGHT);
                offsetY = 120f;
                offsetX = 15f;
                movements[movementFocus].endWaypoint = (GameObject) EditorGUI.ObjectField(windowDisplay, GUIContent.none, movements[movementFocus].endWaypoint, typeof(GameObject));
                break;
            case MovementTypes.WAIT:
                offsetY = 120f;
                offsetX = 15f;
                break;
        }
        //Movement Buttons Display
        offsetX += 15f;
        windowDisplay = new Rect(offsetX, offsetY, 40f, DISPLAY_HEIGHT);
        offsetX += 70f;

		GUIStyle miniRight = new GUIStyle (EditorStyles.miniButtonRight);
		GUIStyle miniLeft = new GUIStyle (EditorStyles.miniButtonLeft);
		GUIStyle miniMid = new GUIStyle (EditorStyles.miniButtonMid);

        if(GUI.Button(windowDisplay,"Prev", miniLeft))
        {
            if (movementFocus > 0)
            {
                movementFocus--;
            }
        }
        windowDisplay = new Rect(offsetX, offsetY, 40f, DISPLAY_HEIGHT);
        offsetX += 70f;
        if(GUI.Button(windowDisplay, "Add", miniMid))
        {
            movements.Insert(movementFocus + 1, new ScriptMovements());
            movementFocus++;
        }
        windowDisplay = new Rect(offsetX, offsetY, 40f, DISPLAY_HEIGHT);
        offsetX -= 105f;
        offsetY += 30f;
        if(GUI.Button(windowDisplay, "Next", miniRight))
        {
            if (movementFocus < movements.Count - 1)
            {
                movementFocus++;
            }
        }
        windowDisplay = new Rect(offsetX, offsetY, 110f, DISPLAY_HEIGHT);
        offsetX += 205f;
        offsetY = 10f;
        GUI.color = Color.red;
        if(GUI.Button(windowDisplay, "Delete Waypoint"))
        {
            if (movementFocus == movements.Count - 1 && effects.Count > 1)
            {
                movements.RemoveAt(movementFocus);
                movementFocus--;
            }
            else if (movements.Count > 1)
            {
                movements.RemoveAt(movementFocus);
            }
            else
            {
                Debug.Log("Cannot remove last element from list");
            }
        }
        GUI.color = Color.white;

        //Effect Display Block
        //Effects Square
        Vector2 effectsPoint1 = new Vector2(154f, 6f);
        Vector2 effectsPoint2 = new Vector2(380f, 6f);
        Drawing.DrawLine(effectsPoint1, effectsPoint2, Color.black, 1f, true);
        Vector2 effectsPoint3 = new Vector2(267, 7);
        Vector2 effectsPoint4 = new Vector2(267, 180);
        Drawing.DrawLine(effectsPoint3, effectsPoint4, Color.black, 1f, true);

        //Effects Info
        windowDisplay = new Rect(offsetX, offsetY, ELEMENT_DISPLAY, DISPLAY_HEIGHT);
        offsetY += DISPLAY_DIF;
        offsetX += 10f;
        EditorGUI.LabelField(windowDisplay, "Effect " + (effectFocus + 1));
        //type
        windowDisplay = new Rect(offsetX, offsetY, ELEMENT_DISPLAY, DISPLAY_HEIGHT);
        offsetY += DISPLAY_DIF;
        effects[effectFocus].effectType = (EffectTypes)EditorGUI.EnumPopup(windowDisplay, effects[effectFocus].effectType);
        //Time
        windowDisplay = new Rect(offsetX, offsetY, ELEMENT_DISPLAY, DISPLAY_HEIGHT);
        offsetY += DISPLAY_DIF;
        effects[effectFocus].effectTime = EditorGUI.FloatField(windowDisplay, effects[effectFocus].effectTime);
        //switch on type
        switch(effects[effectFocus].effectType)
        {
            case EffectTypes.SPLATTER:
                windowDisplay = new Rect(offsetX, offsetY, ELEMENT_DISPLAY, DISPLAY_HEIGHT);
                offsetY += DISPLAY_DIF;
                effects[effectFocus].imageScale = EditorGUI.FloatField(windowDisplay, effects[effectFocus].imageScale);
                goto case EffectTypes.FADE;
            case EffectTypes.FADE:
                windowDisplay = new Rect(offsetX, offsetY, ELEMENT_DISPLAY, DISPLAY_HEIGHT);
                offsetY += DISPLAY_DIF;
                effects[effectFocus].fadeInTime = EditorGUI.FloatField(windowDisplay, effects[effectFocus].fadeInTime);
                windowDisplay = new Rect(offsetX, offsetY, ELEMENT_DISPLAY, DISPLAY_HEIGHT);
                offsetY = 120f;
                effects[effectFocus].fadeOutTime = EditorGUI.FloatField(windowDisplay, effects[effectFocus].fadeOutTime);
                break;
            case EffectTypes.SHAKE:
                windowDisplay = new Rect(offsetX, offsetY, ELEMENT_DISPLAY, DISPLAY_HEIGHT);
                offsetY = 120f;
                effects[effectFocus].magnitude = EditorGUI.FloatField(windowDisplay, effects[effectFocus].magnitude);
                //offsetX 
                break;
            case EffectTypes.WAIT:
                offsetY = 120f;
                break;
        }
        //Effects Buttons Display
        offsetX = 290f;
        windowDisplay = new Rect(offsetX, offsetY, 40f, DISPLAY_HEIGHT);
        offsetX += 70f;
        if (GUI.Button(windowDisplay, "Prev"))
        {
            if (effectFocus > 0)
            {
                effectFocus--;
            }
        }
        windowDisplay = new Rect(offsetX, offsetY, 40f, DISPLAY_HEIGHT);
        offsetX += 70f;
        if (GUI.Button(windowDisplay, "Add"))
        {
            effects.Insert(effectFocus + 1, new ScriptEffects());
            effectFocus++;
        }
        windowDisplay = new Rect(offsetX, offsetY, 40f, DISPLAY_HEIGHT);
        offsetX -= 105f;
        offsetY += 30f;
        if (GUI.Button(windowDisplay, "Next"))
        {
            if (effectFocus < effects.Count - 1)
            {
                effectFocus++;
            }
        }
        windowDisplay = new Rect(offsetX, offsetY, 110f, DISPLAY_HEIGHT);
        offsetX += 205f;
        offsetY = 10f;
        GUI.color = Color.red;
        if (GUI.Button(windowDisplay, "Delete Waypoint"))
        {
            if (effectFocus == effects.Count - 1 && effects.Count > 1)
            {
                effects.RemoveAt(effectFocus);
                effectFocus--;
            }
            else if (effects.Count > 1)
            {
                effects.RemoveAt(effectFocus);
            }
            else
            {
                Debug.Log("Cannot remove last element from list");
            }
        }
        GUI.color = Color.white;

        //Facings Display Block
        offsetX = 525f;
        windowDisplay = new Rect(offsetX, offsetY, ELEMENT_DISPLAY, DISPLAY_HEIGHT);
        offsetX += 10f;
        offsetY += DISPLAY_DIF;
        EditorGUI.LabelField(windowDisplay, "Facing " + (facingFocus + 1));
        //type
        windowDisplay = new Rect(offsetX, offsetY, ELEMENT_DISPLAY, DISPLAY_HEIGHT);
        offsetY += DISPLAY_DIF;
        facings[facingFocus].facingType = (FacingTypes)EditorGUI.EnumPopup(windowDisplay, facings[facingFocus].facingType);
        //switch on type
        switch (facings[facingFocus].facingType)
        {
            case FacingTypes.LOOKAT:

                offsetY = 120f;
                break;
            case FacingTypes.LOOKCHAIN:
                offsetY = 120f;
                break;
            case FacingTypes.FREELOOK:
                offsetY = 120f;
                break;
            case FacingTypes.WAIT:
                windowDisplay = new Rect(offsetX, offsetY, ELEMENT_DISPLAY, DISPLAY_HEIGHT);
                facings[facingFocus].facingTime = EditorGUI.FloatField(windowDisplay, facings[facingFocus].facingTime);
                offsetY = 120f;
                break;

        }
        //Effects Buttons Display
        offsetX = 540f;
        windowDisplay = new Rect(offsetX, offsetY, 40f, DISPLAY_HEIGHT);
        offsetX += 70f;
        if (GUI.Button(windowDisplay, "Prev"))
        {
            if (facingFocus > 0)
            {
                facingFocus--;
            }
        }
        windowDisplay = new Rect(offsetX, offsetY, 40f, DISPLAY_HEIGHT);
        offsetX += 70f;
        if (GUI.Button(windowDisplay, "Add"))
        {
            facings.Insert(facingFocus + 1, new ScriptFacings());
            facingFocus++;
        }
        windowDisplay = new Rect(offsetX, offsetY, 40f, DISPLAY_HEIGHT);
        offsetX -= 105f;
        offsetY += 30f;
        if (GUI.Button(windowDisplay, "Next"))
        {
            if (facingFocus < facings.Count - 1)
            {
                facingFocus++;
            }
        }
        windowDisplay = new Rect(offsetX, offsetY, 110f, DISPLAY_HEIGHT);
        offsetX += 205f;
        offsetY = 10f;
        GUI.color = Color.red;
        if (GUI.Button(windowDisplay, "Delete Waypoint"))
        {
            if (facingFocus == facings.Count - 1 && facings.Count > 1)
            {
                facings.RemoveAt(facingFocus);
                facingFocus--;
            }
            else if (facings.Count > 1)
            {
                facings.RemoveAt(facingFocus);
            }
            else
            {
                Debug.Log("Cannot remove last element from list");
            }
        }
        GUI.color = Color.white;
    }

    void OnLostFocus()
    {
        RecordData();
    }

    void RecordData()
    {
        engine.movements = movements;
        engine.effects = effects;
        engine.facings = facings;
//		Debug.Log ("~~~LOST FOCUS MOVEMENT COUNT~~~\n" +
//			"\tENGINE: " + engine.movements.Count +
//		           "\t\tWINDOW: " + movements.Count);
    }

}
