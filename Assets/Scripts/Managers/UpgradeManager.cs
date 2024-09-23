using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeManager : MonoBehaviour
{

    //Player reference  
    public PlayerBehaviour player;
    //InventoryManager reference
    public InventoryManager inventory;
    // UpgradeSlots
    public GameObject damageSlot;
    public GameObject healthSlot;
    public GameObject speedSlot;
    //Text for the slots
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI MessageCostText;
    public TextMeshProUGUI NumberCoinsText;
    //Values and their max values
    public float damageUpgrade;
    public float damageMax;
    public float healthUpgrade;
    public float healthMax;
    public float speedUpgrade;
    public float speedMax;
    //Cost for the upgrades
    public int damageCost;
    public int healthCost;
    public int speedCost;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerBehaviour>();


        damageUpgrade = 1;
        damageMax = 6;


        healthUpgrade = 10;
        healthMax = 100;


        speedUpgrade = 1;
        speedMax = 10;

        damageCost = 15;
        healthCost = 15;
        speedCost = 15;
    }

    // Update is called once per frame
    void Update()
    {
        inventory = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        damageText.text = "Damage: " + player.damage;
        healthText.text = "Health: " + player.health;
        speedText.text = "Speed: " + player.speed;
        NumberCoinsText.text = "Doubloons " + inventory.coinCount;
        
    }
    public void UpgradeDamage()
    {
        if(player.damage < damageMax)
        {
            player.damage += damageUpgrade;
            Debug.Log("Damage upgraded to: " + damageUpgrade);
            MessageCostText.text = "Damage upgraded to: " + damageUpgrade;
        }
        else
        {
            Debug.Log("Max damage reached");
            MessageCostText.text = "Max damage reached";
        }
    }
    public void DamageCostCheck()
    {
        if(inventory.coinCount >= damageCost )
        {
            inventory.coinCount -= damageCost;
            Debug.Log("You have enough coins");
            UpgradeDamage();
        }
        else
        {
            Debug.Log("You do not have enough coins");
            MessageCostText.text = "You do not have enough coins";
        }
    }
    public void UpgradeHealth()
    {
        if(player.health < healthMax)
        {
            player.health += healthUpgrade;
            Debug.Log("Health upgraded to: " + healthUpgrade);
            MessageCostText.text = "Health upgraded to: " + healthUpgrade;

        }
        else
        {
            Debug.Log("Max health reached");
            MessageCostText.text = "Max health reached";
        }
    }
    public void HealthCostCheck()
    {
        if(inventory.coinCount >= healthCost )
        {
            inventory.coinCount -= healthCost;
            Debug.Log("You have enough coins");
            UpgradeHealth();
        }
        else
        {
            Debug.Log("You do not have enough coins");
            MessageCostText.text = "You do not have enough coins";
        }
    }
    public void UpgradeSpeed()
    {
        if(player.speed < speedMax)
        {
            player.speed += speedUpgrade;
            Debug.Log("Speed upgraded to: " + speedUpgrade);
            MessageCostText.text = "Speed upgraded to: " + speedUpgrade;
        }
        else
        {
            Debug.Log("Max speed reached");
            MessageCostText.text = "Max speed reached";
        }
    }
    public void SpeedCostCheck()
    {
        if(inventory.coinCount >= speedCost )
        {
            inventory.coinCount -= speedCost;
            Debug.Log("You have enough coins");
            UpgradeSpeed();
        }
        else
        {
            Debug.Log("You do not have enough coins");
            MessageCostText.text = "You do not have enough coins";
        }
    }
}