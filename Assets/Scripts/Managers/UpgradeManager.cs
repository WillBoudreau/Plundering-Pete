using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    [Header("Class calls")]
    //Player reference  
    [SerializeField] private PlayerStats player;
    [SerializeField] private ShipUpgrade shipUpgrade;
    [SerializeField] private InventoryManager inventory;
    [SerializeField] private List<Upgrade> upgrades = new List<Upgrade>();
    [Header("Text Slots")]
    //Text for the slots
    [SerializeField] private TextMeshProUGUI NumberCoinsText;
    [Header("Upgrade Limit")]
    [SerializeField] private int totalUpgradesRequired = 9;
    public int totalUpgrades = 0;
    public bool CanUpgradeShip = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerStats>();
        inventory = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
    }
    void Update()
    {
        SetText();
    }
    //Set the text for the inventory
    void SetText()
    {
        NumberCoinsText.text = $"Doubloons {inventory.coinCount}/{inventory.maxCoins}";
    }
    public void OnUpgradePurchased()
    {
        totalUpgrades++;
        if (totalUpgrades >= totalUpgradesRequired)
        {
            CanUpgradeShip = true;
            shipUpgrade.EnableUpgradeSlot();
        }
    }

    //Upgrade the player ship
    public void UpgradeShip()
    {
        inventory.coinCount -= shipUpgrade.cost;
        shipUpgrade.UpgradePlayer();
        ResetUpgrades();
        Debug.Log("Ship Upgraded");
        // if(CanUpgradeShip)
        // {
        //     if(inventory.coinCount >= shipUpgrade.cost)
        //     {
                
        //     }
        //     else
        //     {
        //         shipUpgrade.messageText.text = "Not enough coins";
        //     }
        // }
    }
    //Reset the upgrades
    public void ResetUpgrades()
    {
        Debug.Log("Resetting Upgrades");
        totalUpgrades = 0;
        foreach (var upgrade in upgrades)
        {
            upgrade.ResetUpgrade();
        }
    }
}