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
        GameOver,
        Upgrades,
        Win
    }
    public GameState currentGameState;
    public static UIManager instance;
    [Header("Class calls")]
    public PlayerBehaviour player;
    public LevelManager levelManager;
    public InventoryManager inventoryManager;
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
    [Header("Texts")]
    public TextMeshProUGUI playerCoins;
    public TextMeshProUGUI playerSharkKills;
    public TextMeshProUGUI playerSerpentKills;
    public TextMeshProUGUI playerShipKills;
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
                UpdateGameOver();
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
    void UpdateGameOver()
    {
        //Set the texts in the Game Over screen
        playerCoins.text = "Coins Collected: " + inventoryManager.coinCount.ToString();
        Debug.Log(inventoryManager.coinCount);
        playerSharkKills.text = "Sharks Killed: " + player.SharkKills.ToString();
        playerSerpentKills.text = "Serpents Killed: " + player.SerpentKills.ToString();
        playerShipKills.text = "Ships Sunk: " + player.ShipKills.ToString();
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
    public void UILoadingScreen(GameObject uiPanel)
    {
        StartCoroutine(LoadingUIFadeIN());
        StartCoroutine(DelayedSwitchUIPanel(fadeTime, uiPanel));
    }
    private IEnumerator LoadingUIFadeOut()
    {
        Debug.Log("Starting Fadeout");

        float timer = 0;

        while (timer < fadeTime)
        {
            loadingScreenCanvasGroup.alpha = Mathf.Lerp(1, 0, timer/fadeTime);
            timer += Time.deltaTime;
            yield return null;
        }

        loadingScreenCanvasGroup.alpha = 0;
        LoadingScreen.SetActive(false);

        Debug.Log("Ending Fadeout");
    }
    private IEnumerator LoadingUIFadeIN()
    {
        Debug.Log("Starting Fadein");
        float timer = 0;
        LoadingScreen.SetActive(true);

        while (timer < fadeTime)
        {
            loadingScreenCanvasGroup.alpha = Mathf.Lerp(0, 1, timer / fadeTime);
            timer += Time.deltaTime;
            yield return null;
        }

        loadingScreenCanvasGroup.alpha = 1;

        Debug.Log("Ending Fadein");
        StartCoroutine(LoadingBarProgress());
    }
   private IEnumerator LoadingBarProgress()
    {
        Debug.Log("Starting Progress Bar");
        while (levelManager.scenesToLoad.Count <= 0)
        {
            //waiting for loading to begin
            yield return null;
        }
        while (levelManager.scenesToLoad.Count > 0)
        {
            loadingBar.fillAmount = levelManager.GetLoadingProgress();
            yield return null;
        }
        yield return new WaitForEndOfFrame();
        Debug.Log("Ending Progress Bar");
        StartCoroutine(LoadingUIFadeOut());
        loadingBar.fillAmount = 0;
    }
    private IEnumerator DelayedSwitchUIPanel(float time, GameObject uiPanel)
    {
        yield return new WaitForSeconds(time);
        DeactivateAllUI();
        LoadingScreen.SetActive(true);
        uiPanel.SetActive(true);
    }
}
