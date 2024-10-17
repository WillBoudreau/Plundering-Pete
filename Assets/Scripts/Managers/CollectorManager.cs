using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectorManager : MonoBehaviour
{
    [Header("Class calls")]
    public LevelManager levelManager;
    [Header("Variables")]
    public GameObject Doubloon;
    public List<GameObject> Doubloons = new List<GameObject>();
    public int numofDoubloons;
    public int numofCoins;
    public bool hasSpawnedDoubloons; 

    // Start is called before the first frame update
    void Start()
    {
        numofDoubloons = 15;
        AddDoubloonsToList();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    void Update()
    {
        AddDoubloonsToList();
        SpawnDoubloons();
    }
    //Add doubloons to list
    void AddDoubloonsToList()
    {
        if (Doubloons.Count <= 0)
        {
            for (int i = 0; i < numofDoubloons; i++)
            {
                Doubloons.Add(Doubloon);
            }
        }
    }
    //Spawn Doubloons from list
    public void SpawnDoubloons()
    {
        Debug.Log("Spawned Doubloons");
        if (levelManager.levelName == "GameTestScene" && !hasSpawnedDoubloons)
        {
            for (int i = 0; i < numofDoubloons; i++)
            {
                Instantiate(Doubloons[i], new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), -2), Quaternion.identity);
            }
            hasSpawnedDoubloons = true; 
        }
    }
}
