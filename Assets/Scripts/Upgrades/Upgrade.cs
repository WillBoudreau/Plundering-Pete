using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Upgrade : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI costText;
    public TextMeshProUGUI ButtonText;
    public TextMeshProUGUI messageText;
    [Header("Upgrade Settings")]
    public int cost;
    public int currentUpgradeIndex = 0;
    public List<GameObject> upgradeIndicators = new List<GameObject>();
    public List<GameObject> ActiveUpgradeIndicators = new List<GameObject>();
    // Start is called before the first frame update
    public InventoryManager inventory;
    public PlayerBehaviour playerBehaviour;
    public UpgradeManager upgradeManager;
    public PlayerStats playerStats;
    public PlayerMovementHandler playerMovementHandler;
    public int CostIncrement;
    void Start()
    {
        UpgradeManager upgradeManager = GameObject.Find("UpgradeManager").GetComponent<UpgradeManager>();
    }
    public abstract void CostCheck();
    public abstract void UpgradePlayer();
    public abstract void ResetUpgrade();
    public abstract void SetText();

    public void UpdateUpgradeDisplay(Color color)
    {
        if (currentUpgradeIndex < upgradeIndicators.Count && currentUpgradeIndex < ActiveUpgradeIndicators.Count)
        {
            var inactiveIndicator = upgradeIndicators[currentUpgradeIndex];
            var activeIndicator = ActiveUpgradeIndicators[currentUpgradeIndex];
            if(activeIndicator != null && inactiveIndicator != null)
            {
                inactiveIndicator.SetActive(false);
                activeIndicator.SetActive(true);
            }
            currentUpgradeIndex++;
            // var image = upgradeIndicators[currentUpgradeIndex].GetComponent<UnityEngine.UI.Image>();
            // if (image != null)
            // {
            //     image.color = Color.green;
            // }
            // currentUpgradeIndex++;
        }
    }
    public void ResetIndicator(Color defaultColor)
    {
        foreach (var upgrade in upgradeIndicators)
        {
            var image = upgrade.GetComponent<UnityEngine.UI.Image>();
            upgrade.SetActive(true);
            // var image = upgrade.GetComponent<UnityEngine.UI.Image>();
            // if (image != null)
            // {
            //     image.color = defaultColor;
            // }
        }
        foreach (var upgrade in ActiveUpgradeIndicators)
        {
            upgrade.SetActive(false);
        }
        currentUpgradeIndex = 0;
    }

}
