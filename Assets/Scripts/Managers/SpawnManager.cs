using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameObject Camera;
    [Header("Spawns")]
    public Rect spawnArea1;
    public Rect spawnArea2;
    public Rect spawnArea3;
    public Transform playerSpawnPoint; 
    public GameObject player;
    private const float PlayerZPOS = -2f;
    private const float CameraZPOS = -30f;
    void Update()
    {
        if(levelManager.levelName == "GameTestScene")
        {
            FindSpawnPointInScene();
        }
    }
    public void PlacePlayer()
    {
        Vector3 playerPosition = playerSpawnPoint.position;
        playerPosition.z = PlayerZPOS;
        player.transform.position = playerPosition;

        Vector3 cameraPosition = playerSpawnPoint.position;
        cameraPosition.z =  CameraZPOS;
        Camera.transform.position = cameraPosition;
    }

    public void PlacePlayerAtSpawn()
    {
        Transform spawnPoint = FindSpawnPointInScene();
        if(spawnPoint != null)
        {
            PlacePlayer();
        }
        else
        {
            Debug.Log("Spawn point not found");
        }
    }

    Transform FindSpawnPointInScene()
    {
        GameObject spawnPoint = GameObject.Find("SpawnPoint");
        playerSpawnPoint = spawnPoint.transform;
        if(spawnPoint != null)
        {
            return spawnPoint.transform;
        }
        return null;
    }
    Vector3 GetEnemySpawn(Rect spawnArea)
    {
        Vector3 spawnPoint = new Vector3();
        spawnPoint.x = Random.Range(spawnArea.xMin, spawnArea.xMax);
        spawnPoint.y = Random.Range(spawnArea.yMin, spawnArea.yMax);
        spawnPoint.z = 0;
        return spawnPoint;
    }
    public void SpawnEnemyInRect(GameObject enemyPrefab, Rect spawnArea)
    {
        bool spawnSuccessful = false;
        int attempts = 0;
        while(!spawnSuccessful && attempts < 10)
        {
            Debug.Log("Spawning enemy");
            Vector3 spawnPos = GetEnemySpawn(spawnArea);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPos, 1.0f);
            bool isOccupied = false;
            foreach(var collider in colliders)
            {
                if(collider.gameObject.tag == "Shark")
                {
                    isOccupied = true;
                    break;
                }
            }
            if(!isOccupied)
            {
                GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
                // enemies.Add(enemy);
                spawnSuccessful = true;
            }
            attempts++;
        }
        if(!spawnSuccessful)
        {
            Debug.Log("Failed to spawn enemy");
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(spawnArea1.x, spawnArea1.y, 0), new Vector3(spawnArea1.width, spawnArea1.height, 0));
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3(spawnArea2.x, spawnArea2.y, 0), new Vector3(spawnArea2.width, spawnArea2.height, 0));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3(spawnArea3.x, spawnArea3.y, 0), new Vector3(spawnArea3.width, spawnArea3.height, 0));
    }
}
