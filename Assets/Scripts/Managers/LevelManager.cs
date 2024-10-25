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
    }
    public void LoadLevel(string name)
    {
        Debug.Log("Level load requested for: " + name);
        SceneManager.LoadScene(name);

        if(name == "GameTestScene")
        {
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
        yield return new WaitForSeconds(uiManager.fadeTime); // Adjust timing if needed

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
                obstacleManager.spawnZone1Obstacles(obstacleManager.zone1Xnegative,obstacleManager.zone1Xpositive,obstacleManager.zone1Ynegative,obstacleManager.zone1Ypositive,player.transform, safeDistance);
            }
            else if(waveManager.SecondCheckpoint == true && !obstacleManager.hasSpawnedRocks && !obstacleManager.hasSpawnedIcebergs && !hasSpawnedZone2Obstacles)
            {
                //Spawn Icebergs
                obstacleManager.spawnZone2Obstacles(obstacleManager.zone2Xnegative,obstacleManager.zone2Xpositive,obstacleManager.zone2Ynegative,obstacleManager.zone2Ypositive,player.transform, safeDistance);
            }
            else if(waveManager.ThirdCheckpoint == true && !collectorManager.hasSpawnedDoubloons && !obstacleManager.hasSpawnedRocks && !obstacleManager.hasSpawnedIcebergs && !obstacleManager.hasSpawnedDebris && !hasSpawnedZone3Obstacles)
            {
                //Spawn Debris
                obstacleManager.spawnZone3Obstacles(obstacleManager.zone3Xnegative,obstacleManager.zone3Xpositive,obstacleManager.zone3Ynegative,obstacleManager.zone3Ypositive,player.transform, safeDistance);
            }
    }
    public void UpdateObjects()
    {
        SetFalse();
        Debug.Log("Updating Objects");
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
        Debug.Log("Setting False");
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
