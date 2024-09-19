using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public UIManager uiManager;

    public LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        uiManager.currentGameState = UIManager.GameState.MainMenu;
    }

    // Update is called once per frame
    void Update()
    {
        if(levelManager.levelName == "MainMenuScene")
        {
            uiManager.currentGameState = UIManager.GameState.MainMenu;
        }
        else if(levelManager.levelName == "GameTestScene")
        {
            uiManager.currentGameState = UIManager.GameState.GamePlay;
        }
        else if(levelManager.levelName == "GameOver")
        {
            uiManager.currentGameState = UIManager.GameState.GameOver;
        }
        else if(levelManager.levelName == "Win")
        {
            uiManager.currentGameState = UIManager.GameState.Win;
        }
        uiManager.UpdateUI();
    }
}
