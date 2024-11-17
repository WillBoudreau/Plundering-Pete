using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    [Header("Class calls")]
    //Player reference  
    public PlayerStats player;
    public ShipUpgrade shipUpgrade;
    public List<Upgrade> upgrades = new List<Upgrade>();
    public InventoryManager inventory;
    [Header("Text Slots")]
    //Text for the slots
    public TextMeshProUGUI NumberCoinsText;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerStats>();
        inventory = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
    }
    // Update is called once per frame
    void Update()
    {
        SetText();
    }
    //Set the text for the inventory
    void SetText()
    {
        NumberCoinsText.text = $"Doubloons {inventory.coinCount}/{inventory.maxCoins}";
    }
    //Upgrade the player ship
    public void UpgradeShip()
    {
        if(inventory.coinCount >= shipUpgrade.cost && !player.IsLevel3)
        {
            ResetUpgrades();
            Debug.Log("Upgrading Player Ship");
            if(!player.IsLevel2)
            {
                player.IsLevel2 = true;
            }
            else if(!player.IsLevel3)
            {
                player.IsLevel3 = true;
                player.IsLevel2 = false;
            }
        }
        else
        {
            shipUpgrade.costText.text = "Not enough coins";
        }
    }
    //Reset the upgrades
    public void ResetUpgrades()
    {
        foreach (var upgrade in upgrades)
        {
            upgrade.Reset();
        }
    }
}