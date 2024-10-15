using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string levelName;
    public CollectorManager collectorManager;
    public MusicChanger musicChanger;
    public WaveManger waveManager;
    public GameObject[] obstacles;
    public GameObject[] gold;
    public GameObject[] enemies;

    private Vector3[] obstaclePositions;
    private Vector3[] goldPositions;
    private Vector3[] enemyPositions;

    void Start()
    {
        SaveInitialPositions();
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
            collectorManager.SpawnDoubloons();
            musicChanger.PlayNextTrack();
            waveManager.Start();
            RespawnObjects();
        }
    }

    public void QuitRequest()
    {
        Debug.Log("Quit requested");
        Application.Quit();
    }

    private void SaveInitialPositions()
    {
        obstaclePositions = new Vector3[obstacles.Length];
        goldPositions = new Vector3[gold.Length];
        enemyPositions = new Vector3[enemies.Length];

        for (int i = 0; i < obstacles.Length; i++)
        {
            obstaclePositions[i] = obstacles[i].transform.position;
        }

        for (int i = 0; i < gold.Length; i++)
        {
            goldPositions[i] = gold[i].transform.position;
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            enemyPositions[i] = enemies[i].transform.position;
        }
    }

    private void RespawnObjects()
    {
        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].transform.position = obstaclePositions[i];
        }

        for (int i = 0; i < gold.Length; i++)
        {
            gold[i].transform.position = goldPositions[i];
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].transform.position = enemyPositions[i];
        }
    }
}
