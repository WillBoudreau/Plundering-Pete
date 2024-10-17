using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public LevelManager levelManager;
    public GameObject Rock;
    public List<GameObject> obstacle_Rock = new List<GameObject>();
    public int obstacle_Rock_Count;
    public int obstacle_Iceberg_Count;
    public int obstacle_Debris_Count;
    private int currentRockCount = 0;
    public bool hasSpawnedRocks = false;

    // Start is called before the first frame update
    void Start()
    {
        obstacle_Rock_Count = 3;
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        AddRocksToList();
    }

    // Update is called once per frame
    void Update()
    {
        spawnRocks();
        AddRocksToList();
    }
    void AddRocksToList()
    {
        if (obstacle_Rock.Count <= 0)
        {
            for (int i = 0; i < obstacle_Rock_Count; i++)
            {
                obstacle_Rock.Add(Rock);
            }
        }
    }

    void spawnRocks()
    {
        Debug.Log("Spawned Rocks");
        if (levelManager.levelName == "GameTestScene" && !hasSpawnedRocks)
        {
            for (int i = 0; i < obstacle_Rock_Count; i++)
            {
                Instantiate(obstacle_Rock[i], new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), -2), Quaternion.identity);
            }
            hasSpawnedRocks = true; 
        }
    }
}
