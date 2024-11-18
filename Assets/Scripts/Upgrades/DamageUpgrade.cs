using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamageUpgrade : Upgrade
{
    // Start is called before the first frame update
    [Header("Upgrade Values")]
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI ButtonText;

    [Header("UI Elements")]
    //UI Elements for the upgrade
    public List<GameObject> damageUpgrade = new List<GameObject>();
    private int currentUpgradeIndex = 0;

    [Header("Upgrade Values")]
    //Upgrade Values
    private const int DamageIncrement = 3;
    private const int BaseCost = 10;
    public float MaxDamage;
    public bool IsReset = false;

    void Start()
    {
        MaxDamage = playerStats.damage + DamageIncrement;
        cost = BaseCost;
        // Initialize the damageUpgrade images to red 
        foreach (var upgrade in damageUpgrade)
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
        damageText.text = "Damage: " + playerStats.damage;
        ButtonText.text = "$" + cost;
        if(IsReset == true)
        {
            MaxDamage = playerStats.damage + 3;
            IsReset = false;
        }
    }

    //Check the cost of the upgrade
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
            costText.text = "Not enough coins";
        }
    }

    //Upgrade the player damage
    public override void UpgradePlayer()
    {
        if (playerStats.damage < MaxDamage)
        {
            playerStats.damage ++;
        }
        else
        {
            inventory.coinCount += cost;
            damageText.text = "Max Damage Reached";
        }
    }

    //Reset the upgrade
    public override void Reset()
    {
        IsReset = true;
        foreach (var upgrade in damageUpgrade)
        {
            var image = upgrade.GetComponent<UnityEngine.UI.Image>();
            if (image != null)
            {
                image.color = Color.red;
            }
        }
        currentUpgradeIndex = 0;
        MaxDamage = playerStats.damage + DamageIncrement;
        cost += BaseCost;
    }

    //Update the upgrade display
    void UpdateUpgradeDisplay()
    {
        if (currentUpgradeIndex < damageUpgrade.Count)
        {
            var image = damageUpgrade[currentUpgradeIndex].GetComponent<UnityEngine.UI.Image>();
            if (image != null)
            {
                image.color = Color.green;
            }
            currentUpgradeIndex++;
        }
    }
}
