using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : SingletonManager<ObjectPooling>
{
    public List<GameObject> bulletPrefabs;
    public List<GameObject> pooledObjects = new List<GameObject>();
    public int amountToPool = 100;

    public void CreateBullets(GameObject bulletType)
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(bulletType, transform);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
