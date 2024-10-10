using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    //Base Enemy variables
    public float health;
    public float speed;
    public float damage;
    public Renderer renderer;
    public Color originalColor;
    public float FlickerDuration;
    public int FlickerCount;
    //Get a refernce to the player
    public PlayerBehaviour player;
    //Base Enemy functions
    public abstract void Move();
    public abstract void TakeDamage(float damage);
    public abstract IEnumerator Flicker();
}

