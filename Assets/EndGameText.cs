using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EndGameText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public string GameOverText;
    public float speed = 0.1f;
    public void ShowText()
    {
        StartCoroutine(ShowGameOverText());
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
