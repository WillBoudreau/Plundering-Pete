using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("Class Calls")]
    public UIManager uiManager;
    public HealthManager healthManager;
    public LevelManager levelManager;
    public GameObject player;
    public GameObject Camera;
    public PlayerBehaviour playerBehaviour;
    public CollectorManager collectorManager;
    public ObstacleManager obstacleManager;
    public InventoryManager inventoryManager;
    public CameraManager cameraManager;
    [Header("Variables")]
    public bool PlayerEnabled;
    public Transform playerSpawnPoint;
    public Button loadButton;
    

    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        playerBehaviour = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
        collectorManager = GameObject.Find("CollectorManager").GetComponent<CollectorManager>();
        obstacleManager = GameObject.Find("ObstacleManager").GetComponent<ObstacleManager>();
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        cameraManager = GameObject.Find("Camera").GetComponent<CameraManager>();
        uiManager.currentGameState = UIManager.GameState.MainMenu;
        DisablePlayer();
        DisableCamera();
        DisableLoadButton();
    }

    // Update is called once per frame
    void Update()
    {
        Pause();
        uiManager.UpdateUI();
        if (levelManager.levelName == "GameTestScene" && uiManager.currentGameState == UIManager.GameState.GamePlay)
        {
            playerSpawnPoint = GameObject.Find("PlayerSpawnPoint").transform;
            if (playerBehaviour.PlayerPlaced == false)
            {
                PlacePlayer();
                playerBehaviour.PlayerPlaced = true;
            }
            EnablePlayer();
            EnableCamera();
            EnableLoadButton();
            Time.timeScale = 1;
        }
        else if(uiManager.currentGameState == UIManager.GameState.Pause | uiManager.currentGameState == UIManager.GameState.GameOver)
        {
            Time.timeScale = 0;
            DisablePlayer();
            DisableCamera();
        }
        else if(uiManager.currentGameState == UIManager.GameState.GameOver)
        {
            DisablePlayer();
            DisableCamera();
        }
        else
        {
            DisablePlayer();
            DisableCamera();
        }
    }
void PlacePlayer()
{
    Vector3 playerPosition = playerSpawnPoint.position;
    playerPosition.z = -2;
    player.transform.position = playerPosition;

    Vector3 cameraPosition = playerSpawnPoint.position;
    cameraPosition.z = -30;
    Camera.transform.position = cameraPosition;
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
    void EnableCamera()
    {
        Camera.GetComponent<CameraManager>().enabled = true;
    }
    void DisableCamera()
    {
        Camera.GetComponent<CameraManager>().enabled = false;
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
    void EnableLoadButton()
    {
        loadButton.interactable = true;
    }
    void DisableLoadButton()
    {
        loadButton.interactable = false;
    }
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Create);

        PlayerData data = new PlayerData();
        data.damage = playerBehaviour.damage;
        data.health = playerBehaviour.playerHealth;
        data.speed = playerBehaviour.speed;
        data.Gold = inventoryManager.coinCount;

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
            inventoryManager.coinCount = data.Gold;
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
