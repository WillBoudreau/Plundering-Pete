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
    public TextMeshProUGUI DoubloonText;
    public float time = 5.0f;
    [Header("Class calls")]
    [SerializeField] private InventoryManager inventoryManager;

    // Start is called before the first frame update
    void Start()
    { 
        playerMovementHandler.rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandlePlayer();
        //GetPlayerLayerMask();
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        DoubloonText = GameObject.Find("DoubloonsText").GetComponent<TextMeshProUGUI>();
    }
    //Handle the player
    void HandlePlayer()
    {
        UpdateCounter();
        playerMovementHandler.HandlePlayerMovement();
        playerStats.HandlePlayer();
    }
    //Update the Doubloon counter
    void UpdateCounter()
    {
        DoubloonText.text = "Doubloons: " + inventoryManager.coinCount;
    }
}
