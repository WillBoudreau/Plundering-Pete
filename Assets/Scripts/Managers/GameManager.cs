using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("Class Calls")]
    public UIManager uiManager;
    public HealthManager healthManager;
    public LevelManager levelManager;
    public GameObject player;
    public PlayerBehaviour playerBehaviour;
    public CollectorManager collectorManager;
    public ObstacleManager obstacleManager;
    [Header("Variables")]
    public bool PlayerEnabled;
    public Transform playerSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        playerBehaviour = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
        collectorManager = GameObject.Find("CollectorManager").GetComponent<CollectorManager>();
        obstacleManager = GameObject.Find("ObstacleManager").GetComponent<ObstacleManager>();
        uiManager.currentGameState = UIManager.GameState.MainMenu;
        DisablePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        Pause();
        uiManager.UpdateUI();
        if (levelManager.levelName == "GameTestScene" && uiManager.currentGameState == UIManager.GameState.GamePlay)
        {
            playerSpawnPoint = GameObject.Find("PlayerSpawnPoint").transform;

            //PlacePlayer(); 
            EnablePlayer();
            Time.timeScale = 1;
        }
        else if(uiManager.currentGameState == UIManager.GameState.Pause | uiManager.currentGameState == UIManager.GameState.GameOver)
        {
            Time.timeScale = 0;
            DisablePlayer();
        }
        else
        {
            DisablePlayer();
        }
    }
   

    void Pause()
    {
        if(PlayerEnabled == true)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                uiManager.currentGameState = UIManager.GameState.Pause;
            }
        }
        else if(PlayerEnabled == false && uiManager.currentGameState == UIManager.GameState.Pause)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                uiManager.currentGameState = UIManager.GameState.GamePlay;
            }
        }
    }
    void PlacePlayer()
    {
        if (playerSpawnPoint != null)
        {
            player.transform.position = playerSpawnPoint.position;
        }
    }
    void EnablePlayer()
    {
        PlayerEnabled = true;
        player.GetComponent<PlayerBehaviour>().enabled = true;
        player.GetComponentInChildren<SpriteRenderer>().enabled = true;
    }
    void DisablePlayer()
    {
        PlayerEnabled = false;
        player.GetComponent<PlayerBehaviour>().enabled = false;
        player.GetComponentInChildren<SpriteRenderer>().enabled = false;
    }
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Create);

        PlayerData data = new PlayerData();
        data.damage = playerBehaviour.damage;
        data.health = playerBehaviour.playerHealth;
        data.speed = playerBehaviour.speed;
        data.Gold = playerBehaviour.Doubloons;

        bf.Serialize(file, data);
        file.Close();
    }
    public void Load()
    {
         if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
         {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            playerBehaviour.playerHealth = data.health;
            playerBehaviour.damage = data.damage;
            playerBehaviour.speed = data.speed;
            playerBehaviour.Doubloons = data.Gold;
         }
    }
    [System.Serializable]
    class PlayerData
    {
        public float health;
        public float damage;
        public float speed;
        public int Gold;
    }
}
