using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EndGameText : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    public TextMeshProUGUI text;
    public string GameOverText;
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
        StartCoroutine(ShowGameOverText());
        hasShown = true;
    }

    public IEnumerator ShowGameOverText()
    {
        text.text = "";
        foreach (char letter in GameOverText.ToCharArray())
        {
            text.text += letter;
            yield return new WaitForSeconds(speed);
        }
    }
}
