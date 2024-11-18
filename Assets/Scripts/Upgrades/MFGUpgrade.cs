using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MFGUpgrade : Upgrade
{
// Start is called before the first frame update
    [Header("Upgrade Values")]
    public TextMeshProUGUI MFGText;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI ButtonText;
    [Header("UI Elements")]
    public List<GameObject> BFMUpgrade= new List<GameObject>();
    private int currentUpgradeIndex = 0;
    [Header("Upgrade Values")]
    public float MaxMagnet;
    private const int BaseCost = 10;
    private const float MagnetIncrement = 3;
    public bool IsReset = false;

    void Start()
    {
        MaxMagnet = playerStats.magnet + MagnetIncrement;
        cost = BaseCost;
        // Initialize the damageUpgrade images to white
        foreach (var upgrade in BFMUpgrade)
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
       MFGText.text = "Magnet: " + playerStats.magnet;
       ButtonText.text = "$" + cost;
         if(IsReset == true)
         {
              MaxMagnet = playerStats.magnet + MagnetIncrement;
              IsReset = false;
         }
    }

    // Check the cost of the upgrade
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

    // Upgrade the player
    public override void UpgradePlayer()
    {
        if (playerStats.magnet < MaxMagnet)
        {
            playerStats.magnet ++;
        }
        else
        {
            inventory.coinCount += cost;
            MFGText.text = "Max Magnet Reached";
        }
    }

    // Reset the upgrade
    public override void Reset()
    {
        IsReset = true;
        foreach (var upgrade in BFMUpgrade)
        {
            var image = upgrade.GetComponent<UnityEngine.UI.Image>();
            if (image != null)
            {
                image.color = Color.red;
            }
        }
        currentUpgradeIndex = 0;
        MaxMagnet += MagnetIncrement;
        cost += BaseCost;
    }

    // Update the upgrade display
    void UpdateUpgradeDisplay()
    {
        if (currentUpgradeIndex < BFMUpgrade.Count)
        {
            var image = BFMUpgrade[currentUpgradeIndex].GetComponent<UnityEngine.UI.Image>();
            if (image != null)
            {
                image.color = Color.green;
            }
            currentUpgradeIndex++;
        }
    }
}
