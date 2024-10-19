using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    [Header("Class calls")]
    //Player reference  
    public PlayerBehaviour player;
    //InventoryManager reference
    public InventoryManager inventory;
    [Header("Text Slots")]
    //Text for the slots
    public TextMeshProUGUI MessageCostText;
    public TextMeshProUGUI NumberCoinsText;
    public TextMeshProUGUI CargoText;
    public TextMeshProUGUI FireRateText;
    [Header("Variables")]
    //Values and their max values

    public int CargoUpgrade;
    public int CargoMax;
    public float FireRateUpgrade;
    public float FireRateMax;

    //Start Cost and Cost for the upgrades
    public int CargoCost;
    public int StartCargoCost;
    public int FireRateCost;
    public int StartFireRateCost;
    public int ShipCost;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
        SetValues();
    }
    // Update is called once per frame
    void Update()
    {
        inventory = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        inventory.maxCoins = CargoMax;
        SetText();
    }
    void SetText()
    {
        NumberCoinsText.text = "Doubloons " + inventory.coinCount;
        CargoText.text = "Cargo: " + inventory.maxCoins + " Cost: " + CargoCost;
        FireRateText.text = "FireRate: " + player.startFireRate + " Cost: " + FireRateCost;
    }
    //Set the variable values at the start of the game
    void SetValues()
    {
        //Set the Starting costs 
        StartCargoCost = 15;
        StartFireRateCost = 15;
        ShipCost = 100;

        //Set the values to their starting costs
        CargoCost = StartCargoCost;
        FireRateCost = StartFireRateCost;

        //Set the max values
        CargoMax = 50;
        FireRateMax = 10;

        //Set the amount per upgrade
        CargoUpgrade = 5;
        FireRateUpgrade = 1;

    }
    public void SelectUpgrade(string upgrade)
    {
        switch (upgrade)
        {
            case "Cargo":
                CargoCostCheck();
                break;
            case "FireRate":
                FireRateCostCheck();
                break;
        }
    }
    public void UpgradeShip()
    {
        if (inventory.coinCount >= ShipCost)
        {
            inventory.coinCount -= ShipCost;
            Debug.Log("You have enough coins");
            if(player.IsLevel2 == false)
            {
                player.IsLevel2 = true;
            }
            else if(player.IsLevel2 == true && player.IsLevel3 == false)
            {
                player.IsLevel2 = false;
                player.IsLevel3 = true;
            }
        }
    }
    void CargoCostCheck()
    {
        if (inventory.coinCount >= CargoCost)
        {
            inventory.coinCount -= CargoCost;
            Debug.Log("You have enough coins");
            UpgradeCargo();
            CargoCost += 15;
        }
        else
        {
            Debug.Log("You do not have enough coins");
            MessageCostText.text = "You do not have enough coins";
        }
    }
    void UpgradeCargo()
    {
        if (inventory.maxCoins < CargoMax)
        {
            inventory.maxCoins += CargoUpgrade;
            Debug.Log("Cargo upgraded to: " + inventory.maxCoins);
            MessageCostText.text = "Cargo upgraded to: " + inventory.maxCoins;
        }
        else
        {
            Debug.Log("Max Cargo reached");
            MessageCostText.text = "Max Cargo reached";
            inventory.coinCount += CargoCost;
        }
    }
    void FireRateCostCheck()
    {
        if (inventory.coinCount >= FireRateCost)
        {
            inventory.coinCount -= FireRateCost;
            Debug.Log("You have enough coins");
            UpgradeFireRate();
            FireRateCost += 15;
        }
        else
        {
            Debug.Log("You do not have enough coins");
            MessageCostText.text = "You do not have enough coins";
        }
    }
    void UpgradeFireRate()
    {
        if (player.fireRate < FireRateMax)
        {
            player.fireRate += FireRateUpgrade;
            Debug.Log("FireRate upgraded to: " + player.fireRate);
            MessageCostText.text = "FireRate upgraded to: " + player.fireRate;

        }
        else
        {
            Debug.Log("Max FireRate reached");
            MessageCostText.text = "Max FireRate reached";
            inventory.coinCount += FireRateCost;
        }
    }
}

