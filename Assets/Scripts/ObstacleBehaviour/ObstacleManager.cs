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
    [Header("Obstacle Zones")]
    [Header("Zone 1")]
    public float zone1Xnegative = -15;
    public float zone1Xpositive = 15;
    public float zone1Ynegative = -15;
    public float zone1Ypositive = 15;
    [Header("Zone 2")]
    public float zone2Xnegative = -30;
    public float zone2Xpositive = 30;
    public float zone2Ynegative = -30;
    public float zone2Ypositive = 30;
    [Header("Zone 3")]
    public float zone3Xnegative = -45;
    public float zone3Xpositive = 45;
    public float zone3Ynegative = -45;
    public float zone3Ypositive = 45;
    public List<GameObject> obstacle_Rock = new List<GameObject>();
    public List<GameObject> obstacle_Iceberg = new List<GameObject>();
    public List<GameObject> obstacle_Debris = new List<GameObject>();
    public int obstacle_Rock_Count;
    public int obstacle_Iceberg_Count;
    public int obstacle_Debris_Count;
    public bool hasSpawnedRocks;
    public bool hasSpawnedIcebergs;
    public bool hasSpawnedDebris;

    // Start is called before the first frame update
    void Start()
    {
        // obstacle_Rock_Count = 3;
        // obstacle_Iceberg_Count = 3;
        // obstacle_Debris_Count = 3;
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
    public void spawnZone1Obstacles(float XNeg,float XPos,float YNeg,float YPos,Transform playerTransform, float safeDistance)
    {
        if (levelManager.levelName == "GameTestScene" && !hasSpawnedRocks)
        {
            for (int i = 0; i < obstacle_Rock_Count; i++)
            {
                Vector3 spawnPosition = GetSafeSpawnPos(XNeg, XPos, YNeg, YPos, playerTransform.position, safeDistance);
                Instantiate(obstacle_Rock[i], spawnPosition, Quaternion.identity);
            }
            hasSpawnedRocks = true; 
        }
    }
    public void spawnZone2Obstacles(float XNeg,float XPos,float YNeg,float YPos,Transform playerTransform, float safeDistance)
    {
        //Spawn Icebergs
        if (levelManager.levelName == "GameTestScene" && !hasSpawnedIcebergs)
        {
            for(int i = 0; i < obstacle_Rock_Count; i++)
            {
                Vector3 spawnPosition = GetSafeSpawnPos(XNeg, XPos, YNeg, YPos, playerTransform.position, safeDistance);
                Instantiate(obstacle_Rock[i], spawnPosition, Quaternion.identity);
            }
            for (int i = 0; i < obstacle_Iceberg_Count; i++)
            {
                Vector3 spawnPosition = GetSafeSpawnPos(XNeg, XPos, YNeg, YPos, playerTransform.position, safeDistance);
                Instantiate(obstacle_Iceberg[i], spawnPosition, Quaternion.identity);
            }
            hasSpawnedIcebergs = true;
            hasSpawnedRocks = true;
        }
    }
    public void spawnZone3Obstacles(float XNeg,float XPos,float YNeg, float YPos,Transform playerTransform, float safeDistance)
    {
        //Spawn Debris
        if (levelManager.levelName == "GameTestScene" && !hasSpawnedDebris)
        {
            for (int i = 0; i < obstacle_Debris_Count; i++)
            {
                Vector3 spawnPosition = GetSafeSpawnPos(XNeg, XPos, YNeg, YPos, playerTransform.position, safeDistance);
                Instantiate(obstacle_Debris[i], spawnPosition, Quaternion.identity);
            }
            for (int i = 0; i < obstacle_Rock_Count; i++)
            {
                Vector3 spawnPosition = GetSafeSpawnPos(XNeg, XPos, YNeg, YPos, playerTransform.position, safeDistance);
                Instantiate(obstacle_Rock[i], spawnPosition, Quaternion.identity);
            }
            for (int i = 0; i < obstacle_Iceberg_Count; i++)
            {
                Vector3 spawnPosition = GetSafeSpawnPos(XNeg, XPos, YNeg, YPos, playerTransform.position, safeDistance);
                Instantiate(obstacle_Iceberg[i], spawnPosition, Quaternion.identity);
            }
            hasSpawnedDebris = true;
            hasSpawnedRocks = true;
            hasSpawnedIcebergs = true;
        }
    }
    //Get the spawn position for the obstacles
    public Vector3 GetSpawnPos(float XNeg,float XPos,float YNeg,float YPos)
    {
        Vector3 spawnPos = new Vector3(Random.Range(XNeg, XPos), Random.Range(YNeg, YPos), -2);
        return spawnPos;
    }

    // Get a safe spawn position that is not too close to the player
    public Vector3 GetSafeSpawnPos(float XNeg, float XPos, float YNeg, float YPos, Vector3 playerPosition, float safeDistance)
    {
        Vector3 spawnPosition;
        do
        {
            spawnPosition = GetSpawnPos(XNeg, XPos, YNeg, YPos);
        } while (Vector3.Distance(spawnPosition, playerPosition) < safeDistance);
        return spawnPosition;
    }
}
