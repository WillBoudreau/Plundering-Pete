using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManger : MonoBehaviour
{
    //Class calls
    [Header("Classes")]

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
    [Header("Lists")]
    //List of Spawn points
    public GameObject[] SpawnPoints1;
    public void SetAll()
    {
        SetStartValues();
        SetArrays();
    }
    void FixedUpdate()
    {
        Timer();
        FillArrays();
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
        numSerpents = 0;
        numShips = 0;

        Debug.Log("Number of Sharks: " + numSharks);
        
        //Update Lists after initiating the values
        UpdateLists();
    }
    //Set the Length of the Arrays
    void SetArrays()
    {
        SpawnPoints1 = new GameObject[3];
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
        for(int i = 0; i < SpawnPoints1.Length && i < spawnPoints1.Length; i++)
        {
            SpawnPoints1[i] = spawnPoints1[i];
        }
    }
    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (spawnTime <= 0 && !FirstCheckpoint)
            {
                for (int i = 0; i < numSharks; i++)
                {
                    foreach (GameObject spawn in SpawnPoints1)
                    {
                        Vector3 spawnpos = spawn.transform.position;
                        spawnpos.z = -2;
                        Instantiate(SharkPrefab, spawnpos, Quaternion.identity);
                    }
                }
                spawnTime = 5f; 
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
