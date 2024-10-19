using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldStorageUpgrade : Upgrade
{
    // Start is called before the first frame update
    [Header("Upgrade Values")]
    public TextMeshProUGUI CargoText;
    public TextMeshProUGUI costText;
    public int MaxCargo;
    public List<GameObject> CargoUpgrade= new List<GameObject>();
    private int currentUpgradeIndex = 0;

    void Start()
    {
        MaxCargo = 3;
        cost = 10;
        // Initialize the damageUpgrade images to white
        foreach (var upgrade in CargoUpgrade)
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
      CargoText.text = "Cargo: " + inventory.maxCoins + "Cost: " + cost;
    }

    public override void CostCheck()
    {
        Debug.Log("Checking Cost for Magnet Upgrade");
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
        if (inventory.maxCoins < MaxCargo)
        {
            Debug.Log("Upgrading Player Cargo");
            inventory.maxCoins += 10;
            Debug.Log("Player Cargo: " + inventory.maxCoins);
        }
        else
        {
            Debug.Log("Max Cargo Reached");
            CargoText.text = "Max Cargo Reached";
        }
    }

    void UpdateUpgradeDisplay()
    {
        if (currentUpgradeIndex < CargoUpgrade.Count)
        {
            var image = CargoUpgrade[currentUpgradeIndex].GetComponent<UnityEngine.UI.Image>();
            if (image != null)
            {
                image.color = Color.red;
            }
            currentUpgradeIndex++;
        }
    }
}