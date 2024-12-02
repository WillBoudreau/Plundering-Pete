using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MFGUpgrade : Upgrade
{
    [Header("Upgrade Values")]
    public float MaxMagnet;
    private const int BaseCost = 10;
    private const float MagnetIncrement = 3;
    void Awake()
    {
        cost = BaseCost;
        ResetIndicator(Color.red);
        MaxMagnet = (playerStats.magnet + MagnetIncrement);
    }

    void Start()
    {
        MaxMagnet = (playerStats.magnet + MagnetIncrement);
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
        costText.text = "Magnet: " + playerStats.magnet;
        ButtonText.text = "$" + cost;
    }

    // Check the cost of the upgrade
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

    // Upgrade the player
    public override void UpgradePlayer()
    {
        upgradeManager.OnUpgradePurchased();
        if (playerStats.magnet < MaxMagnet)
        {
            playerStats.magnet ++;
            UpdateUpgradeDisplay(Color.green);
        }
        else
        {
            inventory.coinCount += cost;
            messageText.text = "Max Magnet" + MaxMagnet + " reached" + "Player Magnet: " + playerStats.magnet + "Magnet " + (playerStats.magnet + MagnetIncrement);
        }
    }

    // Reset the upgrade
    public override void ResetUpgrade()
    {
        ResetIndicator(Color.red);
        currentUpgradeIndex = 0;
        MaxMagnet = (playerStats.magnet + MagnetIncrement);
        cost += BaseCost;
    }
}
