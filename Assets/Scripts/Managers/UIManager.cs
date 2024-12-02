using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public enum GameState
    {
        MainMenu,
        Options,
        GamePlay,
        Instructions,
        Pause,
        Credits,
        GameOver,
        Upgrades,
        Win,
        Results
    }
    public GameState currentGameState;
    [Header("Class calls")]
    public InventoryManager inventoryManager;
    public DistanceTracker distanceTracker;
    public PlayerStats playerStats;
    [Header("UI GameObjects")]
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject gamePlay;
    public GameObject pauseMenu;
    public GameObject gameOver;
    public GameObject winMenu;
    public GameObject upgradesMenu;
    public GameObject InstructionsScreen;
    public GameObject LoadingScreen;
    public GameObject CreditsScreen;
    public GameObject results;
    [Header("Texts")]
    public TextMeshProUGUI GameOverText;
    public TextMeshProUGUI playerCoins;
    public TextMeshProUGUI playerSharkKills;
    public TextMeshProUGUI playerSerpentKills;
    public TextMeshProUGUI playerShipKills;
    public TextMeshProUGUI playerDistance;
    [Header("Loading Bar")]
    public CanvasGroup loadingScreenCanvasGroup;
    public float fadeTime = 0.5f;
    public Image loadingBar;

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
                //UpdateGameOver();
                //endGameText.ShowText();
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
            case GameState.Credits:
                DeactivateAllUI();
                CreditsScreen.SetActive(true);
                break;
            case GameState.Results:
                DeactivateAllUI();
                results.SetActive(true);
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
        CreditsScreen.SetActive(true);
        results.SetActive(true);
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
        CreditsScreen.SetActive(false);
        results.SetActive(false);
    }
    void UpdateGameOver()
    {
        //Set the texts in the Game Over screen
        //playerCoins.text = "Coins Collected: " + inventoryManager.coinCount.ToString();
        playerSharkKills.text = "Sharks Killed: " + playerStats.SharkKills.ToString();
        playerSerpentKills.text = "Serpents Killed: " + playerStats.SerpentKills.ToString();
        playerShipKills.text = "Ships Sunk: " + playerStats.ShipKills.ToString();
        playerDistance.text = "Distance Travelled: " + distanceTracker.playerDistance.ToString();
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
            case "Credits":
                currentGameState = GameState.Credits;
                break;
            case "Results":
                currentGameState = GameState.Results;
                break;
            default:
                currentGameState = GameState.MainMenu;
                break;
        }
    }
}
