using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public struct ObstacleZone
    {
        public float Xnegative;
        public float Xpositive;
        public float Ynegative;
        public float Ypositive;
    }
    public ObstacleZone zone1, zone2, zone3;
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

    private List<Vector3> usedPositions = new List<Vector3>();

    void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        AddObstaclesToList();
        zone1 = new ObstacleZone{Xnegative = zone1Xnegative, Xpositive = zone1Xpositive, Ynegative = zone1Ynegative, Ypositive = zone1Ypositive};
        zone2 = new ObstacleZone{Xnegative = zone2Xnegative, Xpositive = zone2Xpositive, Ynegative = zone2Ynegative, Ypositive = zone2Ypositive};
        zone3 = new ObstacleZone{Xnegative = zone3Xnegative, Xpositive = zone3Xpositive, Ynegative = zone3Ynegative, Ypositive = zone3Ypositive};
    }

    void Update()
    {
        AddObstaclesToList();
    }

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
    public void SpawnObstaclesInZone(ObstacleZone zone,List<GameObject> obstacles,Transform playerTransform, float safeDistance)
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            Vector3 spawnPosition = GetSafeSpawnPos(zone.Xnegative, zone.Xpositive, zone.Ynegative, zone.Ypositive, playerTransform.position, safeDistance);
            Instantiate(obstacles[i], spawnPosition, Quaternion.identity);
            usedPositions.Add(spawnPosition);
        }
    }
    public Vector3 GetSpawnPos(float XNeg,float XPos,float YNeg,float YPos)
    {
        Vector3 spawnPos = new Vector3(Random.Range(XNeg, XPos), Random.Range(YNeg, YPos), -2);
        return spawnPos;
    }

    public Vector3 GetSafeSpawnPos(float XNeg, float XPos, float YNeg, float YPos, Vector3 playerPosition, float safeDistance)
    {
        Vector3 spawnPosition;
        do
        {
            spawnPosition = GetSpawnPos(XNeg, XPos, YNeg, YPos);
        } while (Vector3.Distance(spawnPosition, playerPosition) < safeDistance || IsPositionUsed(spawnPosition));
        return spawnPosition;
    }

    private bool IsPositionUsed(Vector3 position)
    {
        foreach (Vector3 usedPosition in usedPositions)
        {
            if (Vector3.Distance(position, usedPosition) < 1.0f) // Adjust the distance threshold as needed
            {
                return true;
            }
        }
        return false;
    }
}
