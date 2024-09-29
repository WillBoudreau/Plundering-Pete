using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public LevelManager levelManager;
    public GameObject[] obstacle_Rock;
    public GameObject[] obstacle_Iceberg;
    public GameObject[] obstacle_Debris;
    public int obstacle_Rock_Count;
    public int obstacle_Iceberg_Count;
    public int obstacle_Debris_Count;
    private int currentRockCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        obstacle_Debris_Count = 1;
        obstacle_Iceberg_Count = 1;
        obstacle_Rock_Count = 1;
    }

    // Update is called once per frame
    void Update()
    {
        spawnObstacles();
    }

    public void spawnObstacles()
    {
        if(levelManager.levelName == "GameTestScene")
        {
            spawnRocks();
        }
    }

    void spawnRocks()
    {
        while (currentRockCount < obstacle_Rock_Count)
        {
            Instantiate(obstacle_Rock[Random.Range(0, obstacle_Rock.Length)], new Vector3(Random.Range(-10,10), Random.Range(-10,10), -2), Quaternion.identity);
            currentRockCount++;
        }
    }
}
