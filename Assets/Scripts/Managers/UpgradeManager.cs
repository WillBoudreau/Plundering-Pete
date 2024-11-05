using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    [Header("Class calls")]
    //Player reference  
    public PlayerStats player;
    public ShipUpgrade shipUpgrade;
    public List<Upgrade> upgrades = new List<Upgrade>();
    //InventoryManager reference
    public InventoryManager inventory;
    [Header("Text Slots")]
    //Text for the slots
    public TextMeshProUGUI NumberCoinsText;
    [Header("Variables")]
    //Values and their max values
    //Start Cost and Cost for the upgrades
    public int ShipCost;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerStats>();
        SetValues();
    }
    // Update is called once per frame
    void Update()
    {
        inventory = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        SetText();
    }
    void SetText()
    {
        NumberCoinsText.text = "Doubloons " + inventory.coinCount + "/" + inventory.maxCoins;
    }
    //Set the variable values at the start of the game
    void SetValues()
    {
        ShipCost = shipUpgrade.cost;
    }
    public void UpgradeShip()
    {
        if(inventory.coinCount >= ShipCost && player.IsLevel3 == false)
        {
            Debug.Log("Upgrading Ship from manager");
            if(player.IsLevel2 == false)
            {
                Debug.Log("Level 2");
                player.IsLevel2 = true;
                Reset();
            }
            else if(player.IsLevel2 == true && player.IsLevel3 == false)
            {
                player.IsLevel2 = false;
                player.IsLevel3 = true;
                Reset();
            }
        }
    }
    public void Reset()
    {
        foreach (var upgrade in upgrades)
        {
            upgrade.Reset();
        }
    }
}