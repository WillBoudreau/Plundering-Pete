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
    [SerializeField] private WaveManger waveManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private ObstacleManager obstacleManager;
    [SerializeField] private PlayerBehaviour player;
    [SerializeField] private CameraManager cameraManager;
    [Header("Loading Screen")]
    public List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();
    [Header("Level Variables")]
    public string levelName;
    public Transform playerSpawnPoint; 
    public float safeDistance = 5f;
    public bool[] spawnZones = new bool[3];
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

        if(name == "GameTestScene")
        {
            SetPlayerSpawnPoint();
            waveManager.UpdateCheckpointStatus(0,true);
            StartCoroutine(WaitForSceneToLoadAndRespawn());
            musicChanger.PlaySceneTrack(name);
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
        yield return new WaitForSeconds(uiManager.fadeTime);

        // Rest of the setup
        UpdateObjects();
        musicChanger.PlaySceneTrack(SceneManager.GetActiveScene().name);
        waveManager.SetAll();
    }   
    void SpawnObjects()
    {
        Debug.Log("Spawning Objects");  
            if(waveManager.FirstCheckpoint == true && !collectorManager.hasSpawnedDoubloons && !obstacleManager.hasSpawnedRocks)
            {
                //Spawn Doubloons
                collectorManager.SpawnDoubloons(player.transform, safeDistance);
                //Spawn Rocks
                obstacleManager.SpawnObstaclesInZone(obstacleManager.zone1,obstacleManager.Zone1Obstacles,player.transform,safeDistance);
            }
            else if(waveManager.SecondCheckpoint == true && !obstacleManager.hasSpawnedRocks && !obstacleManager.hasSpawnedIcebergs && !hasSpawnedZone2Obstacles)
            {
                //Spawn Icebergs
                obstacleManager.SpawnObstaclesInZone(obstacleManager.zone2,obstacleManager.Zone2Obstacles,player.transform,safeDistance);
            }
            else if(waveManager.ThirdCheckpoint == true && !obstacleManager.hasSpawnedRocks && !obstacleManager.hasSpawnedIcebergs && !obstacleManager.hasSpawnedDebris && !hasSpawnedZone3Obstacles)
            {
                //Spawn Debris
                obstacleManager.SpawnObstaclesInZone(obstacleManager.zone3,obstacleManager.Zone3Obstacles,player.transform,safeDistance);
                hasSpawnedZone3Obstacles = true;
            }
    }
    public void UpdateObjects()
    {
        SetFalse();
        if(waveManager.FirstCheckpoint == true)
        {
            SpawnObjects();
            hasSpawnedZone1Obstacles = true;
        }
        else if (waveManager.SecondCheckpoint == true)
        {
            SpawnObjects();
            hasSpawnedZone2Obstacles = true;
        }
        else if (waveManager.ThirdCheckpoint == true)
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

    public Vector3 SetPlayerSpawnPoint()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null && playerSpawnPoint != null)
        {
            player.transform.position = playerSpawnPoint.position;
            cameraManager.transform.position = playerSpawnPoint.position;
            cameraManager.transform.position = new Vector3(cameraManager.transform.position.x, cameraManager.transform.position.y, -30);
        }
        return player.transform.position;
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
