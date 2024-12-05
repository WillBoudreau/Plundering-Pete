using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private DistanceTracker distanceTracker;
    [SerializeField] private GameObject Camera;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private CheckpointManager checkPointManager;
    [Header("Spawns")]
    public Rect spawnArea1;
    public Rect spawnArea2;
    public Rect spawnArea3;
    public Rect PlayerSpawnArea;
    public Rect ShipSpawnArea;
    public Transform playerSpawnPoint; 
    public GameObject player;
    private const float PlayerZPOS = -2f;
    private const float CameraZPOS = -30f;
    float spawnZone = 10.0f;
    void Update()
    {
        if(levelManager.levelName == "GameTestScene")
        {
            FindSpawnPointInScene();
        }
    }
    //Place the player at the spawn point coordinates
    public void PlacePlayer()
    {
        Vector3 playerPosition = playerSpawnPoint.position;
        if(!IsValidPlayerSpawn(playerPosition))
        {
            Debug.Log("Invalid player spawn point");
            playerPosition.x = Mathf.Clamp(playerPosition.x, PlayerSpawnArea.xMin, PlayerSpawnArea.xMax);
            playerPosition.y = Mathf.Clamp(playerPosition.y, PlayerSpawnArea.yMin, PlayerSpawnArea.yMax);
        }
        else if(IsValidPlayerSpawn(playerPosition))
        {
            if(IsOnCheckpoint(playerPosition))
            {
                playerPosition.y -= 10;
            }
            else if(!IsOnCheckpoint(playerPosition))
            {
                 //Vector3 playerPosition = playerSpawnPoint.position;
                playerPosition.z = PlayerZPOS;
                player.transform.position = playerPosition;

                Vector3 cameraPosition = playerSpawnPoint.position;
                cameraPosition.z =  CameraZPOS;
                Camera.transform.position = cameraPosition;

                distanceTracker.ResetValues();
            }
        }
    }
    bool IsOnCheckpoint(Vector3 position)
    {
        float tolerance = 10f;
        return Mathf.Abs(position.y - checkPointManager.Checkpoint1) < tolerance || 
        Mathf.Abs(position.y - checkPointManager.Checkpoint2) < tolerance || 
        Mathf.Abs(position.y - checkPointManager.Checkpoint3) < tolerance || 
        Mathf.Abs(position.y - checkPointManager.Checkpoint4) < tolerance;
    }
    //Place the player at the spawn point
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
    private bool IsValidPlayerSpawn(Vector3 spawnPoint)
    {
        Rect validSpawnArea = PlayerSpawnArea;

        Vector3 originalPosition = spawnPoint;

        spawnPoint.x = Mathf.Clamp(spawnPoint.x, validSpawnArea.xMin, validSpawnArea.xMax);
        spawnPoint.y = Mathf.Clamp(spawnPoint.y, validSpawnArea.yMin, validSpawnArea.yMax);

        if(spawnPoint != originalPosition)
        {
            return false;
        }

        return validSpawnArea.Contains(new Vector2(spawnPoint.x, spawnPoint.y));
    }
    //Find the spawn point in the scene
    Transform FindSpawnPointInScene()
    {
        GameObject spawnPoint = GameObject.Find("SpawnPoint");
        playerSpawnPoint = spawnPoint.transform;
        if(spawnPoint != null)
        {
            Vector3 potentialSpawn = spawnPoint.transform.position;
            if(IsValidPlayerSpawn(potentialSpawn))
            {
                playerSpawnPoint.position = potentialSpawn;
                return spawnPoint.transform;
            }
        }
        if(spawnPoint == null)
        {
            Debug.Log("Spawn point not found");
        }
        return null;
    }
    //Get a random spawn point in the specified area
    Vector3 GetEnemySpawn(Rect spawnArea)
    {
        Vector3 spawnPoint = new Vector3();
        spawnPoint.x = Random.Range(spawnArea.xMin, spawnArea.xMax);
        spawnPoint.y = Random.Range(spawnArea.yMin, spawnArea.yMax);
        spawnPoint.z = 0;
        return spawnPoint;
    }
    //Spawn an enemy in the specified area
    public void SpawnEnemyInRect(GameObject enemyPrefab, Rect spawnArea)
    {
        bool spawnSuccessful = false;
        int attempts = 0;
        while(!spawnSuccessful && attempts < 10)
        {
            Debug.Log("Spawning enemy");
            if(enemyPrefab.tag == "EnemyShip")
            {
                spawnArea = ShipSpawnArea;
            }
            Vector3 spawnPos = GetEnemySpawn(spawnArea);
            if(IsInCameraView(spawnPos))
            {
                Debug.Log("Enemy is in camera view");
                attempts++;
            }
            else if(!IsInCameraView(spawnPos))
            {
                
                Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPos, spawnZone);
                bool isOccupied = false;
                foreach(var collider in colliders)
                {
                    if(collider.gameObject.tag == "Shark")
                    {
                        isOccupied = true;
                        break;
                    }
                    else if(collider.gameObject.tag == "Serpent")
                    {
                        isOccupied = true;
                        break;
                    }
                    else if(collider.gameObject.tag == "EnemyShip")
                    {
                        isOccupied = true;
                        break;
                    }
                    else if(collider.gameObject.tag == "Player")
                    {
                        isOccupied = true;
                        break;
                    }
                    else if(Vector3.Distance(player.transform.position, spawnArea.center) < spawnZone)
                    {
                        Debug.Log("Player is too close to spawn point");
                        isOccupied = true;
                    }
                }
                if(!isOccupied)
                {
                    GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
                    spawnSuccessful = true;
                }
                attempts++;
            }
            if(!spawnSuccessful)
            {
                Debug.Log("Failed to spawn enemy");
            }
        }
    }
    bool IsInCameraView(Vector3 position)
    {
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(position);
        bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        return onScreen;
    }
    //Draw the spawn areas in the editor
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(spawnArea1.x, spawnArea1.y, 0), new Vector3(spawnArea1.width, spawnArea1.height, 0));
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3(spawnArea2.x, spawnArea2.y, 0), new Vector3(spawnArea2.width, spawnArea2.height, 0));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3(spawnArea3.x, spawnArea3.y, 0), new Vector3(spawnArea3.width, spawnArea3.height, 0));
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector3(ShipSpawnArea.x, ShipSpawnArea.y, 0), new Vector3(ShipSpawnArea.width, ShipSpawnArea.height, 0));
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(new Vector3(PlayerSpawnArea.x, PlayerSpawnArea.y, 0), new Vector3(PlayerSpawnArea.width, PlayerSpawnArea.height, 0));
    }
}
