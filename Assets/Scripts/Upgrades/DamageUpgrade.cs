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
    public float MaxDamage;
    public List<GameObject> damageUpgrade = new List<GameObject>();
    private int currentUpgradeIndex = 0;
    public bool IsReset = false;

    void Start()
    {
        MaxDamage = player.damage + 3;
        cost = 10;
        // Initialize the damageUpgrade images to white red 
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
        damageText.text = "Damage: " + player.damage;
        ButtonText.text = "Cost: " + cost;
        if(IsReset == true)
        {
            MaxDamage = player.damage + 3;
            IsReset = false;
        }
    }

    public override void CostCheck()
    {
        Debug.Log("Checking Cost for Damage Upgrade");
        if (inventory.coinCount >= cost)
        {
            Debug.Log("Upgrading Player Damage");
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
        if (player.damage < MaxDamage)
        {
            Debug.Log("Upgrading Player Damage");
            Debug.Log("Player Damage: " + player.damage + " Max Damage: " + MaxDamage);
            player.damage ++;
            Debug.Log("Player Damage: " + player.damage);
        }
        else
        {
            inventory.coinCount += cost;
            Debug.Log("Max Damage Reached");
            damageText.text = "Max Damage Reached";
            Debug.Log("Max Damage Reached");
            Debug.Log("Player Damage: " + player.damage + " Max Damage: " + MaxDamage);
        }
    }
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
        MaxDamage = player.damage + 3;
        cost += 10;
        Debug.Log("Resetting Damage Upgrade");
        Debug.Log("Player Damage: " + player.damage + " Max Damage: " + MaxDamage);
    }

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
