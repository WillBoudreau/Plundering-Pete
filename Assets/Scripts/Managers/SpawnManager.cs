using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameObject Camera;
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
}
