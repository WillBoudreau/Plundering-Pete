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
    public float MaxDamage;
    public List<GameObject> damageUpgrade = new List<GameObject>();
    private int currentUpgradeIndex = 0;

    void Start()
    {
        MaxDamage = player.damage + 3;
        cost = 10;
        // Initialize the damageUpgrade images to white
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
        damageText.text = "Damage: " + player.damage + " Cost: " + cost;
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
        if (player.damage < MaxDamage)
        {
            Debug.Log("Upgrading Player Damage");
            player.damage ++;
            Debug.Log("Player Damage: " + player.damage);
        }
        else
        {
            inventory.coinCount += cost;
            Debug.Log("Max Damage Reached");
            damageText.text = "Max Damage Reached";
        }
    }
    public override void Reset()
    {
        foreach (var upgrade in damageUpgrade)
        {
            var image = upgrade.GetComponent<UnityEngine.UI.Image>();
            if (image != null)
            {
                image.color = Color.red;
            }
        }
        currentUpgradeIndex = 0;
        MaxDamage += 3;
        cost += 10;
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
