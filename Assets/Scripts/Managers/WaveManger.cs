using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManger : MonoBehaviour
{

    //Class calls
    [Header("Classes")]
    public EnemyManager enemMan;


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
    public GameObject[] Spawns;


    // Start is called before the first frame update
    void Start()
    {
        SetStartValues();
        SetArrays();
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
        Spawns = new GameObject[3];

        //Set each individual array
        //Set Array 1
        SpawnPoints1[0] = new GameObject();
        SpawnPoints1[1] = new GameObject();
        SpawnPoints1[2] = new GameObject();

        //Set Array2
        SpawnPoints2[0] = new GameObject();
        SpawnPoints2[1] = new GameObject();
        SpawnPoints2[2] = new GameObject();

        //Set Array3
        SpawnPoints3[0] = new GameObject();
        SpawnPoints3[1] = new GameObject();
        SpawnPoints3[2] = new GameObject();

        //Set the master array
        //Spawns[0] = new GameObject[SpawnPoints1];
        //Spawns[1] = new GameObject[SpawnPoints2];
        //Spawns[2] = new GameObject[SpawnPoints3];
    }
    void UpdateLists()
    {
        //Update Lists based off values from the num variables
        for(int i = 0; i <= numSharks; i++)
        {
            Sharks.Add(SharkPrefab);
        }
        for(int i = 0; i <= numSerpents; i++)
        {
            Serpents.Add(SerpentPrefab);
        }
        for(int i = 0; i <= numShips; i++)
        {
            Ships.Add(ShipPrefab);
        }
    }
}
