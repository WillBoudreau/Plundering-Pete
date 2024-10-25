using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectorManager : MonoBehaviour
{
    [Header("Class calls")]
    [SerializeField] private LevelManager levelManager;
    [Header("Variables")]
    [SerializeField] private GameObject Doubloon;
    [SerializeField] private List<GameObject> Doubloons = new List<GameObject>();

    [SerializeField] private int  numofDoubloons;
    public bool hasSpawnedDoubloons; 
    [SerializeField] private Vector2 mapMinBounds;
    [SerializeField] private Vector2 mapMaxBounds;

    // Start is called before the first frame update
    void Start()
    {
        numofDoubloons = 10;
        AddDoubloonsToList();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    void Update()
    {
        AddDoubloonsToList();
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
    public void SpawnDoubloons(Transform playerTransform, float safeDistance)
    {
        if (levelManager.levelName == "GameTestScene" && !hasSpawnedDoubloons)
        {
            for (int i = 0; i < numofDoubloons; i++)
            {
                Vector3 spawnPosition = GetRandomSpawnPosition();
                if (Vector3.Distance(spawnPosition, playerTransform.position) < safeDistance)
                {
                    spawnPosition = GetRandomSpawnPosition();
                }
                else if(spawnPosition == Doubloons[i].transform.position)
                {
                    spawnPosition = GetRandomSpawnPosition();
                }
                else
                {
                    Instantiate(Doubloons[i], spawnPosition, Quaternion.identity);
                }
            }
            hasSpawnedDoubloons = true; 
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        float x = Random.Range(mapMinBounds.x, mapMaxBounds.x);
        float y = Random.Range(mapMinBounds.y, mapMaxBounds.y);
        return new Vector3(x, y, -2);
    }
}
