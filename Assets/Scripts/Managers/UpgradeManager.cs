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
    [SerializeField] private TextMeshProUGUI NumberUpgradesText;
    [Header("Upgrade Limit")]
    [SerializeField] private int totalUpgradesRequired = 9;

    public int totalUpgrades = 0;
    public bool CanUpgradeShip = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerStats>();
        inventory = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        UpdateUpgradeText(totalUpgrades);
    }
    void Update()
    {
        SetText();
    }
    //Set the text for the inventory
    void SetText()
    {
        NumberCoinsText.text = $"{inventory.coinCount}/{inventory.maxCoins}";
    }
    public void OnUpgradePurchased()
    {
        shipUpgrade.UpgradeShipDisplay(1);
        totalUpgrades++;
        UpdateUpgradeText(totalUpgrades);
        if (totalUpgrades >= totalUpgradesRequired)
        {
            CanUpgradeShip = true;
            shipUpgrade.UpgradeShipDisplay(0);
        }
    }

    void UpdateUpgradeText(int totalUpgrades)
    {
        NumberUpgradesText.text = $"Upgrades {totalUpgrades}/{totalUpgradesRequired}";
    }
    //Reset the upgrades
    public void ResetUpgrades()
    {
        Debug.Log("Resetting Upgrades");
        totalUpgrades = 0;
        shipUpgrade.UpgradeShipDisplay(1);
        UpdateUpgradeText(totalUpgrades);
        foreach (var upgrade in upgrades)
        {
            upgrade.ResetUpgrade();
        }
    }
}