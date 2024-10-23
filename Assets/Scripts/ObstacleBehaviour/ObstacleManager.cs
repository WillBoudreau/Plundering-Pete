using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [Header("Class Calls")]
    public LevelManager levelManager;
    [Header("Variables")]
    public GameObject Rock;
    public GameObject Iceberg;
    public GameObject Debris;
    public List<GameObject> obstacle_Rock = new List<GameObject>();
    public List<GameObject> obstacle_Iceberg = new List<GameObject>();
    public List<GameObject> obstacle_Debris = new List<GameObject>();
    public int obstacle_Rock_Count;
    public int obstacle_Iceberg_Count;
    public int obstacle_Debris_Count;
    public bool hasSpawnedRocks = false;
    public bool hasSpawnedIcebergs = false;
    public bool hasSpawnedDebris = false;

    // Start is called before the first frame update
    void Start()
    {
        obstacle_Rock_Count = 3;
        obstacle_Iceberg_Count = 3;
        obstacle_Debris_Count = 3;
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        AddObstaclesToList();
    }

    // Update is called once per frame
    void Update()
    {
        AddObstaclesToList();
    }
    //Add the Rocks to a list
    void AddObstaclesToList()
    {
        if (obstacle_Rock.Count <= 0)
        {
            for (int i = 0; i < obstacle_Rock_Count; i++)
            {
                obstacle_Rock.Add(Rock);
            }
        }
        if(obstacle_Iceberg.Count <= 0)
        {
            for (int i = 0; i < obstacle_Iceberg_Count; i++)
            {
                obstacle_Iceberg.Add(Iceberg);
            }
        }
        if(obstacle_Debris.Count <= 0)
        {
            for (int i = 0; i < obstacle_Debris_Count; i++)
            {
                obstacle_Debris.Add(Debris);
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
                Vector3 spawnPosition = GetSpawnPosition(playerTransform, safeDistance);
                Debug.Log("Spawning Rocks");
                Instantiate(obstacle_Rock[i], spawnPosition, Quaternion.identity);
            }
            hasSpawnedRocks = true; 
        }
    }
    public void spawnIceBergs(Transform playerTransform, float safeDistance)
    {
        //Spawn Icebergs
        if (levelManager.levelName == "GameTestScene" && !hasSpawnedIcebergs)
        {
            for (int i = 0; i < obstacle_Iceberg_Count; i++)
            {
                Vector3 spawnPosition = GetSpawnPosition(playerTransform, safeDistance);
                Debug.Log("Spawning Icebergs");
                Instantiate(obstacle_Iceberg[i], spawnPosition, Quaternion.identity);
            }
            hasSpawnedIcebergs = true;
        }
    }
    public void SpawnDebris(Transform playerTransform, float safeDistance)
    {
        //Spawn Debris
        if (levelManager.levelName == "GameTestScene" && !hasSpawnedDebris)
        {
            for (int i = 0; i < obstacle_Debris_Count; i++)
            {
                Vector3 spawnPosition = GetSpawnPosition(playerTransform, safeDistance);
                Debug.Log("Spawning Debris");
                Instantiate(obstacle_Debris[i], spawnPosition, Quaternion.identity);
            }
            hasSpawnedDebris = true;
        }
    }

    private Vector3 GetSpawnPosition(Transform playerTransform, float safeDistance)
    {
        Vector3 spawnPosition;
        do
        {
            spawnPosition = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), -2);
        } while (Vector3.Distance(spawnPosition, playerTransform.position) < safeDistance);
        return spawnPosition;
    }
}
