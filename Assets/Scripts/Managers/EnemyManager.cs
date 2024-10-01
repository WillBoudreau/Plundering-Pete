using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject Shark;
    public GameObject SharkSpawn;
    public GameObject[] enemySpawn;

    public float spawnTime;

    public int NumSharks;

    public List<Transform> spawnPoints = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        NumSharks = 1;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime -= Time.deltaTime;
        FindSpawn();
        spawnEnemy();
    }
    void FindSpawn()
    {
        enemySpawn = GameObject.FindGameObjectsWithTag("EnemySpawn");
        foreach (GameObject enemspawn in enemySpawn)
        {
            spawnPoints.Add(enemspawn.transform);
        }
    }
    void spawnEnemy()
    {
        if(spawnTime <= 0)
        {
            for(int i = 0; i < NumSharks; i++)
            {
                foreach (GameObject enemspawn in enemySpawn)
                {
                    Vector3 spawnPosition = enemspawn.transform.position;
                    spawnPosition.z = -2; 
                    Instantiate(Shark, spawnPosition, Quaternion.identity);
                }
            }
            spawnTime = 5;  
        }
    }
}
