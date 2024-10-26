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
    public int MaxHeatlh;
    public List<GameObject> healthUpgrade = new List<GameObject>();
    private int currentUpgradeIndex = 0;

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
                image.color = Color.green;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + player.playerHealth + " Cost: " + cost;
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
        if (player.playerHealth < MaxHeatlh)
        {
            Debug.Log("Upgrading Player Health");
            player.playerHealth ++;
            Debug.Log("Player Health: " + player.playerHealth);
        }
        else
        {
            inventory.coinCount += cost;
            Debug.Log("Max health Reached");
            healthText.text = "Max health Reached";
        }
    }
    public override void Reset()
    {
        foreach (var upgrade in healthUpgrade)
        {
            var image = upgrade.GetComponent<UnityEngine.UI.Image>();
            if (image != null)
            {
                image.color = Color.green;
            }
        }
        currentUpgradeIndex = 0;
        MaxHeatlh += 3;
        cost += 10;
    }

    void UpdateUpgradeDisplay()
    {
        if (currentUpgradeIndex < healthUpgrade.Count)
        {
            var image = healthUpgrade[currentUpgradeIndex].GetComponent<UnityEngine.UI.Image>();
            if (image != null)
            {
                image.color = Color.red;
            }
            currentUpgradeIndex++;
        }
    }
}
