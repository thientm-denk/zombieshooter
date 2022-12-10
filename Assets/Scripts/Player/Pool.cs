using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField]
    GameObject bulletPrefabs;

    private List<GameObject> pooledObjects;
    private int amoutToPool = 20;

    #region Singleton
    public static Pool instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion



    // Start is called before the first frame update


    void Start()
    {
        pooledObjects = new List<GameObject>();

        for (int i = 0; i < amoutToPool; i++)
        {
            GameObject obj = Instantiate(bulletPrefabs);
            obj.transform.parent = transform;
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

    public GameObject GetAndActivePooledObject(Vector3 posToSpawn, bool status)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                pooledObjects[i].transform.position = posToSpawn;
                pooledObjects[i].SetActive(status);
                return pooledObjects[i];
            }
        }
        return null;
    }
    public GameObject GetAndActivePooledObject(Vector3 posToSpawn)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                pooledObjects[i].transform.position = posToSpawn;
                pooledObjects[i].SetActive(true);
                return pooledObjects[i];
            }
        }
        return null;
    }

    public void BackToPool(GameObject bulet)
    {
        bulet.transform.parent = transform;
        bulet.SetActive(false);
    }
}
