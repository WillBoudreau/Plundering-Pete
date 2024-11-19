using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamageUpgrade : Upgrade
{
    [Header("Upgrade Values")]
    //Upgrade Values
    private const int DamageIncrement = 3;
    private const int BaseCost = 10;
    public float MaxDamage;

    void Start()
    {
        MaxDamage = playerStats.damage + DamageIncrement;
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
        costText.text = "Damage: " + playerStats.damage;
        ButtonText.text = "$" + cost;
    }

    //Check the cost of the upgrade
    public override void CostCheck()
    {
        Debug.Log("Checking Cost for Damage Upgrade");
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

    //Upgrade the player damage
    public override void UpgradePlayer()
    {
        upgradeManager.OnUpgradePurchased();
        if (playerStats.damage < MaxDamage)
        {
            playerStats.damage ++;
            UpdateUpgradeDisplay(Color.green);
        }
        else
        {
            inventory.coinCount += cost;
            messageText.text = "Max Damage Reached";
        }
    }

    //Reset the upgrade
    public override void ResetUpgrade()
    {
        ResetIndicator(Color.red);
        currentUpgradeIndex = 0;
        cost += BaseCost;
        MaxDamage = playerStats.damage + DamageIncrement;
    }
}
