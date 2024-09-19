using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public UIManager uiManager;

    public LevelManager levelManager;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        uiManager.currentGameState = UIManager.GameState.MainMenu;
    }

    // Update is called once per frame
    void Update()
    {
        uiManager.UpdateUI();
        player.SetActive(uiManager.currentGameState == UIManager.GameState.GamePlay);
    }
}
