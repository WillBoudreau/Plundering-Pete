using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectorManager : MonoBehaviour
{
    public LevelManager levelManager;
    public GameObject Doubloon;
    public List<GameObject> Doubloons = new List<GameObject>();
    public int numofDoubloons;
    public int numofCoins;
    private bool hasSpawnedDoubloons = false; 

    // Start is called before the first frame update
    void Start()
    {
        numofDoubloons = 15;
        AddDoubloonsToList();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    void Update()
    {
        SpawnDoubloons();
    }

    void AddDoubloonsToList()
    {
        if (Doubloons.Count == 0)
        {
            for (int i = 0; i < numofDoubloons; i++)
            {
                Doubloons.Add(Doubloon);
            }
        }
    }

    void SpawnDoubloons()
    {
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
