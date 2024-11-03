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
    public float MaxMagnet;
    public List<GameObject> BFMUpgrade= new List<GameObject>();
    private int currentUpgradeIndex = 0;
    public bool IsReset = false;

    void Start()
    {
        MaxMagnet = player.magnet + 3;
        cost = 10;
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
       MFGText.text = "Magnet: " + player.magnet;
       ButtonText.text = "Cost: " + cost;
         if(IsReset == true)
         {
              MaxMagnet = player.magnet + 3;
              IsReset = false;
         }
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
        if (player.magnet < MaxMagnet)
        {
            Debug.Log("Upgrading Player Magnet");
            player.magnet ++;
            Debug.Log("Player Magnet: " + player.magnet);
        }
        else
        {
            inventory.coinCount += cost;
            Debug.Log("Max Magnet Reached");
            MFGText.text = "Max Magnet Reached";
        }
    }
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
        MaxMagnet += 3;
        cost += 10;
    }

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
