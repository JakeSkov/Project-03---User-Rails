using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

/// <summary>
/// Author: Matt Gipson
/// Contact: Deadwynn@gmail.com
/// Domain: www.livingvalkyrie.com
/// 
/// Description: ExportWaypoints 
/// </summary>
public class ExportWaypoints {
    #region Fields

    #endregion

    [MenuItem("Waypoint Engine/Export/From selected object")]
    public static void Export() {
        Debug.Log("export clicked");
        Debug.Log("selected object: " + Selection.activeObject);

        if (Selection.activeObject is GameObject) {
            Debug.Log("is game object");
            GameObject go = (GameObject) Selection.activeObject;
            if (go.GetComponent<ScriptEngine>() != null) {
                Debug.Log("has engine component");
                ExportLists(go);
            } else {
                EditorUtility.DisplayDialog("Warning!", "This game object does not have an engine component!", "understood");
            }
        } else {
            EditorUtility.DisplayDialog("Warning!", "The selected object is not a game object!", "understood");
        }
    }

    static void ExportLists(GameObject go) {
        //get script
        ScriptEngine engine = go.GetComponent<ScriptEngine>();

        List<string> waypoints = new List<string>();

        foreach (ScriptMovements node in engine.movements) {
            string nodeString = "M_";

            //type
            switch (node.moveType) {
                case MovementTypes.STRAIGHT:

                    //MOVE takes a time and a destination separated by a space.
                    nodeString += string.Format("MOVE {0} {1},{2},{3}",
                                                node.movementTime,
                                                node.endWaypoint.transform.position.x,
                                                node.endWaypoint.transform.position.y,
                                                node.endWaypoint.transform.position.z);

                    break;
                case MovementTypes.BEZIER:

                    //BEZIER takes a time, a destination, and a curve control point.
                    nodeString += string.Format("BEZIER {0} {1},{2},{3} {4},{5},{6}",
                                                node.movementTime,
                                                node.endWaypoint.transform.position.x,
                                                node.endWaypoint.transform.position.y,
                                                node.endWaypoint.transform.position.z,
                                                node.curveWaypoint.transform.position.x,
                                                node.curveWaypoint.transform.position.y,
                                                node.curveWaypoint.transform.position.z );

                    break;
                case MovementTypes.WAIT:

                    //WAIT takes just a time.
                    nodeString += string.Format("WAIT {0}", node.movementTime);

                    break;
            }

            waypoints.Add(nodeString);
        }

        foreach (ScriptFacings node in engine.facings) {
            string nodeString = "F_";

            //Facings are FREELOOK, WAIT, LOOKAT, and LOOKCHAIN

            waypoints.Add(nodeString);
        }

        foreach (ScriptEffects node in engine.effects) {
            string nodeString = "E_";

            //Effects are SHAKE, SPLATTER, WAIT, and FADE

            waypoints.Add(nodeString);
        }

        foreach (string s in waypoints) {
            Debug.Log(s);
        }
    }

}

//After the underscore is the specific type of movement, effect, or facing followed by a space.
// Movements are MOVE, WAIT, and BEZIER
// Facings are FREELOOK, WAIT, LOOKAT, and LOOKCHAIN
// Effects are SHAKE, SPLATTER, WAIT, and FADE

//EX: M_MOVE 1 2,3,4

//EX: M_WAIT 1
// EX: F_WAIT 1
// EX: E_WAIT 1

//EX: M_BEZIER 1 2,3,4 5,6,7
//FREELOOK takes a time.
// EX: F_FREELOOK 1
//LOOKAT takes a look to time, a lock time, a look target, and a return time.
//EX: F_LOOKAT 1 2 3,4,5 6
//LOOKCHAIN takes two times per target, an unlimited number of targets, and a return time at the end.
// EX: F_LOOKCHAIN 1 2 3,4,5 6 7 8,9,0 1 2 3,4,5 6
//SHAKE takes an effect time and a magnitude.
// EX: E_SHAKE 1 2
//SPLATTER takes a stay time, a fade-in time, a fade-out time, and an image scale
// EX: E_SPLATTER 1 2 3 4
//FADE takes a stay-blacked out time, a fade-to-black time, and a fade-back time
// EX: E_FADE 1 2 3
//The line declarations will work with out full uppercase, but that is poor modding practice.
//Any line that does not start with one of these line declarations will be treated as a comment and skipped.