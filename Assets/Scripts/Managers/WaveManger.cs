using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManger : MonoBehaviour
{
    //Class calls
    [Header("Classes")]
    [SerializeField] private CheckpointManager checkpointManager;
    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private Camera camera;
    [Header("Variables")]
    //List of all the enemy objects
    public List<GameObject> Sharks;
    public List<GameObject> Serpents;
    public List<GameObject> Ships;

    //Get prefabs of enemies
    public GameObject SharkPrefab;
    public GameObject SerpentPrefab;
    public GameObject ShipPrefab;

    //Number of each enemy to spawn
    public int numSharks = 3;
    public int numSerpents;
    public int numShips;
    //Spawn Values
    public float spawnTime = 0f;
    [SerializeField] private bool hasSpawnedEnemies;
    // Start is called before the first frame update
    void Start()
    {
        UpdateLists();
    }
    // Update is called once per frame
    void Update()
    {
        Timer();
        StartCoroutine(SpawnEnemiesForWave(hasSpawnedEnemies));
    }

    //Timer for spawning enemies
    void Timer()
    {
        spawnTime -= Time.deltaTime;
    }
    //Update the lists based off the num variables
    void UpdateLists()
    {
        //Update Lists based off values from the num variables
        for(int i = 0; i < numSharks; i++)
        {
            Sharks.Add(SharkPrefab);
        }
        for(int i = 0; i < numSerpents; i++)
        {
            Serpents.Add(SerpentPrefab);
        }
        for(int i = 0; i < numShips; i++)
        {
            Ships.Add(ShipPrefab);
        }
    }

    IEnumerator SpawnEnemiesForWave(bool hasSpawnedEnemies)
    {
        if(spawnTime <= 0 && checkpointManager.FirstCheckpoint && !hasSpawnedEnemies && checkpointManager.SecondCheckpoint == false)
        {
            Debug.Log("Spawning Wave 1");
            for(int i = 0; i < Sharks.Count; i++)
            {
                spawnManager.SpawnEnemyInRect(SharkPrefab,spawnManager.spawnArea1);
            }
            spawnTime = 5f;
        }
        if(spawnTime <= 0 && checkpointManager.SecondCheckpoint && checkpointManager.ThirdCheckpoint == false)
        {
            Debug.Log("Spawning Wave 2");
            for(int i = 0; i < Serpents.Count; i++)
            {
                spawnManager.SpawnEnemyInRect(SharkPrefab,spawnManager.spawnArea2);
            }
            spawnManager.SpawnEnemyInRect(SerpentPrefab,spawnManager.spawnArea2);
            spawnTime = 5f;
        }
        if(spawnTime <= 0 && checkpointManager.ThirdCheckpoint)
        {
            Debug.Log("Spawning Wave 3");
            for(int i = 0; i < Sharks.Count; i++)
            {
                spawnManager.SpawnEnemyInRect(SharkPrefab,spawnManager.spawnArea3);
            }
            for(int i = 0; i < Serpents.Count; i++)
            {
                spawnManager.SpawnEnemyInRect(SerpentPrefab,spawnManager.spawnArea3);
            }
            spawnManager.SpawnEnemyInRect(ShipPrefab,spawnManager.spawnArea3);
            spawnTime = 5f;
        }
        hasSpawnedEnemies = true;
        yield return new WaitForSeconds(spawnTime);
    }
}
