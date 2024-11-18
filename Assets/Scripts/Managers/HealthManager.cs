using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthManager : MonoBehaviour
{
    public UIManager uIManager;
    public GameObject player;
    public Image playerhealth;
    public TextMeshProUGUI healthText;
    public float health;
    public float maxHealth;
    private const float StartingHealth = 5;
    public bool IsDead;
    // Start is called before the first frame update
    void Start()
    {
        IsDead = false;
        maxHealth = StartingHealth;
        uIManager = FindObjectOfType<UIManager>();
        health = maxHealth;
        HandlePlayerHealthBar(health, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            health = 0;
        }
    }
    public void HandlePlayerHealthBar(float health, float maxHealth)
    {
        playerhealth.fillAmount = health / maxHealth;
        HealthCounter(health, maxHealth);
    }
    void HealthCounter(float health, float maxHealth)
    {
        healthText.text = "HP: " + health + "/" + maxHealth;
    }
    public virtual void Death()
    {
        if (health <= 0)
        {
            health = 0;
            Destroy(gameObject);
        }
        IsDead = true;
        uIManager.currentGameState = UIManager.GameState.GameOver;
        uIManager.SetGameState("GameOver");
    }
}
