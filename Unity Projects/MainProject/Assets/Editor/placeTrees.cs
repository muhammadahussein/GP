using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using Random = UnityEngine.Random;

public class placeTrees : MonoBehaviour
{
    [MenuItem("Tools/Trees/Place Trees")]
    static void treeRay()
    {
        foreach (GameObject g in Selection.gameObjects)
        {
            RaycastHit h;
            if(Physics.Raycast(g.transform.position + (Vector3.up * 20), Vector3.down, out h, 100))
                g.transform.position = h.point;
        }
    }

    [MenuItem("Tools/Trees/Randomize Scale")]
    static void RandomizeScale()
    {
        foreach (GameObject g in Selection.gameObjects)
        {
            g.transform.localScale = Vector3.one*(0.8f+(Random.value/2));
        }
    }
    [MenuItem("Tools/Trees/Randomize Rotation")]
    static void RandomizeRotation()
    {
        foreach (GameObject g in Selection.gameObjects)
        {
            g.transform.Rotate(Vector3.up, Random.value*360, Space.World);
        }
    }
}
