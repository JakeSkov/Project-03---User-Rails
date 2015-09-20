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
        }
        if (facings.Count <= 0)
        {
            facings.Add(new ScriptFacings());
        }
        if (movements.Count <= 0)
        {
            movements.Add(new ScriptMovements());
        }
    }

    void OnGUI()
    {
        minSize = new Vector2(750, 300);
        //display variables
        float offsetX = 5f;
        float offsetY = 10f;
        const float DISPLAY_HEIGHT = 17F;
        const float DISPLAY_DIF = 20F;
        const float ELEMENT_DISPLAY = 215F;
        GUI.color = Color.white;

        //----------------------------------
        //Display block for movement
        Vector2 movementPoint1 = new Vector2(-115, 6);
        Vector2 movementPoint2 = new Vector2(120, 6);
        Drawing.DrawLine(movementPoint1, movementPoint2, Color.black, 1f, true);

        Rect windowDisplay = new Rect(offsetX, offsetY, ELEMENT_DISPLAY, DISPLAY_HEIGHT);
        offsetX += 10f;
        offsetY += DISPLAY_DIF;
        EditorGUI.LabelField(windowDisplay, "Movement " + (movementFocus + 1));
        //type
        windowDisplay = new Rect(offsetX, offsetY, ELEMENT_DISPLAY, DISPLAY_HEIGHT);
        offsetY += DISPLAY_DIF;
        movements[movementFocus].moveType = (MovementTypes)EditorGUI.EnumPopup(windowDisplay, movements[movementFocus].moveType);
        //time
        windowDisplay = new Rect(offsetX, offsetY, ELEMENT_DISPLAY, DISPLAY_HEIGHT);
        offsetY += DISPLAY_DIF;
        movements[movementFocus].movementTime = EditorGUI.FloatField(windowDisplay, movements[movementFocus].movementTime);
        switch(movements[movementFocus].moveType)
        {
            case MovementTypes.BEZIER:
                //display waypoint
                offsetY += DISPLAY_DIF;
                windowDisplay = new Rect(offsetX, offsetY, ELEMENT_DISPLAY, DISPLAY_HEIGHT);
                offsetY -= DISPLAY_DIF;
                movements[movementFocus].curveWaypoint = (GameObject) EditorGUI.ObjectField(windowDisplay, "Curve Waypoint", movements[movementFocus].curveWaypoint, typeof(GameObject));
                goto case MovementTypes.MOVE;
            case MovementTypes.MOVE:
                //display curve point
                windowDisplay = new Rect(offsetX, offsetY, ELEMENT_DISPLAY, DISPLAY_HEIGHT);
                offsetY = 120f;
                movements[movementFocus].endWaypoint = (GameObject) EditorGUI.ObjectField(windowDisplay, "End Waypoint", movements[movementFocus].endWaypoint, typeof(GameObject));
                break;
            case MovementTypes.WAIT:
                offsetY = 120f;
                break;
        }
        //Movement Buttons Display
        offsetX += 15f;
        windowDisplay = new Rect(offsetX, offsetY, 40f, DISPLAY_HEIGHT);
        offsetX += 70f;
        if(GUI.Button(windowDisplay,"Prev"))
        {
            if (movementFocus > 0)
            {
                movementFocus--;
            }
        }
        windowDisplay = new Rect(offsetX, offsetY, 40f, DISPLAY_HEIGHT);
        offsetX += 70f;
        if(GUI.Button(windowDisplay, "Add"))
        {
            movements.Insert(movementFocus + 1, new ScriptMovements());
            movementFocus++;
        }
        windowDisplay = new Rect(offsetX, offsetY, 40f, DISPLAY_HEIGHT);
        offsetX -= 105f;
        offsetY += 30f;
        if(GUI.Button(windowDisplay, "Next"))
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
            if(movementFocus == movements.Count - 1)
            {
                movements.RemoveAt(movementFocus);
                movementFocus--;
            }
            else
            {
                movements.RemoveAt(movementFocus);
            }
        }
        GUI.color = Color.white;

        //Effect Display Block
        windowDisplay = new Rect(offsetX, offsetY, ELEMENT_DISPLAY, DISPLAY_HEIGHT);
        offsetY += DISPLAY_DIF;
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
        offsetX = 280f;
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
            if (effectFocus == effects.Count - 1)
            {
                effects.RemoveAt(effectFocus);
                effectFocus--;
            }
            else
            {
                effects.RemoveAt(effectFocus);
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
    }

}
