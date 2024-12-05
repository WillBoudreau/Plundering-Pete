using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EndGameText : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [Header("End Game text Fields")]
    [SerializeField] private TextMeshProUGUI GameOverText;
    [SerializeField] private TextMeshProUGUI GameOverDoubloons;
    public TextMeshProUGUI text;
    public float speed = 0.1f;
    bool hasShown = false;
    void Update()
    {
        if (uiManager.currentGameState == UIManager.GameState.GameOver && !hasShown)
        {
            ShowText();
        }
        else if(uiManager.currentGameState != UIManager.GameState.GameOver && hasShown)
        {
            hasShown = false;
        }
    }
    public void ShowText()
    {
        hasShown = true;
        GameOverText.text = "";
        GameOverDoubloons.text = "";
        StartCoroutine(ShowGameOverText(GameOverText, "Game Over"));
        StartCoroutine(ShowGameOverText(GameOverDoubloons, $"You collected {uiManager.inventoryManager.coinCount} Doubloons"));
    }

    public IEnumerator ShowGameOverText(TextMeshProUGUI text,string content)
    {
        foreach (char letter in content.ToCharArray())
        {
            text.text += letter;
            yield return new WaitForSecondsRealtime(speed);
        }
    }
}
