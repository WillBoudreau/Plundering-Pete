using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeedUpgrade : Upgrade
{
    [Header("Upgrade Values")]
    public float MaxSpeed;
    private const int BaseCost = 10;
    private const float SpeedIncrement = 3;

    void Start()
    {
        MaxSpeed = (playerStats.speed + SpeedIncrement);
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
        costText.text = "Speed: " + playerStats.speed;
        ButtonText.text = "$" + cost;
    }

    public override void CostCheck()
    {
        Debug.Log("Checking Cost for Speed Upgrade");
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
        if (playerStats.speed < MaxSpeed)
        {
            playerStats.speed ++;
            UpdateUpgradeDisplay(Color.green);
            upgradeManager.OnUpgradePurchased();
        }
        else
        {
            inventory.coinCount += cost;
            messageText.text = "Max Speed" + MaxSpeed + "Player Speed" + playerStats.speed + "Speed Increment" + SpeedIncrement + "Speed" + (playerStats.speed+SpeedIncrement);
        }
    }
    public override void ResetUpgrade()
    {
        ResetIndicator(Color.red);
        currentUpgradeIndex = 0;
        MaxSpeed = playerStats.speed + SpeedIncrement;
        cost += BaseCost;
    }
}
