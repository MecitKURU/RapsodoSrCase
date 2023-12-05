using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[Serializable]

[CustomEditor(typeof(ObjectGenerator))]
public class SceneObjectsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ObjectGenerator objectSpawner = (ObjectGenerator)target;

        DrawDefaultInspector();

        EditorGUILayout.Space(20);
        if (GUILayout.Button("Generate Random Objects", GUILayout.Height(30)))
        {
            objectSpawner.GenerateRandomObjects();
        }
    }
}
