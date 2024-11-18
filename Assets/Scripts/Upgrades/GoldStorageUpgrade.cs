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
    public TextMeshProUGUI ButtonText;
    [Header("UI Elements")]
    public List<GameObject> CargoUpgrade= new List<GameObject>();
    private int currentUpgradeIndex = 0;
    [Header("Upgrade Values")]
    public int MaxCargo;
    public int MaxNumberOfCoins = 100;
    private const int BaseCost = 10;
    public bool IsReset = false;

    void Start()
    {
        MaxCargo = inventory.maxCoins + MaxNumberOfCoins;
        cost = BaseCost;
        // Initialize the damageUpgrade images to white
        foreach (var upgrade in CargoUpgrade)
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
      CargoText.text = "Cargo Hold: " + inventory.maxCoins;
      ButtonText.text = "$" + cost;
        if(IsReset == true)
        {
            MaxCargo = inventory.maxCoins + MaxNumberOfCoins;
            IsReset = false;
        }
    }

    public override void CostCheck()
    {
        Debug.Log("Checking Cost for cargo Upgrade");
        if (inventory.coinCount >= cost)
        {
            inventory.coinCount -= cost;
            Debug.Log("Inventory Max " + inventory.maxCoins + " Max Cargo " + MaxCargo);
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
        if (inventory.maxCoins <= MaxCargo && currentUpgradeIndex != 3)
        {
            Debug.Log("Upgrading Player Cargo");
            inventory.maxCoins += 10;
            MaxCargo = inventory.maxCoins + 10;
            Debug.Log("Player Cargo: " + inventory.maxCoins);
        }
        else
        {
            inventory.coinCount += cost;
            Debug.Log("Max Cargo Reached");
            costText.text = "Max Cargo Reached";
        }
    }

    void UpdateUpgradeDisplay()
    {
        if (currentUpgradeIndex < CargoUpgrade.Count)
        {
            var image = CargoUpgrade[currentUpgradeIndex].GetComponent<UnityEngine.UI.Image>();
            if (image != null)
            {
                image.color = Color.green;
            }
            currentUpgradeIndex++;
        }
    }
    public override void Reset()
    {
        IsReset = true;
        Debug.Log("Resetting Cargo Upgrade");
        foreach (var upgrade in CargoUpgrade)
        {
            var image = upgrade.GetComponent<UnityEngine.UI.Image>();
            if (image != null)
            {
                image.color = Color.red;
            }
        }
        MaxCargo = inventory.maxCoins + MaxNumberOfCoins;
        currentUpgradeIndex = 0;
        cost += BaseCost;
    }
}