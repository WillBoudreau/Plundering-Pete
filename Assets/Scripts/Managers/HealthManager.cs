using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public UIManager uIManager;
    public GameObject player;
    public Slider playerhealth;
    public float health;
    public bool IsDead;
    // Start is called before the first frame update
    void Start()
    {
        IsDead = false;
        uIManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            health = 0;
        }
    }
    public virtual void TakeDamage(float damage)
    {
        health -= damage;
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
