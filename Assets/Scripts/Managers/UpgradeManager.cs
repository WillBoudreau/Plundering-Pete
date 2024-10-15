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
    [Header("Text Slots")]
    //Text for the slots
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI MessageCostText;
    public TextMeshProUGUI NumberCoinsText;
    public TextMeshProUGUI MagnetText;
    public TextMeshProUGUI CargoText;
    public TextMeshProUGUI FireRateText;
    [Header("Variables")]
    //Values and their max values
    public float damageUpgrade;
    public float damageMax;
    public float healthUpgrade;
    public float healthMax;
    public float speedUpgrade;
    public float speedMax;
    public float magnetUpgrade;
    public float magnetMax;
    public int CargoUpgrade;
    public int CargoMax;
    public float FireRateUpgrade;
    public float FireRateMax;

    //Start Cost and Cost for the upgrades
    public int damageCost;
    public int StartDanageCost;
    public int healthCost;
    public int StartHealthCost;
    public int speedCost;
    public int StartSpeedCost;
    public int magnetCost;
    public int StartMagnetCost;
    public int CargoCost;
    public int StartCargoCost;
    public int FireRateCost;
    public int StartFireRateCost;


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
            SetText();
        }
        void SetText()
        {
            NumberCoinsText.text = "Doubloons " + inventory.coinCount;
            damageText.text = "Damage: " + player.damage + " Cost: " + damageCost;
            healthText.text = "Health: " + player.playerHealth + " Cost: " + healthCost;
            speedText.text = "Speed: " + player.speed + " Cost: " + speedCost;
            MagnetText.text = "Magnet: " + player.magnet + " Cost: " + magnetCost;
            CargoText.text = "Cargo: " + inventory.maxCoins + " Cost: " + CargoCost;
            FireRateText.text = "FireRate: " + player.fireRate + " Cost: " + FireRateCost;
        }
        //Set the variable values at the start of the game
        void SetValues()
        {
            //Set the Starting costs 
            StartDanageCost = 15;
            StartHealthCost = 15;
            StartSpeedCost = 15;
            StartMagnetCost = 15;
            StartCargoCost = 15;
            StartFireRateCost = 15;

            //Set the values to their starting costs
            damageCost = StartDanageCost;
            healthCost = StartHealthCost;
            speedCost = StartSpeedCost;
            magnetCost = StartMagnetCost;
            CargoCost = StartCargoCost;
            FireRateCost = StartFireRateCost;

            //Set the max values
            damageMax = 4;
            speedMax = 25;
            healthMax = 20;
            magnetMax = 10;
            CargoMax = 50;
            FireRateMax = 10;

            //Set the amount per upgrade
            damageUpgrade = 1;
            healthUpgrade = 5;
            speedUpgrade = 5;
            magnetUpgrade = 1;
            CargoUpgrade = 5;
            FireRateUpgrade = 1;

        }
        public void SelectUpgrede(string upgrade)
        {
            switch (upgrade)
            {
                case "Damage":
                    DamageCostCheck();
                    break;
                case "Health":
                    HealthCostCheck();
                    break;
                case "Speed":
                    SpeedCostCheck();
                    break;
                case "Magnet":
                    MagnetCostCheck();
                    break;
                case "Cargo":
                    CargoCostCheck();
                    break;
                case "FireRate":
                    FireRateCostCheck();
                    break;
            }
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
        public void UpgradeMagnet()
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
        public void MagnetCostCheck()
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
        public void CargoCostCheck()
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
        public void UpgradeCargo()
        {
            if(inventory.maxCoins < CargoMax)
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
        public void FireRateCostCheck()
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
        public void UpgradeFireRate()
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