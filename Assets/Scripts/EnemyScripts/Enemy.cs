using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    //Base Enemy variables
    public float health;
    public float speed;
    public float damage;
    public GameObject Gold;
    public GameObject GoldBag;
    public Renderer renderer;
    public Color originalColor;
    public float FlickerDuration;
    public int FlickerCount;
    public Slider healthBar;
    public const int StartPOSZ = -2;
    public float AttackTimer;
    [Header("Player")]
    //Get a refernce to the player
    // public PlayerBehaviour player;
    public GameObject player;
    public PlayerStats playerStats;
    //Base Enemy functions
    public abstract void Move();
    public abstract void TakeDamage(float damage);
    public abstract IEnumerator Flicker();
    public abstract void Attack();
}

