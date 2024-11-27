using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FireRateUpgrade : Upgrade
{
    [Header("Upgrade Values")]
    public float MaxFireRate;
    private const int BaseCost = 10;
    private const float FireRateIncrement = 0.1f;

    void Start()
    {
        // Set the max fire rate to the player's fire rate minus the increment times the number of upgrades
        MaxFireRate = (playerStats.startFireRate - (FireRateIncrement * upgradeIndicators.Count));
        cost = BaseCost;
        // Initialize the fire rate upgrade images to red
        ResetIndicator(Color.red);
    }

    // Update is called once per frame
    void Update()
    {
        SetText();
    }
    public override void SetText()
    {
        costText.text = "Fire Rate: " + playerStats.startFireRate.ToString("F2");
        ButtonText.text = "$" + cost;
    }

    // Check the cost of the upgrade
    public override void CostCheck()
    {
        Debug.Log("Checking Cost for Fire Rate Upgrade");
        if (inventory.coinCount >= cost)
        {
            inventory.coinCount -= cost;
            UpgradePlayer();
        }
        else
        {

            messageText.text = "Not enough coins";
        }
    }

    // Upgrade the player's fire rate
    public override void UpgradePlayer()
    {
        if (playerStats.startFireRate > MaxFireRate)
        {
            playerStats.fireRate -= FireRateIncrement;
            playerStats.startFireRate -= FireRateIncrement;
            UpdateUpgradeDisplay(Color.green);
            upgradeManager.OnUpgradePurchased();
        }
        else
        {
            inventory.coinCount += cost;
            messageText.text = "Max Fire Rate Reached";
        }
    }
    
    // Reset the upgrade
    public override void ResetUpgrade()
    {
        Debug.Log("Max Fire Rate: " + MaxFireRate);
        cost += BaseCost;
        ResetIndicator(Color.red);
        currentUpgradeIndex = 0;
        MaxFireRate = (playerStats.startFireRate - (FireRateIncrement * upgradeIndicators.Count));
        Debug.Log("Fire Rate: " + (FireRateIncrement * upgradeIndicators.Count));
        Debug.Log("Max Fire Rate: " + MaxFireRate);
    }
}
