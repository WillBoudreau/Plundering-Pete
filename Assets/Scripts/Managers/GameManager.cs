using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public UIManager uiManager;
    public HealthManager healthManager;
    public LevelManager levelManager;
    public GameObject player;
    public PlayerBehaviour playerBehaviour; 


    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        playerBehaviour = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
        uiManager.currentGameState = UIManager.GameState.MainMenu;
        DisablePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        uiManager.UpdateUI();
        if (levelManager.levelName == "GameTestScene" && uiManager.currentGameState == UIManager.GameState.GamePlay)
        {
            EnablePlayer();
            Time.timeScale = 1;
        }
        else if(uiManager.currentGameState == UIManager.GameState.Pause)
        {
            Time.timeScale = 0;
            DisablePlayer();
        }
        else
        {
            DisablePlayer();
        }
    }
    void EnablePlayer()
    {
        player.GetComponent<PlayerBehaviour>().enabled = true;
        player.GetComponent<SpriteRenderer>().enabled = true;
    }
    void DisablePlayer()
    {
        player.GetComponent<PlayerBehaviour>().enabled = false;
        player.GetComponent<SpriteRenderer>().enabled = false;
    }
}
