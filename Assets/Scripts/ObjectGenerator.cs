using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectGenerator : MonoBehaviour
{
    public List<SceneObjectItem> SceneObjectItems;
    public Transform Environment;
    public GameObject SceneObjectPrefab;

    public Terrain Terrain;

    public void GenerateRandomObjects()
    {
        int childCount = Environment.childCount;

        for(int i = 0; i < childCount; i++)
        {
            DestroyImmediate(Environment.GetChild(0).gameObject);
        }

        foreach (var sceneObjectItem in SceneObjectItems)
        {
            for(int i = 0; i < sceneObjectItem.Count; i++)
            {
                Vector3 position = Util.GetRandomPosition(Terrain);

                GameObject sceneObject = Instantiate(SceneObjectPrefab, Environment);
                sceneObject.name = sceneObjectItem.SceneObjectData.Name + "-" + (i + 1).ToString();
                sceneObject.transform.position = position;
                sceneObject.GetComponent<SceneObject>().Init(sceneObjectItem.SceneObjectData);
            }
        }

        Debug.Log("Scene objects created at random points!");
    }
}

[Serializable]
public class SceneObjectItem // It contains scene object data and count for generating
{
    public SceneObjectDataSO SceneObjectData;
    public int Count;
}
