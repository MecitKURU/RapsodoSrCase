using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SceneObjectData", fileName = "SceneObjectData")]
public class SceneObjectDataSO : ScriptableObject
{
    public string Name;
    public GameObject Prefab;
}
