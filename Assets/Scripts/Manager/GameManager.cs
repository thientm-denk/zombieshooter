using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    [SerializeField] List<GameObject> spawnArea;


    private ZombieSpawner zombieSpawner;

    int currentRound = 1;
    public bool isPlaying = false;
    // Start is called before the first frame update
    void Start()
    {
        zombieSpawner = ZombieSpawner.instance;
        IEnumerator coroutine = SpawnZombie(currentRound, 2f);
        StartCoroutine(coroutine);

    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaying)
        {
            return;
        }

        if(zombieSpawner.activeZomList.Count == 0)
        {
            isPlaying = false;
            currentRound++;
            IEnumerator coroutine = SpawnZombie(currentRound, 2f);
            StartCoroutine(coroutine);
        }
    }


    IEnumerator SpawnZombie(int round, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        UIManager.Instance.ShowMessageRound(round);

        SpawnMutiZombie(spawnArea, 5 + round * 5);
        isPlaying = true;
    }

    private void SpawnMutiZombie(List<GameObject> spawnArea, int numberZombieToSpawn)
    {
        for (int i = 0; i < numberZombieToSpawn; i++)
        {
            zombieSpawner.SpawnAZombie(GetRandomSpawnPos(spawnArea));
        }
    
    }

    public Vector3 GetRandomSpawnPos(List<GameObject> spawnArea)
    {
        var index = Random.Range(0, spawnArea.Count);

        var spawnPos = spawnArea[index].transform.position;
        
        var scaleX = spawnArea[index].transform.localScale.x / 2;
        var scaleZ = spawnArea[index].transform.localScale.z / 2;
        spawnPos.x = Random.Range(spawnArea[index].transform.position.x - scaleX, spawnArea[index].transform.position.x + scaleX);
        spawnPos.z = Random.Range(spawnArea[index].transform.position.z - scaleZ, spawnArea[index].transform.position.z + scaleZ);

        return spawnPos;

    }

    
}
