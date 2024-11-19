using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldStorageUpgrade : Upgrade
{
    [Header("Upgrade Values")]
    public int MaxCargo;
    public int MaxNumberOfCoins = 100;
    private const int BaseCost = 10;

    void Start()
    {
        MaxCargo = inventory.maxCoins + MaxNumberOfCoins;
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
        costText.text = "Cargo Hold: " + inventory.maxCoins;
        ButtonText.text = "$" + cost;
    }

    public override void CostCheck()
    {
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
        if (inventory.maxCoins <= MaxCargo && currentUpgradeIndex != 3)
        {
            upgradeManager.OnUpgradePurchased();
            inventory.maxCoins += 10;
            MaxCargo = inventory.maxCoins + 10;
            UpdateUpgradeDisplay(Color.green);
        }
        else
        {
            inventory.coinCount += cost;
            messageText.text = "Max Cargo Reached";
        }
    }
    public override void ResetUpgrade()
    {
        ResetIndicator(Color.red);
        MaxCargo = inventory.maxCoins + MaxNumberOfCoins;
        currentUpgradeIndex = 0;
        cost += BaseCost;
    }
}