using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public enum GameState
    {
        MainMenu,
        Options,
        GamePlay,
        Pause,
        GameOver,
        Win
    }
    public GameState currentGameState;
    public static UIManager instance;
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject gamePlay;
    public GameObject pauseMenu;
    public GameObject gameOver;
    public GameObject winMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }
    public void UpdateUI()
    {
        switch(currentGameState)
        {
            case GameState.MainMenu:
                mainMenu.SetActive(true);
                optionsMenu.SetActive(false);
                gamePlay.SetActive(false);
                pauseMenu.SetActive(false);
                gameOver.SetActive(false);
                winMenu.SetActive(false);
                break;
            case GameState.Options:
                mainMenu.SetActive(false);
                optionsMenu.SetActive(true);
                gamePlay.SetActive(false);
                pauseMenu.SetActive(false);
                gameOver.SetActive(false);
                winMenu.SetActive(false);
                break;
            case GameState.GamePlay:
                mainMenu.SetActive(false);
                optionsMenu.SetActive(false);
                gamePlay.SetActive(true);
                pauseMenu.SetActive(false);
                gameOver.SetActive(false);
                winMenu.SetActive(false);
                break;
        }
    }
    public void SetGameState(string state)
    {
        switch(state)
        {
            case "MainMenu":
                currentGameState = GameState.MainMenu;
                break;
            case "Options":
                currentGameState = GameState.Options;
                break;
            case "GamePlay":
                currentGameState = GameState.GamePlay;
                break;
            case "Pause":
                currentGameState = GameState.Pause;
                break;
            case "GameOver":
                currentGameState = GameState.GameOver;
                break;
            case "Win":
                currentGameState = GameState.Win;
                break;
        }
    }
}
