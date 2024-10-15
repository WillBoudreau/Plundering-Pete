using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManger : MonoBehaviour
{
    //Class calls
    [Header("Classes")]
    public EnemyManager enemMan;
    public LevelManager levelMan;


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

    //List of Spawn points
    public GameObject[] SpawnPoints1;
    public GameObject[] SpawnPoints2;
    public GameObject[] SpawnPoints3;
    public GameObject[][] Spawns;

    // Start is called before the first frame update
    void Start()
    {
        SetStartValues();
        SetArrays();
        FillArrays();
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
        SpawnPoints2 = new GameObject[3];
        SpawnPoints3 = new GameObject[3];
        Spawns = new GameObject[3][];

        // Initialize the master array with the individual arrays
        Spawns[0] = SpawnPoints1;
        Spawns[1] = SpawnPoints2;
        Spawns[2] = SpawnPoints3;
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

        //Fill the second array with spawn points tagged "SpawnPoint2"
        GameObject[] spawnPoints2 = GameObject.FindGameObjectsWithTag("SpawnPoint2");
        for(int i = 0; i < SpawnPoints2.Length && i < spawnPoints2.Length; i++)
        {
            SpawnPoints2[i] = spawnPoints2[i];
        }

        //Fill the third array with spawn points tagged "SpawnPoint3"
        GameObject[] spawnPoints3 = GameObject.FindGameObjectsWithTag("SpawnPoint3");
        for(int i = 0; i < SpawnPoints3.Length && i < spawnPoints3.Length; i++)
        {
            SpawnPoints3[i] = spawnPoints3[i];
        }

        // Fill the master array with each of the other arrays
        Spawns[0] = SpawnPoints1;
        Spawns[1] = SpawnPoints2;
        Spawns[2] = SpawnPoints3;
    }
}
