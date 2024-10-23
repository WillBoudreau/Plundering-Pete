using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [Header("Level Manager")]
    [Header("Class Calls")]
    public CollectorManager collectorManager;
    [SerializeField] private MusicChanger musicChanger;
    public WaveManger waveManager;
    public UIManager uiManager;
    public ObstacleManager obstacleManager;
    public PlayerBehaviour player;
    [Header("Loading Screen")]
    public List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();
    [Header("Level Variables")]
    public string levelName;
    public Transform playerSpawnPoint; 
    public float safeDistance = 5f;

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
        SpawnObjects();
        musicChanger.PlaySceneTrack(SceneManager.GetActiveScene().name);
        waveManager.SetAll();
    }   
    void SpawnObjects()
    {
        //Spawn Doubloons
        collectorManager.hasSpawnedDoubloons = false;
        collectorManager.SpawnDoubloons(player.transform, safeDistance);
        //Spawn Rocks
        obstacleManager.hasSpawnedRocks = false;
        obstacleManager.spawnRocks(player.transform, safeDistance);
        //Spawn Icebergs
        obstacleManager.hasSpawnedIcebergs = false;
        obstacleManager.spawnIceBergs(player.transform, safeDistance);
        //Spawn Debris
        obstacleManager.hasSpawnedDebris = false;
        obstacleManager.SpawnDebris(player.transform, safeDistance);
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
}