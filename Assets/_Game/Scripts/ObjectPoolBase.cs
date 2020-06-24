using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPoolBase : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab = null;

    protected List<GameObject> objectPool = new List<GameObject>();

    protected GameObject GetInactiveFromPool()
    {
        var inactiveObject = objectPool.Find(x => !x.activeSelf);
        if (inactiveObject != null)
            return inactiveObject;

        var newObject = Instantiate(objectPrefab, transform);
        newObject.SetActive(false);
        objectPool.Add(newObject);

        return newObject;
    }
}
