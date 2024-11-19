using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthUpgrade : Upgrade
{
    [Header("Upgrade Values")]
    float MaxHeatlh;
    private const int BaseCost = 10;
    private const float HealthIncrement = 3;
    void Start()
    {
        MaxHeatlh = playerStats.playerHealth + HealthIncrement;
        cost = BaseCost;
        ResetIndicator(Color.red);
    }

    // Update is called once per frame
    void Update()
    {
       SetText();
    }
    public override void SetText()
    {
        costText.text = "Health: " + playerStats.playerHealth;
        ButtonText.text = "$" + cost;
    }

    public override void CostCheck()
    {
        Debug.Log("Checking Cost for Health Upgrade");
        if (inventory.coinCount >= cost)
        {
            inventory.coinCount -= cost;
            UpgradePlayer();
        }
        else
        {
            costText.text = "Not enough coins";
        }
    }

    public override void UpgradePlayer()
    {   
        upgradeManager.OnUpgradePurchased();
        if (playerStats.playerHealth < MaxHeatlh)
        {
            playerStats.playerHealth ++;
            playerStats.startHealth += 1;
            playerStats.healthManager.HandlePlayerHealthBar(playerStats.playerHealth, playerStats.startHealth);
            UpdateUpgradeDisplay(Color.green);
        }
        else
        {
            inventory.coinCount += cost;
            messageText.text = "Max health Reached";
        }
    }
    public override void ResetUpgrade()
    {
        ResetIndicator(Color.red);
        currentUpgradeIndex = 0;
        MaxHeatlh = playerStats.playerHealth + HealthIncrement;
        cost += BaseCost;
    }
}
