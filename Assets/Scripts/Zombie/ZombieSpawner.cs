using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    #region Singleton
    public static ZombieSpawner instance;
    private void Awake()
    {
        instance = this;
    }

    #endregion


    // Pool Manager
    private List<GameObject> pooledObjects;
    private int amoutToPool = 40;
    [SerializeField] GameObject zombiePrefab;

    public List<GameObject> activeZomList;
    // Start is called before the first frame update
    void Start()
    {
        pooledObjects = new List<GameObject>();

        for (int i = 0; i < amoutToPool; i++)
        {
            GameObject obj = Instantiate(zombiePrefab);
            obj.transform.parent = transform;
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Pool Manager
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
        var obj = GetPooledObject();
        if(obj != null)
        {
            obj.transform.position = posToSpawn;
            obj.SetActive(status);
            return obj;
        }
        
        return null;
    }
    public GameObject GetAndActivePooledObject(Vector3 posToSpawn)
   {
        var obj = GetPooledObject();
        if (obj != null)
        {
            obj.transform.position = posToSpawn;
            obj.SetActive(true);
            activeZomList.Add(obj);
            return obj;
        }

        return null;
    }

    public void BackToPool(GameObject zombie)
    {
        zombie.transform.parent = transform;
        activeZomList.Remove(zombie);
        zombie.SetActive(false);
    }

    #endregion

    #region Spawner

    public GameObject SpawnAZombie(Vector3 posToSpawn)
    {
        return GetAndActivePooledObject(posToSpawn);
    }

    public void DeSpawnAZombie(GameObject zombie)
    {
        BackToPool(zombie);
    }
    #endregion
}
