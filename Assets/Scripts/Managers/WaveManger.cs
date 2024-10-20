using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManger : MonoBehaviour
{
    //Class calls
    [Header("Classes")]
    public DistanceTracker distanceTracker;
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
    public int numSharks;
    public int numSerpents;
    public int numShips;
    //Spawn Values
    public float spawnTime = 5f;

    [Header("Bools")]
    //Bools for checkpoints
    public bool FirstCheckpoint;
    public bool SecondCheckpoint;
    public bool ThirdCheckpoint;
    [Header("Arrays")]
    //List of Spawn points
    public GameObject[] SpawnPoints1;
    public GameObject[] SpawnPoints2;
    public GameObject[] SpawnPoints3;
    public List<GameObject> Allenemies;
    private int currentSpawnPointIndex = 0;
    void Start()
    {
        UpdateCheckpointStatus(0, true);
    }
    public void SetAll()
    {
        SetStartValues();
        SetArrays();
    }
    void FixedUpdate()
    {
        Timer();
        FillArrays();
        //UpdateCheckpoints();
        StartCoroutine(SpawnEnemies());
    }
    void Timer()
    {
        spawnTime -= Time.deltaTime;
    }
    void SetStartValues()
    {
        //Set the number of starting enemies
        numSharks = 50;
        numSerpents = 50;
        numShips = 50;
        
        //Update Lists after initiating the values
        UpdateLists();
    }
    //Set the Length of the Arrays
    void SetArrays()
    {
        SpawnPoints1 = new GameObject[3];
        SpawnPoints2 = new GameObject[3];
        SpawnPoints3 = new GameObject[3];
        Allenemies = new List<GameObject>();
    }

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

    //Fill the arrays with the spawn points
    void FillArrays()
    {
        //Fill the first array with spawn points tagged "SpawnPoint1"
        GameObject[] spawnPoints1 = GameObject.FindGameObjectsWithTag("SpawnPoint1");
        GameObject[] spawnPoints2 = GameObject.FindGameObjectsWithTag("SpawnPoint2");
        GameObject[] spawnPoints3 = GameObject.FindGameObjectsWithTag("SpawnPoint3");

        for(int i = 0; i < SpawnPoints1.Length && i < spawnPoints1.Length; i++)
        {
            SpawnPoints1[i] = spawnPoints1[i];
        }

        for(int i = 0; i < SpawnPoints2.Length && i < spawnPoints2.Length; i++)
        {
            SpawnPoints2[i] = spawnPoints2[i];
        }

        for(int i = 0; i < SpawnPoints3.Length && i < spawnPoints3.Length; i++)
        {
            SpawnPoints3[i] = spawnPoints3[i];
        }
    }
    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (spawnTime <= 0 && FirstCheckpoint)
            {
                SpawnEnemyGroup(SharkPrefab, 1, SpawnPoints1); // Only spawn one shark at a time
                spawnTime = 5f; 
            }
            if(spawnTime <= 0 && SecondCheckpoint)
            {
                SpawnEnemyGroup(SerpentPrefab, 1, SpawnPoints2);
                spawnTime = 5f;
            }
            if(spawnTime <= 0 && ThirdCheckpoint)
            {
                SpawnEnemyGroup(ShipPrefab, 1, SpawnPoints3);
                spawnTime = 5f;
            }
            yield return new WaitForSeconds(1f);
        }
    }

    void SpawnEnemyGroup(GameObject enemyPrefab, int numEnemies, GameObject[] spawnPoints)
    {
        for (int i = 0; i < numEnemies; i++)
        {
            Vector3 spawnpos = spawnPoints[currentSpawnPointIndex].transform.position;
            spawnpos.z = -2;
            GameObject newEnemy = Instantiate(enemyPrefab, spawnpos, Quaternion.identity);
            Allenemies.Add(newEnemy);
            currentSpawnPointIndex = (currentSpawnPointIndex + 1) % spawnPoints.Length;
        }
    }

    public void UpdateCheckpointStatus(int checkpointIndex, bool status)
    {
        Debug.Log("Checkpoint " + checkpointIndex + " status: " + status);
        switch (checkpointIndex)
        {
            case 0:
                FirstCheckpoint = status;
                break;
            case 1:
                SecondCheckpoint = status;
                break;
            case 2:
                ThirdCheckpoint = status;
                break;
            default:
                Debug.LogWarning("Invalid checkpoint index");
                break;
        }
    }
}
