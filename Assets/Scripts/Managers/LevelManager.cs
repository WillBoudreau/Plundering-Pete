using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [Header("Level Manager")]
    [Header("Class Calls")]
    [SerializeField] private CollectorManager collectorManager;
    [SerializeField] private MusicChanger musicChanger;
    [SerializeField] private CheckpointManager checkpointManager;  
    [SerializeField] private ObstacleManager obstacleManager;
    [SerializeField] private PlayerBehaviour player;
    [SerializeField] private SpawnManager spawnManager;
    [Header("Loading Screen")]
    public List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();
    [Header("Level Variables")]
    public string levelName;
 
    public float safeDistance = 5f;
    [SerializeField] private bool[] hasSpawnedZone = new bool[3];


    // Update is called once per frame
    void Update()
    {
        levelName = SceneManager.GetActiveScene().name;
    }

    //Update the zone obstacles
    public void UpdateZoneObstacles()
    {
        hasSpawnedZone[0] = checkpointManager.FirstCheckpoint;
        hasSpawnedZone[1] = checkpointManager.SecondCheckpoint;
        hasSpawnedZone[2] = checkpointManager.ThirdCheckpoint;
    }

    //Load the level
    public void LoadLevel(string name)
    {
        StartCoroutine(WaitForSceneToLoadAndRespawn(name));
    }

    //Reset the level
    void ResetLevel()
    {
        checkpointManager.SetFalse();
        // UpdateZoneObstacles();
        collectorManager.hasSpawnedDoubloons = false;
    }

    //Quit the game
    public void QuitRequest()
    {
        Debug.Log("Quit requested");
        Application.Quit();
    }

    //Wait for the scene to load and respawn
    private IEnumerator WaitForSceneToLoadAndRespawn(string SceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneName);
        yield return new WaitUntil(() => operation.isDone);
        ClearObstacles();
        ResetLevel();
        UpdateZoneObstacles();
        //UpdateObjects();
        spawnManager.PlacePlayerAtSpawn();
        musicChanger.PlaySceneTrack(SceneManager.GetActiveScene().name);
    } 

    //Spawn the objects in the scene  
    void SpawnObjects()
    {
        if(levelName == "GameTestScene")
        {
            obstacleManager.AddObstaclesToList();
            if(checkpointManager.FirstCheckpoint == true)
            {
                //Spawn Doubloons and obstacles for the first part of the level
                collectorManager.SpawnDoubloons(player.transform, safeDistance);
                SpawnObstaclesInZone(obstacleManager.zone1,obstacleManager.Zone1Obstacles);
                hasSpawnedZone[0] = true;
            }
            else if(checkpointManager.SecondCheckpoint == true && !hasSpawnedZone[1])
            {
                //Spawn Obstacles for the second part of the level
                SpawnObstaclesInZone(obstacleManager.zone2,obstacleManager.Zone2Obstacles);
                hasSpawnedZone[1] = true;
            }
            else if(checkpointManager.ThirdCheckpoint == true && !hasSpawnedZone[2])
            {
                //Spawn obstacles for the third part of the level
                SpawnObstaclesInZone(obstacleManager.zone3,obstacleManager.Zone3Obstacles);
                hasSpawnedZone[2] = true;
            }
        }
    }

    //Spawn the obstacles in the zone
    void SpawnObstaclesInZone(ObstacleManager.ObstacleZone zone, List<GameObject> obstacles)
    {
        //Spawn obstacles in the appropriate zone
        obstacleManager.SpawnObstaclesInZone(zone,obstacles,player.transform,safeDistance);
    }


    //Update the objects in the scene
    public void UpdateObjects()
    {
        //Update the objects in the scene
        if(checkpointManager.FirstCheckpoint == true && !hasSpawnedZone[0])
        {
            SpawnObjects();
        }
        else if (checkpointManager.SecondCheckpoint == true && !hasSpawnedZone[1])
        {
            SpawnObjects();
        }
        else if (checkpointManager.ThirdCheckpoint == true && !hasSpawnedZone[2])
        {
            SpawnObjects();
        }
    }

    //Clear the obstacles
    void ClearObstacles()
    {
        obstacleManager.Zone1Obstacles.Clear();
        obstacleManager.Zone2Obstacles.Clear();
        obstacleManager.Zone3Obstacles.Clear();
    }
}