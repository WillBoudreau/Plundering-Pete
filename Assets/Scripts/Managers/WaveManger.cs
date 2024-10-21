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
    public float spawnTime = 0f;
    private int currentSpawnPointIndex = 0;
    public int SharkSpawnIndex;
    public int SerpentSpawnIndex;
    public int ShipSpawnIndex;

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
    public List<GameObject> Enemies = new List<GameObject>();
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
        StartCoroutine(SpawnEnemies());
        Timer();
        FillArrays();
        //UpdateCheckpoints();
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
                //Debug.Log("Spawning Sharks");
                SpawnEnemy(SharkPrefab, SpawnPoints1, ref SharkSpawnIndex);
                spawnTime = 5f;
                if (SecondCheckpoint)
                {
                    // Spawn 5 sharks before each serpent
                    for (int i = 0; i < 5; i++)
                    {
                        SpawnEnemy(SharkPrefab, SpawnPoints2, ref SerpentSpawnIndex);
                    }
                    SpawnEnemy(SerpentPrefab, SpawnPoints2, ref SerpentSpawnIndex);
                    spawnTime = 5f;
                }
                if (ThirdCheckpoint)
                {
                    for(int i = 0; i < 10; i++)
                    {
                        SpawnEnemy(SharkPrefab, SpawnPoints3, ref SharkSpawnIndex);
                    }
                    for(int i = 0; i < 5; i++)
                    {
                        SpawnEnemy(SerpentPrefab, SpawnPoints3, ref SerpentSpawnIndex);
                    }
                    SpawnEnemy(ShipPrefab, SpawnPoints3, ref ShipSpawnIndex);
                    spawnTime = 5f;
                }
            }
            yield return new WaitForSeconds(2f);
        }
    }

void SpawnEnemy(GameObject enemyPrefab, GameObject[] spawnPoints, ref int currentSpawnPointIndex)
{
    bool spawnSuccessful = false;
    int attempts = 0;

    while (!spawnSuccessful && attempts < spawnPoints.Length)
    {
        Vector3 spawnPos = spawnPoints[currentSpawnPointIndex].transform.position;
        spawnPos.z = -2;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPos, 5f); 
        bool isOccupied = false;

        foreach (Collider2D col in colliders)
        {
            if (col.gameObject.CompareTag("Enemy")) 
            {
                isOccupied = true;
                break;
            }
        }

        if (!isOccupied)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            Enemies.Add(enemy);
            spawnSuccessful = true; 
        }

        currentSpawnPointIndex = (currentSpawnPointIndex + 1) % spawnPoints.Length;
        attempts++;
    }

    if (!spawnSuccessful)
    {
        Debug.LogWarning("All spawn points are occupied. Enemy could not spawn.");
    }
}

    public void UpdateCheckpointStatus(int checkpointIndex, bool status)
    {
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
