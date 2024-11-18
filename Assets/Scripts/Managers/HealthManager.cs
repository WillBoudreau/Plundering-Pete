using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public UIManager uIManager;
    public GameObject player;
    public Image playerhealth;
    public float health;
    public float maxHealth;
    public bool IsDead;
    // Start is called before the first frame update
    void Start()
    {
        IsDead = false;
        uIManager = FindObjectOfType<UIManager>();
        health = maxHealth;
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
