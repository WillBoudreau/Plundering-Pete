using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FireRateUpgrade : Upgrade
{
    // Start is called before the first frame update
    [Header("Upgrade Values")]
    public TextMeshProUGUI FireRateText;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI ButtonText;
    [Header("UI Elements")]
    public List<GameObject> FRUpgrade = new List<GameObject>();
    private int currentUpgradeIndex = 0;
    [Header("Upgrade Values")]
    public float MaxFireRate;
    private const int BaseCost = 10;
    private const float FireRateIncrement = 0.2f;

    void Start()
    {
        // Set the max fire rate to the player's fire rate minus the increment times the number of upgrades
        MaxFireRate = playerStats.fireRate - FireRateIncrement * FRUpgrade.Count;
        cost = BaseCost;
        // Initialize the damageUpgrade images to white
        foreach (var upgrade in FRUpgrade)
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
        FireRateText.text = "Fire Rate: " + playerStats.startFireRate.ToString("F2");
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
            Debug.Log("Not enough coins");
            costText.text = "Not enough coins";
        }
    }

    // Upgrade the player's fire rate
    public override void UpgradePlayer()
    {
        if (playerStats.startFireRate > MaxFireRate)
        {
            playerStats.fireRate -= FireRateIncrement;
            playerStats.startFireRate -= FireRateIncrement;
            UpdateUpgradeDisplay();
        }
        else
        {
            inventory.coinCount += cost;
            FireRateText.text = "Max Fire Rate Reached";
        }
    }
    
    // Reset the upgrade
    public override void Reset()
    {
        foreach (var upgrade in FRUpgrade)
        {
            var image = upgrade.GetComponent<UnityEngine.UI.Image>();
            if (image != null)
            {
                image.color = Color.red;
            }
        }
        currentUpgradeIndex = 0;
        MaxFireRate = playerStats.fireRate - FireRateIncrement * FRUpgrade.Count;
        cost = BaseCost;
    }

    // Update the upgrade display
    void UpdateUpgradeDisplay()
    {
        if (currentUpgradeIndex < FRUpgrade.Count)
        {
            var image = FRUpgrade[currentUpgradeIndex].GetComponent<UnityEngine.UI.Image>();
            if (image != null)
            {
                image.color = Color.green;
            }
            currentUpgradeIndex++;
        }
    }
}
