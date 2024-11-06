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
    public float MaxFireRate;
    public List<GameObject> FRUpgrade = new List<GameObject>();
    private int currentUpgradeIndex = 0;

    void Start()
    {
        MaxFireRate = playerStats.fireRate - 0.2f * FRUpgrade.Count;
        cost = 10;
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
        ButtonText.text = "Cost: " + cost;
    }

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

    public override void UpgradePlayer()
    {
        if (playerStats.startFireRate > MaxFireRate)
        {
            Debug.Log("Upgrading Player Fire Rate");
            playerStats.fireRate -= 0.2f;
            playerStats.startFireRate -= 0.2f;
            Debug.Log("Player FireRate: " + playerStats.fireRate.ToString("F2"));
            UpdateUpgradeDisplay();
        }
        else
        {
            inventory.coinCount += cost;
            Debug.Log("Max fire rate Reached");
            FireRateText.text = "Max Fire Rate Reached";
        }
    }

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
        MaxFireRate = playerStats.fireRate - 0.2f * FRUpgrade.Count;
        cost = 10;
    }

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
