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
    public int MaxLevel;
    public List<GameObject> UpgradeDisplay = new List<GameObject>();
    private int currentUpgradeIndex = 0;

    void Start()
    {
        MaxLevel = 3;
        cost = 55;
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
       ButtonText.text = "Cost: " + cost; 
    }

    public override void CostCheck()
    {
        Debug.Log("Checking Cost for Ship Upgrade");
        if (inventory.coinCount >= cost)
        {
            inventory.coinCount -= cost;
            UpgradePlayer();
            UpdateUpgradeDisplay();
        }
        else
        {
            Debug.Log("Not enough coins");
            costText.text = "Not enough coins";
        }
    }

    public override void UpgradePlayer()
    {
        if (playerStats.Level < MaxLevel)
        {
            Debug.Log("Upgrading Player Ship");
            UpgradeBonus();
            playerStats.healthManager.playerhealth.maxValue = playerStats.playerHealth;
            Debug.Log("Player Ship: " + playerStats.Level);
        }
        else
        {
            ButtonText.text = "Max Ship Reached";
            inventory.coinCount += cost;
            Debug.Log("Max Ship Reached");
            ShipText.text = "Max Ship Reached";
        }
    }
    public override void Reset()
    {
        
    }
    void UpgradeBonus()
    {
        playerStats.LevelUp();
        playerStats.damage += 10;
        playerStats.playerHealth += 10;
        playerStats.startHealth += 10;
        playerStats.speed += 10;
        playerStats.magnet += 3;
        cost += 30;
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
