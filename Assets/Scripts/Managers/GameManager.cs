using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Class Calls")]
    [SerializeField] private UIManager uiManager;
    [SerializeField] private LevelManager levelManager;

    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private PlayerBehaviour playerBehaviour;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private InventoryManager inventoryManager;
    [Header("GameObjects")]
    public GameObject player;
    public GameObject Camera;
    [Header("Variables")]
    public bool PlayerEnabled;
    public Button loadButton;
    

    // Start is called before the first frame update
    void Start()
    {
        //Get the components
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        playerBehaviour = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
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
        //If the game is in the gameplay state, enable the player and camera
        if (levelManager.levelName == "GameTestScene" && uiManager.currentGameState == UIManager.GameState.GamePlay)
        {
            if (playerStats.PlayerPlaced == false)
            {
                spawnManager.PlacePlayerAtSpawn();
                playerStats.PlayerPlaced = true;
            }
            EnableGameplay();
        }
        else if(uiManager.currentGameState == UIManager.GameState.Pause || uiManager.currentGameState == UIManager.GameState.GameOver)
        {
            DisableGameplay();
        }
    }

    //Pause the game
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
    //Enable and disable gameplay
    void EnableGameplay()
    {
        EnableCamera();
        EnablePlayer();
        EnableLoadButton();
        DisableCursor();
        Time.timeScale = 1;
    }
    void DisableGameplay()
    {
        DisablePlayer();
        DisableCamera();
        DisableLoadButton();
        EnableCursor();
        Time.timeScale = 0;
    }
    //Enable and disable camera, cursor, player, and load button
    void EnableCamera()
    {
        Camera.GetComponent<CameraManager>().enabled = true;
    }
    void DisableCamera()
    {
        Camera.GetComponent<CameraManager>().enabled = false;
    }
    void EnableCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    void DisableCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
    //Save and load game
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Create);

        PlayerData data = new PlayerData();
        data.damage = playerStats.damage;
        data.health = playerStats.playerHealth;
        data.speed = playerStats.speed;
        data.Gold = inventoryManager.coinCount;
        data.Level = playerStats.Level;
        Debug.Log("Game Saved");

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

            playerStats.playerHealth = data.health;
            playerStats.damage = data.damage;
            playerStats.speed = data.speed;
            inventoryManager.coinCount = data.Gold;
            playerStats.Level = data.Level;
            Debug.Log("Game Loaded");
         }
    }
    //Player data
    [System.Serializable]
    class PlayerData
    {
        public float health;
        public float damage;
        public float speed;
        public int Gold;
        public int Level;
    }
}
