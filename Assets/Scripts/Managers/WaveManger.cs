using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManger : MonoBehaviour
{

    //Class calls
    [Header("Classes")]
    public EnemyManager enemMan;

    public List<GameObject> Sharks;
    public List<GameObject> Serpents;
    public List<GameObject> Ships;

    public GameObject SharkPrefab;
    public GameObject SerpentPrefab;
    public GameObject ShipPrefab;

    public int numSharks;
    public int numSerpents;
    public int numShips;


    // Start is called before the first frame update
    void Start()
    {
        SetStartValues();
    }

    // Update is called once per frame
    void Update()
    {
         
    }
    void SetStartValues()
    {
        //Set the number of starting enemies
        int StartNumSharks = 50;
        int StartNumSerpents = 0;
        int StartNumShips = 0;

        numSharks = StartNumSharks;
        numSerpents = StartNumSerpents;
        numSharks = StartNumShips;
        
        //Update Lists after initiating the values
        UpdateLists();
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
