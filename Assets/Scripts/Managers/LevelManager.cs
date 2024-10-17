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
    public MusicChanger musicChanger;
    public WaveManger waveManager;
    public UIManager uiManager;
    public ObstacleManager obstacleManager;
    [Header("Loading Screen")]
    public List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();
    [Header("Level Variables")]
    public string levelName;
    public Transform playerSpawnPoint; 

    void Start()
    {
        
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

        if (name == "GameTestScene")
        {
            // Respawn the objects when the scene is reloaded
            StartCoroutine(WaitForSceneToLoadAndRespawn());
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

        Debug.Log("Scene Loaded. Respawning Objects...");

        // Rest of the setup
        collectorManager.hasSpawnedDoubloons = false;   
        obstacleManager.hasSpawnedRocks = false;
        musicChanger.PlayNextTrack();
        waveManager.SetAll();
    }   

    private void SetPlayerSpawnPoint()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null && playerSpawnPoint != null)
        {
            player.transform.position = playerSpawnPoint.position;
        }
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