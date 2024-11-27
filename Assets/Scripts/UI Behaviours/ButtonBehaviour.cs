using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ButtonBehaviour : MonoBehaviour
{ 
    public GameObject text;
    //public TextMeshProUGUI buttonText;
    public UIManager uIManger;
    public float Timer = 1f;
    public void OnButtonPress(string GameState)
    {
        DisableText();
        StartCoroutine(ChangeText(GameState));
        uIManger.currentGameState = UIManager.GameState.Instructions;
        EnableText();
    }
    void DisableText()
    {
        Debug.Log("Disabling Text");
        text.SetActive(false);
    }
    void EnableText()
    {
        text.SetActive(true);
    }
    IEnumerator ChangeText(string GameState)
    {
        Debug.Log("Changing Text");
        Debug.Log(Timer);
        yield return new WaitForSeconds(Timer);
    }
}
