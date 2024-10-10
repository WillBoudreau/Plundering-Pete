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
    //Start Cost and Cost for the upgrades
    public int damageCost;
    public int StartDanageCost;
    public int healthCost;
    public int StartHealthCost;
    public int speedCost;
    public int StartSpeedCost;


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
            damageText.text = "Damage: " + player.damage;
            healthText.text = "Health: " + player.startHealth;
            speedText.text = "Speed: " + player.StartSpeed;
            NumberCoinsText.text = "Doubloons " + inventory.coinCount;

        }
        //Set the variable values at the start of the game
        void SetValues()
        {
            //Set the Starting costs 
            StartDanageCost = 15;
            StartHealthCost = 15;
            StartSpeedCost = 15;

            //Set the values to their starting costs
            damageCost = StartDanageCost;
            healthCost = StartHealthCost;
            speedCost = StartSpeedCost;

            //Set the max values
            damageMax = 4;
            speedMax = 25;
            healthMax = 20;

            //Set the amount per upgrade
            damageUpgrade = 1;
            healthUpgrade = 5;
            speedUpgrade = 5;

        }
        public void UpgradeDamage()
        {
            if (player.damage < damageMax)
            {
                player.damage += damageUpgrade;
                Debug.Log("Damage upgraded to: " + player.damage);
                MessageCostText.text = "Damage upgraded to: " + player.damage;
            }
            else
            {
                Debug.Log("Max damage reached");
                MessageCostText.text = "Max damage reached";
                inventory.coinCount += damageCost;
            }
        }
        public void DamageCostCheck()
        {
            if (inventory.coinCount >= damageCost)
            {
                inventory.coinCount -= damageCost;
                Debug.Log("You have enough coins");
                UpgradeDamage();
                damageCost += 15;
            }
            else
            {
                Debug.Log("You do not have enough coins");
                MessageCostText.text = "You do not have enough coins";
            }
        }
        public void UpgradeHealth()
        {
            if (player.playerHealth < healthMax)
            {
                player.playerHealth += healthUpgrade;
                Debug.Log("Health upgraded to: " + healthUpgrade);
                MessageCostText.text = "Health upgraded to: " + healthUpgrade;

            }
            else
            {
                Debug.Log("Max health reached");
                MessageCostText.text = "Max health reached";
                inventory.coinCount += healthCost;
            }
        }
        public void HealthCostCheck()
        {
            if (inventory.coinCount >= healthCost)
            {
                inventory.coinCount -= healthCost;
                Debug.Log("You have enough coins");
                UpgradeHealth();
                healthCost += 15;
            }
            else
            {
                Debug.Log("You do not have enough coins");
                MessageCostText.text = "You do not have enough coins";
            }
        }
        public void UpgradeSpeed()
        {
            if (player.speed < speedMax)
            {
                player.speed += speedUpgrade;
                Debug.Log("Speed upgraded to: " + speedUpgrade);
                MessageCostText.text = "Speed upgraded to: " + speedUpgrade;
            }
            else
            {
                Debug.Log("Max speed reached");
                MessageCostText.text = "Max speed reached";
                inventory.coinCount += speedCost;
            }
        }
        public void SpeedCostCheck()
        {
            if (inventory.coinCount >= speedCost)
            {
                inventory.coinCount -= speedCost;
                Debug.Log("You have enough coins");
                UpgradeSpeed();
                speedCost += 15;
            }
            else
            {
                Debug.Log("You do not have enough coins");
                MessageCostText.text = "You do not have enough coins";
            }
        }
}