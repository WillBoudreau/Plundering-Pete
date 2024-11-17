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
    public TextMeshProUGUI ButtonText;
    [Header("UI Elements")]
    public List<GameObject> speedUpgrade = new List<GameObject>();
    private int currentUpgradeIndex = 0;
    [Header("Upgrade Values")]
    public float MaxSpeed;
    private const int BaseCost = 10;
    private const float SpeedIncrement = 3;
    public bool IsReset = false;

    void Start()
    {
        MaxSpeed = playerStats.speed + SpeedIncrement;
        cost = BaseCost;
        // Initialize the damageUpgrade images to white
        foreach (var upgrade in speedUpgrade)
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
        speedText.text = "Speed: " + playerStats.speed;
        ButtonText.text = "Cost: " + cost;
        if(IsReset == true)
        {
            MaxSpeed = playerStats.speed + 3.0f;
            IsReset = false;
        }
    }

    public override void CostCheck()
    {
        Debug.Log("Checking Cost for Speed Upgrade");
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
        if (playerStats.speed < MaxSpeed)
        {
            playerStats.speed ++;
        }
        else
        {
            inventory.coinCount += cost;
            speedText.text = "Max Speed Reached";
        }
    }
    public override void Reset()
    {
        IsReset = true;
        foreach (var upgrade in speedUpgrade)
        {
            var image = upgrade.GetComponent<UnityEngine.UI.Image>();
            if (image != null)
            {
                image.color = Color.red;
            }
        }
        currentUpgradeIndex = 0;
        MaxSpeed = playerStats.speed + SpeedIncrement;
        cost += BaseCost;
    }

    void UpdateUpgradeDisplay()
    {
        if (currentUpgradeIndex < speedUpgrade.Count)
        {
            var image = speedUpgrade[currentUpgradeIndex].GetComponent<UnityEngine.UI.Image>();
            if (image != null)
            {
                image.color = Color.green;
            }
            currentUpgradeIndex++;
        }
    }
}
