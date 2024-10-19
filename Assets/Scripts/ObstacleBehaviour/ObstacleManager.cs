using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [Header("Class Calls")]
    public LevelManager levelManager;
    [Header("Variables")]
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
        AddRocksToList();
    }
    //Add the Rocks to a list
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
    //Spawn Rocks dependent on how many in the array
    public void spawnRocks(Transform playerTransform, float safeDistance)
    {
        if (levelManager.levelName == "GameTestScene" && !hasSpawnedRocks)
        {
            for (int i = 0; i < obstacle_Rock_Count; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), -2);
                if (Vector3.Distance(spawnPosition, playerTransform.position) < safeDistance)
                {
                    spawnPosition = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), -2);
                }
                else
                {
                    Debug.Log("Spawning Rocks");
                    Instantiate(obstacle_Rock[i], spawnPosition, Quaternion.identity);
                }
            }
            hasSpawnedRocks = true; 
        }
    }
}
