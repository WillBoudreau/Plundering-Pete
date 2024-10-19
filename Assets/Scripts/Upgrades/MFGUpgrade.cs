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
    public int MaxMagnet;
    public List<GameObject> BFMUpgrade= new List<GameObject>();
    private int currentUpgradeIndex = 0;

    void Start()
    {
        MaxMagnet = 3;
        cost = 10;
        // Initialize the damageUpgrade images to white
        foreach (var upgrade in BFMUpgrade)
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
       MFGText.text = "Magnet: " + player.magnet + " Cost: " + cost;
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
            Debug.Log("Max Magnet Reached");
            MFGText.text = "Max Magnet Reached";
        }
    }
    public override void Reset()
    {
        foreach (var upgrade in BFMUpgrade)
        {
            var image = upgrade.GetComponent<UnityEngine.UI.Image>();
            if (image != null)
            {
                image.color = Color.green;
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
                image.color = Color.red;
            }
            currentUpgradeIndex++;
        }
    }
}
