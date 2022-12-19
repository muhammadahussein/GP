using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(QuestGiver))]
public class SpawnHandle : Editor
{
    public void OnSceneGUI()
    {
        var t = target as QuestGiver;

        EditorGUI.BeginChangeCheck();

        Vector3 position =  Handles.PositionHandle(t.SpawnPoint, Quaternion.identity);
        
        
        Handles.Label(t.SpawnPoint, "Spawn Point");
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "CHanged Spawn Point");
            t.SpawnPoint = position;
            RaycastHit hit;
            if (Physics.Raycast(t.SpawnPoint + Vector3.up * 100, Vector3.down, out hit))
                t.SpawnPoint.y = hit.point.y;
        }
    }
}