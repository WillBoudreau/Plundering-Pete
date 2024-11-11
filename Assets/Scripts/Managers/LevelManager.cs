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
    private bool[] hasSpawnedZone = new bool[3];
    private const float PlayerZPOS = -2f;
    private const float CameraZPOS = -30f;


    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        levelName = SceneManager.GetActiveScene().name;
        playerSpawnPoint = GameObject.Find("PlayerSpawnPoint").transform;
        UpdateZoneObstacles();
    }
    void UpdateZoneObstacles()
    {
        hasSpawnedZone[0] = checkpointManager.FirstCheckpoint;
        hasSpawnedZone[1] = checkpointManager.SecondCheckpoint;
        hasSpawnedZone[2] = checkpointManager.ThirdCheckpoint;
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
            if(checkpointManager.FirstCheckpoint == true && !hasSpawnedZone[0])
            {
                Debug.Log("Spawning Objects");
                collectorManager.hasSpawnedDoubloons = false;
                //Spawn Doubloons
                collectorManager.SpawnDoubloons(player.transform, safeDistance);
                //Spawn Rocks
                SpawnObstaclesInZone(obstacleManager.zone1,obstacleManager.Zone1Obstacles,0);
                hasSpawnedZone[0] = true;
            }
            else if(checkpointManager.SecondCheckpoint == true && !hasSpawnedZone[1])
            {
                //Spawn Icebergs
                SpawnObstaclesInZone(obstacleManager.zone2,obstacleManager.Zone2Obstacles,1);
                hasSpawnedZone[1] = true;
            }
            else if(checkpointManager.ThirdCheckpoint == true && !hasSpawnedZone[2])
            {
                //Spawn Debris
                SpawnObstaclesInZone(obstacleManager.zone3,obstacleManager.Zone3Obstacles,2);
                hasSpawnedZone[2] = true;
            }
        }
    }
    void SpawnObstaclesInZone(ObstacleManager.ObstacleZone zone, List<GameObject> obstacles,int zoneIndex)
    {
        obstacleManager.SpawnObstaclesInZone(zone,obstacles,player.transform,safeDistance);
    }
    public void UpdateObjects()
    {
        if(checkpointManager.FirstCheckpoint == true)
        {
            SpawnObjects();
        }
        else if (checkpointManager.SecondCheckpoint == true)
        {
            SpawnObjects();
        }
        else if (checkpointManager.ThirdCheckpoint == true)
        {
            SpawnObjects();
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
    void ClearObstacles()
    {
        obstacleManager.Zone1Obstacles.Clear();
        obstacleManager.Zone2Obstacles.Clear();
        obstacleManager.Zone3Obstacles.Clear();
    }
}