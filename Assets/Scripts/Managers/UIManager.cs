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
        Instructions,
        Pause,
        GameOver,
        Upgrades,
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
    public GameObject upgradesMenu;
    public GameObject InstructionsScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateUI();
    }
    public void UpdateUI()
    {
        switch(currentGameState)
        {
            case GameState.MainMenu:
                DeactivateAllUI();
                mainMenu.SetActive(true);
                break;
            case GameState.Options:
                DeactivateAllUI();
                optionsMenu.SetActive(true);
                break;
            case GameState.GamePlay:
                DeactivateAllUI();
                gamePlay.SetActive(true);
                break;
            case GameState.Pause:
                DeactivateAllUI();
                pauseMenu.SetActive(true);
                break;
            case GameState.GameOver:
                DeactivateAllUI();
                gameOver.SetActive(true);
                break;
            case GameState.Win:
                DeactivateAllUI();
                winMenu.SetActive(true);
                break;
            case GameState.Upgrades:
                DeactivateAllUI();
                upgradesMenu.SetActive(true);
                break;
            case GameState.Instructions:
                DeactivateAllUI();
                InstructionsScreen.SetActive(true);
                break;
            default:
                break;
            
        }
    }
    void ActivateAllUI()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(true);
        gamePlay.SetActive(true);
        pauseMenu.SetActive(true);
        gameOver.SetActive(true);
        winMenu.SetActive(true);
        upgradesMenu.SetActive(true);
        InstructionsScreen.SetActive(true);
    }
    void DeactivateAllUI()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        gamePlay.SetActive(false);
        pauseMenu.SetActive(false);
        gameOver.SetActive(false);
        winMenu.SetActive(false);
        upgradesMenu.SetActive(false);
        InstructionsScreen.SetActive(false);
    }
    public void SetGameState(string state)
    { 
        Debug.Log("Changing game state to: " + state);
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
            case "Upgrades":
                currentGameState = GameState.Upgrades;
                break;
            case "Instructions":
                currentGameState = GameState.Instructions;
                break;
            default:
                currentGameState = GameState.MainMenu;
                break;
        }
    }
}
