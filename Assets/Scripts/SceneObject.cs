using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SceneObject : MonoBehaviour
{
    [HideInInspector] public SceneObjectDataSO SceneObjectDataSO;

    private GameObject _item;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void FixedUpdate()
    {
        CheckObject();
    }

    public void Init(SceneObjectDataSO sceneObjectDataSO)
    {
        SceneObjectDataSO = sceneObjectDataSO;
    }

    public void VisibleItem()
    {
        if (_item == null) InstantiateItem();
        else _item.SetActive(true);
    }

    public void UnvisibleItem()
    {
        if (_item != null) _item.SetActive(false);
    }

    private void InstantiateItem()
    {
        GameObject newSceneObject = Instantiate(SceneObjectDataSO.Prefab, transform);
        newSceneObject.transform.localPosition = Vector3.zero;
        newSceneObject.transform.localEulerAngles = Vector3.zero;
        _item = newSceneObject;
        _item.SetActive(true);
    }

    private void CheckObject()
    {
        if (Util.IsVisible(transform.position, _camera))
        {
            VisibleItem();
        }
        else
        {
            UnvisibleItem();
        }
    }
}
