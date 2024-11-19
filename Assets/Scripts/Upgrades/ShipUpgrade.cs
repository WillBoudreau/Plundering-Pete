using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShipUpgrade : Upgrade
{
    [Header("Upgrade Values")]
    public GameObject UpgradeSlot;
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
        ResetIndicator(Color.red);
        UpgradeSlot.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
       SetText();
    }
    public override void SetText()
    {
        costText.text = "Ship Level " + playerStats.Level;
        ButtonText.text = "$" + cost;
    }

    public override void CostCheck()
    {
        if(upgradeManager.CanUpgradeShip == true)
        {
            Debug.Log("Checking Cost for Ship Upgrade");
            if (inventory.coinCount >= cost)
            {
                inventory.coinCount -= cost;
                UpgradePlayer();
                upgradeManager.CanUpgradeShip = false;
            }
            else
            {
                messageText.text = "Not enough coins";
            }
        }
        else
        {
            messageText.text = "Max Ship Reached";
        }
    }

    public override void UpgradePlayer()
    {
        if (playerStats.Level < MaxLevel)
        {
            UpgradeBonus();
            playerStats.healthManager.HandlePlayerHealthBar(playerStats.playerHealth, playerStats.startHealth);
            UpdateUpgradeDisplay(Color.green);
        }
        else
        {
            ButtonText.text = "Max Ship Reached";
            inventory.coinCount += cost;
            messageText.text = "Max Ship Reached";
        }
    }
    public override void ResetUpgrade()
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
    public void EnableUpgradeSlot()
    {
        UpgradeSlot.SetActive(true);
    }
}
