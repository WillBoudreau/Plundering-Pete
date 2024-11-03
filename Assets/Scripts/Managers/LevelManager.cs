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
    [SerializeField] private UIManager uiManager;
    [SerializeField] private ObstacleManager obstacleManager;
    [SerializeField] private PlayerBehaviour player;
    [SerializeField] private GameObject Camera;
    [Header("Loading Screen")]
    public List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();
    [Header("Level Variables")]
    public string levelName;
    public Transform playerSpawnPoint; 
    public float safeDistance = 5f;
    public bool hasSpawnedZone1Obstacles;
    public bool hasSpawnedZone2Obstacles;
    public bool hasSpawnedZone3Obstacles;


    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        levelName = SceneManager.GetActiveScene().name;
        playerSpawnPoint = GameObject.Find("PlayerSpawnPoint").transform;
    }
    public void LoadLevel(string name)
    {
        Debug.Log("Level load requested for: " + name);
        SceneManager.LoadScene(name);

        //PlacePlayer();
        if(name == "GameTestScene")
        {
            obstacleManager.AddObstaclesToList();
            StartCoroutine(WaitForSceneToLoadAndRespawn());
            musicChanger.PlaySceneTrack(name);
        }
        else if(name == "MainMenuScene")
        {
            obstacleManager.Zone1Obstacles.Clear();
            obstacleManager.Zone2Obstacles.Clear();
            obstacleManager.Zone3Obstacles.Clear();
        }
        else
        {
            musicChanger.PlaySceneTrack(name);
        }

    }

    public void QuitRequest()
    {
        Debug.Log("Quit requested");
        Application.Quit();
    }

    private IEnumerator WaitForSceneToLoadAndRespawn()
    {
        Debug.Log("Waiting for scene to load");
        Debug.Log(uiManager.fadeTime);
        yield return new WaitForSeconds(uiManager.fadeTime);
        Debug.Log("Loading Scene Starting");
        // Rest of the setup
        UpdateObjects();
        musicChanger.PlaySceneTrack(SceneManager.GetActiveScene().name);
    }   
    void SpawnObjects()
    {
        if(levelName == "GameTestScene")
        {
            if(checkpointManager.FirstCheckpoint == true && !collectorManager.hasSpawnedDoubloons && !obstacleManager.hasSpawnedRocks)
            {
                Debug.Log("Spawning Objects");
                //Spawn Doubloons
                collectorManager.SpawnDoubloons(player.transform, safeDistance);
                //Spawn Rocks
                obstacleManager.SpawnObstaclesInZone(obstacleManager.zone1,obstacleManager.Zone1Obstacles,player.transform,safeDistance);
            }
            else if(checkpointManager.SecondCheckpoint == true && !obstacleManager.hasSpawnedRocks && !obstacleManager.hasSpawnedIcebergs && !hasSpawnedZone2Obstacles)
            {
                //Spawn Icebergs
                obstacleManager.SpawnObstaclesInZone(obstacleManager.zone2,obstacleManager.Zone2Obstacles,player.transform,safeDistance);
            }
            else if(checkpointManager.ThirdCheckpoint == true && !obstacleManager.hasSpawnedRocks && !obstacleManager.hasSpawnedIcebergs && !obstacleManager.hasSpawnedDebris && !hasSpawnedZone3Obstacles)
            {
                //Spawn Debris
                obstacleManager.SpawnObstaclesInZone(obstacleManager.zone3,obstacleManager.Zone3Obstacles,player.transform,safeDistance);
                hasSpawnedZone3Obstacles = true;
            }
        }
    }
    public void UpdateObjects()
    {
        SetFalse();
        if(checkpointManager.FirstCheckpoint == true)
        {
            SpawnObjects();
            hasSpawnedZone1Obstacles = true;
        }
        else if (checkpointManager.SecondCheckpoint == true)
        {
            SpawnObjects();
            hasSpawnedZone2Obstacles = true;
        }
        else if (checkpointManager.ThirdCheckpoint == true)
        {
            SpawnObjects();
            hasSpawnedZone3Obstacles = true;
        }
    }
    public void SetFalse()
    {
        collectorManager.hasSpawnedDoubloons = false;
        obstacleManager.hasSpawnedRocks = false;
        obstacleManager.hasSpawnedIcebergs = false;
        obstacleManager.hasSpawnedDebris = false;
    }
    public void ClearScene()
    {
        obstacleManager.Zone1Obstacles.Clear();
        obstacleManager.Zone2Obstacles.Clear();
        obstacleManager.Zone3Obstacles.Clear();
    }
    public void PlacePlayer()
    {
        Vector3 playerPosition = playerSpawnPoint.position;
        playerPosition.z = -2;
        player.transform.position = playerPosition;

        Vector3 cameraPosition = playerSpawnPoint.position;
        cameraPosition.z = -30;
        Camera.transform.position = cameraPosition;
    }

    private IEnumerator WaitForScreenLoad(string sceneName)
    {
        yield return new WaitForSeconds(uiManager.fadeTime);
        Debug.Log("Loading Scene Starting");

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.completed += OperationCompleted;
        scenesToLoad.Add(operation);
    }

    public float GetLoadingProgress()
    {
        float totalprogress = 0;

        foreach (AsyncOperation operation in scenesToLoad)
        {
            totalprogress += operation.progress;
        }

        return totalprogress / scenesToLoad.Count;
    }

    private void OperationCompleted(AsyncOperation operation)
    {
        Debug.Log("Scene Loaded");
        scenesToLoad.Remove(operation);
    }
    public void SetLocationFalse()
    {
        hasSpawnedZone1Obstacles = false;
        hasSpawnedZone2Obstacles = false;
        hasSpawnedZone3Obstacles = false;
    }
}
