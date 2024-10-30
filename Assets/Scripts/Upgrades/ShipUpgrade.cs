using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShipUpgrade : Upgrade
{
    [Header("Upgrade Values")]
    public TextMeshProUGUI ShipText;
    public TextMeshProUGUI costText;
    public int MaxLevel;
    public List<GameObject> UpgradeDisplay = new List<GameObject>();
    private int currentUpgradeIndex = 0;

    void Start()
    {
        MaxLevel = 3;
        cost = 100;
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
       ShipText.text = "Ship Level " + player.Level + " Cost: " + cost;
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
        if (player.Level < MaxLevel)
        {
            Debug.Log("Upgrading Player Ship");
            player.Level ++;
            player.damage += 10;
            player.playerHealth += 10;
            player.speed += 10;
            Debug.Log("Player Ship: " + player.Level);
        }
        else
        {
            inventory.coinCount += cost;
            Debug.Log("Max Ship Reached");
            ShipText.text = "Max Ship Reached";
        }
    }
    public override void Reset()
    {
        // foreach (var upgrade in UpgradeDisplay)
        // {
        //     var image = upgrade.GetComponent<UnityEngine.UI.Image>();
        //     if (image != null)
        //     {
        //         image.color = Color.green;
        //     }
        // }
        // currentUpgradeIndex = 0;
        // MaxLevel += 3;
        // cost += 10;
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
