using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private  PlayerStats playerStats;
    [Header("Player Movement")]
    [SerializeField] private PlayerMovementHandler playerMovementHandler;
    public float time = 5.0f;
    [Header("Class calls")]
    [SerializeField] private InventoryManager inventoryManager;

    // Start is called before the first frame update
    void Start()
    { 
        playerMovementHandler.rb = GetComponent<Rigidbody2D>();
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCounter();
        Debug.Log("Magnet: " + playerStats.magnet);
        HandlePlayer();
        Debug.Log("Player Stats: " + playerStats.magnet);
    }
    //Handle the player
    void HandlePlayer()
    {
        playerMovementHandler.HandlePlayerMovement();
        Debug.Log("Handling Player");
        Debug.Log("Magnet: " + playerStats.magnet);
        playerStats.HandlePlayer();
        Debug.Log("Player Stats: " + playerStats.magnet);
    }
    //Update the Doubloon counter
    void UpdateCounter()
    {
        inventoryManager.coinText.text = $"x{inventoryManager.coinCount}/{inventoryManager.maxCoins}";
    }
}
