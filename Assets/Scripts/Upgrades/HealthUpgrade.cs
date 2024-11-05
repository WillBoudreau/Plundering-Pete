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
    public float MaxHeatlh;
    public List<GameObject> healthUpgrade = new List<GameObject>();
    private int currentUpgradeIndex = 0;
    public bool IsReset = false;

    void Start()
    {
        MaxHeatlh = 8;
        cost = 10;
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
        ButtonText.text = "Cost: " + cost;
        if(IsReset == true)
        {
            MaxHeatlh = playerStats.playerHealth + 3;
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
            Debug.Log("Not enough coins");
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
            playerStats.healthManager.playerhealth.maxValue += 1;
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
        MaxHeatlh = playerStats.playerHealth + 3;
        cost += 10;
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
