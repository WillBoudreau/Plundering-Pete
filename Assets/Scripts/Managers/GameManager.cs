using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public UIManager uiManager;

    public LevelManager levelManager;
    public GameObject player;
    public GameObject Shark;
    public GameObject SharkSpawn;
    public GameObject[] enemySpawn;

    public int NumSharks;

    public List<Transform> spawnPoints = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        NumSharks = 5;
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        uiManager.currentGameState = UIManager.GameState.MainMenu;
    }

    // Update is called once per frame
    void Update()
    {
        enemySpawn = GameObject.FindGameObjectsWithTag("EnemySpawn");
        foreach (GameObject enemspawn in enemySpawn)
        {
            spawnPoints.Add(enemspawn.transform);
        }
        uiManager.UpdateUI();
        player.SetActive(uiManager.currentGameState == UIManager.GameState.GamePlay);
        if (levelManager.levelName == "GameTestScene")
        {
            spawnEnemy();
        }
    }
    void spawnEnemy()
    {
        for(int i = 0; i < NumSharks; i++)
        {
            foreach (GameObject enemspawn in enemySpawn)
            {
                Instantiate(Shark, enemspawn.transform.position, Quaternion.identity);
            }
        }
    }
}
