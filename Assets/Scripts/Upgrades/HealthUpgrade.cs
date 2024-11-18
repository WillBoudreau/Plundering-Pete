using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthUpgrade : Upgrade
{
  // Start is called before the first frame update
    [Header("Upgrade Values")]
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI ButtonText;
    [Header("UI Elements")]
    public List<GameObject> healthUpgrade = new List<GameObject>();
    private int currentUpgradeIndex = 0;
    [Header("Upgrade Values")]
    float MaxHeatlh;
    private const int BaseCost = 10;
    private const float HealthIncrement = 3;
    bool IsReset = false;

    void Start()
    {
        MaxHeatlh = playerStats.playerHealth + HealthIncrement;
        cost = BaseCost;
        // Initialize the damageUpgrade images to white
        foreach (var upgrade in healthUpgrade)
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
        healthText.text = "Health: " + playerStats.playerHealth;
        ButtonText.text = "$" + cost;
        if(IsReset == true)
        {
            MaxHeatlh = playerStats.playerHealth + HealthIncrement;
            IsReset = false;
        }
    }

    public override void CostCheck()
    {
        Debug.Log("Checking Cost for Health Upgrade");
        if (inventory.coinCount >= cost)
        {
            inventory.coinCount -= cost;
            UpgradePlayer();
            UpdateUpgradeDisplay();
        }
        else
        {
            costText.text = "Not enough coins";
        }
    }

    public override void UpgradePlayer()
    {
        if (playerStats.playerHealth < MaxHeatlh)
        {
            Debug.Log("Upgrading Player Health");
            playerStats.playerHealth ++;
            playerStats.startHealth += 1;
            playerStats.healthManager.HandlePlayerHealthBar(playerStats.playerHealth, playerStats.startHealth);
            Debug.Log("Player Health: " + playerStats.playerHealth);
        }
        else
        {
            Debug.Log("Player Health: " + playerStats.playerHealth + " Max Health: " + MaxHeatlh);
            inventory.coinCount += cost;
            Debug.Log("Max health Reached");
            healthText.text = "Max health Reached";
        }
    }
    public override void Reset()
    {
        IsReset = true;
        foreach (var upgrade in healthUpgrade)
        {
            var image = upgrade.GetComponent<UnityEngine.UI.Image>();
            if (image != null)
            {
                image.color = Color.red;
            }
        }
        currentUpgradeIndex = 0;
        MaxHeatlh = playerStats.playerHealth + HealthIncrement;
        cost += BaseCost;
    }

    void UpdateUpgradeDisplay()
    {
        if (currentUpgradeIndex < healthUpgrade.Count)
        {
            var image = healthUpgrade[currentUpgradeIndex].GetComponent<UnityEngine.UI.Image>();
            if (image != null)
            {
                image.color = Color.green;
            }
            currentUpgradeIndex++;
        }
    }
}
