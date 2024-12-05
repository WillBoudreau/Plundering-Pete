using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    //Struct for the obstacle zones
    public struct ObstacleZone
    {
        public float Xnegative;
        public float Xpositive;
        public float Ynegative;
        public float Ypositive;
    }
    public ObstacleZone zone1, zone2, zone3;
    [Header("Object Calls")]
    [SerializeField] private Camera mainCamera;
    [Header("Variables")]
    public GameObject Rock;
    public GameObject Iceberg;
    public GameObject Debris;
    public float takenPOS = 3.0f;
    public float safeDistance = 10.0f;
    public int currentRockCount = 0;
    public int RockMaxZone1 = 10;
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
    [Header("Lists")]
    public List<GameObject> Zone1Obstacles = new List<GameObject>();
    public List<GameObject> Zone2Obstacles = new List<GameObject>();
    public List<GameObject> Zone3Obstacles = new List<GameObject>();
    public List<Vector3> usedPositions = new List<Vector3>();
    public int obstacle_Rock_Count;
    public int obstacle_Iceberg_Count;
    public int obstacle_Debris_Count;
    public int IcebergMax = 10;
    public int DebrisMax = 10;
    //List of occupied positions

    void Start()
    {
        zone1 = new ObstacleZone{Xnegative = zone1Xnegative, Xpositive = zone1Xpositive, Ynegative = zone1Ynegative, Ypositive = zone1Ypositive};
        zone2 = new ObstacleZone{Xnegative = zone2Xnegative, Xpositive = zone2Xpositive, Ynegative = zone2Ynegative, Ypositive = zone2Ypositive};
        zone3 = new ObstacleZone{Xnegative = zone3Xnegative, Xpositive = zone3Xpositive, Ynegative = zone3Ynegative, Ypositive = zone3Ypositive};
    }
    //Adding obstacles to their respective lists
    public void AddObstaclesToList()
    {
        //Spawns rocks in zone 1
        if (Zone1Obstacles.Count <= RockMaxZone1 && currentRockCount < RockMaxZone1)
        {
            int rocksToSpawn  = Mathf.Min(RockMaxZone1 - Zone1Obstacles.Count, RockMaxZone1 - currentRockCount);
            for (int i = 0; i < obstacle_Rock_Count; i++)
            {
                Zone1Obstacles.Add(Rock);
            }
        }
        //Zone 2 only spawns rocks and icebergs
        if(Zone2Obstacles.Count <= 0)
        {
            for(int i = 0; i < obstacle_Rock_Count; i++)
            {
                Zone2Obstacles.Add(Rock);
            }
            for (int i = 0; i < obstacle_Iceberg_Count; i++)
            {
                Zone2Obstacles.Add(Iceberg);
            }
        }
        //Zone 3 spawns all obstacles
        if(Zone3Obstacles.Count <= 0)
        {
            for (int i = 0; i < obstacle_Rock_Count; i++)
            {
                Zone3Obstacles.Add(Rock);
            }
            for (int i = 0; i < obstacle_Iceberg_Count; i++)
            {
                Zone3Obstacles.Add(Iceberg);
            }
            for (int i = 0; i < obstacle_Debris_Count; i++)
            {
                Zone3Obstacles.Add(Debris);
            }
        }
    }
    //Spawn obstacles in the zone that is passed in
    public void SpawnObstaclesInZone(ObstacleZone zone,List<GameObject> obstacles,Transform playerTransform, float safeDistance)
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            Vector3 spawnPosition = GetSafeSpawnPos(zone.Xnegative, zone.Xpositive, zone.Ynegative, zone.Ypositive, playerTransform.position, safeDistance);
            if(obstacles[i] == Rock && currentRockCount >= RockMaxZone1)
            {
                continue;
            }
            if(obstacles[i] == Rock)
            {
                currentRockCount++;
            }
            Instantiate(obstacles[i], spawnPosition, Quaternion.identity);
            usedPositions.Add(spawnPosition);
            obstacles[i].SetActive(true);
        }
    }
    //Finds position on map
    public Vector3 GetSpawnPos(float XNeg,float XPos,float YNeg,float YPos)
    {
        Vector3 spawnPos = new Vector3(Random.Range(XNeg, XPos), Random.Range(YNeg, YPos), -2);
        return spawnPos;
    }
    //Runs a check to make sure that the position on the map is safe
    public Vector3 GetSafeSpawnPos(float XNeg, float XPos, float YNeg, float YPos, Vector3 playerPosition, float safeDistance)
    {
        Vector3 spawnPosition;
        int attempts = 0;
        int maxAttempts = 10;
        do
        {
            spawnPosition = GetSpawnPos(XNeg, XPos, YNeg, YPos);
            attempts++;
        } while ((Vector3.Distance(spawnPosition, playerPosition) < safeDistance || IsPositionUsed(spawnPosition) || IsInCameraView(spawnPosition)) && attempts < maxAttempts);
        Debug.Log("Attempts: " + attempts);
        return spawnPosition;
    }
    //For if the position is occupied pick another
    private bool IsPositionUsed(Vector3 position)
    {
        foreach (Vector3 usedPosition in usedPositions)
        {
            if (Vector3.Distance(position, usedPosition) < takenPOS) 
            {
                return true;
            }
        }
        return false;
    }
    //Check if the position is within the camera's view
    private bool IsInCameraView(Vector3 position)
    {
        Vector3 viewportPoint = mainCamera.WorldToViewportPoint(position);
        bool InView = viewportPoint.x > 0 && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 1;
        return InView;
    }
}
