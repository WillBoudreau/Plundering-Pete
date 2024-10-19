using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeedUpgrade : Upgrade
{
    // Start is called before the first frame update
    [Header("Upgrade Values")]
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI costText;
    public int MaxSpeed;
    public List<GameObject> speedUpgrade = new List<GameObject>();
    private int currentUpgradeIndex = 0;

    void Start()
    {
        MaxSpeed = 3;
        cost = 10;
        // Initialize the damageUpgrade images to white
        foreach (var upgrade in speedUpgrade)
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
        speedText.text = "Speed" + player.speed + " Cost: " + cost;
    }

    public override void CostCheck()
    {
        Debug.Log("Checking Cost for Damage Upgrade");
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
        if (player.damage < MaxSpeed)
        {
            Debug.Log("Upgrading Player Speed");
            player.speed ++;
            Debug.Log("Player Damage: " + player.speed);
        }
        else
        {
            Debug.Log("Max Damage Reached");
            speedText.text = "Max Speed Reached";
        }
    }

    void UpdateUpgradeDisplay()
    {
        if (currentUpgradeIndex < speedUpgrade.Count)
        {
            var image = speedUpgrade[currentUpgradeIndex].GetComponent<UnityEngine.UI.Image>();
            if (image != null)
            {
                image.color = Color.red;
            }
            currentUpgradeIndex++;
        }
    }
}
