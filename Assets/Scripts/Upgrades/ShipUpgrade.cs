using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShipUpgrade : Upgrade
{
    [Header("Upgrade Values")]
    public TextMeshProUGUI ShipText;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI ButtonText;
    [Header("UI Elements")]
    public List<GameObject> UpgradeDisplay = new List<GameObject>();
    private int currentUpgradeIndex = 0;
    [Header("Upgrade Values")]
    public int MaxLevel = 3;
    private const int BaseCost = 55;
    private const int CostIncrement = 30;
    private const int DamageIncrement = 10;
    private const int HealthIncrement = 10;
    private const int SpeedIncrement = 10;
    private const int MagnetIncrement = 3;


    void Start()
    {
        MaxLevel = 3;
        cost = BaseCost;
        // Initialize the damageUpgrade images to white
        foreach (var upgrade in UpgradeDisplay)
        {
            var image = upgrade.GetComponent<UnityEngine.UI.Image>();
            if (image != null)
            {
                image.color = Color.red;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
       ShipText.text = "Ship Level " + playerStats.Level;
       ButtonText.text = "$" + cost; 
    }

    public override void CostCheck()
    {
        if (inventory.coinCount >= cost)
        {
            inventory.coinCount -= cost;
            UpgradePlayer();
            UpdateUpgradeDisplay();
        }
        else
        {
            costText.text = "Not enough coins";
        }
    }

    public override void UpgradePlayer()
    {
        if (playerStats.Level < MaxLevel)
        {
            UpgradeBonus();
            playerStats.healthManager.HandlePlayerHealthBar(playerStats.playerHealth, playerStats.startHealth);
        }
        else
        {
            ButtonText.text = "Max Ship Reached";
            inventory.coinCount += cost;
            ShipText.text = "Max Ship Reached";
        }
    }
    public override void Reset()
    {
        
    }
    void UpgradeBonus()
    {
        playerStats.LevelUp();
        playerStats.damage += DamageIncrement;
        playerStats.playerHealth += HealthIncrement;
        playerStats.startHealth += HealthIncrement;
        playerStats.speed += SpeedIncrement;
        playerStats.magnet += MagnetIncrement;
        cost += CostIncrement;
    }

    void UpdateUpgradeDisplay()
    {
        if (currentUpgradeIndex < UpgradeDisplay.Count)
        {
            var image = UpgradeDisplay[currentUpgradeIndex].GetComponent<UnityEngine.UI.Image>();
            if (image != null)
            {
                image.color = Color.green;
            }
            currentUpgradeIndex++;
        }
    }
}
