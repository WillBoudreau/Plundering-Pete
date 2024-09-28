using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : HealthManager
{

    public int Doubloons = 0;
    public float speed;
    public float fireRate;
    public float damage;
    public float bulletVelocity;
    public float playerHealth;

    public Camera mainCamera;
    public Rigidbody2D rb;
    public GameObject bulletPrefab;
    public bool Win;


    // Start is called before the first frame update
    void Start()
    { 
        fireRate = 0f;
        rb = GetComponent<Rigidbody2D>();
        speed = 10.0f;
        playerHealth = 5f;
        damage = 1f;
        bulletVelocity = 25f;
        Win = false;
        health = playerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        playerhealth.value = playerHealth;
        HandleMovement();
        HandleShooting();
        Death();
    }
    void HandleShooting()
    {
        //Handle player shooting by tracking the mouse pos
        fireRate -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse0) && fireRate <= 0)
        {
            Vector2 mouseWorldPOS = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            GameObject bullet = Instantiate(bulletPrefab, transform.position,Quaternion.identity);
            Vector2 shootdirection = (mouseWorldPOS - (Vector2)transform.position).normalized;
            bullet.GetComponent<Rigidbody2D>().velocity = shootdirection * bulletVelocity;
            Destroy(bullet, 2.0f);
            fireRate = 10f;
        }
    }
    void HandleMovement()
    {
        //Handle input using the old inout system. TODO: change to new input system
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical) * speed;
        rb.velocity = movement;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with: " + other.gameObject.name);
        switch(other.gameObject.tag)
        {
            case "Doubloon":
                Doubloons++;
                Destroy(other.gameObject);
                break;
            case "Win":
                Win = true;
                break;
            case "Enemy":
                TakeDamage(10);
                break;
        }
    }

    public override void Death()
    {
        if(playerHealth <= 0)
        {
            IsDead = true;
            playerHealth = 0;
            uIManager.SetGameState("GameOver");
        }
    }
    public override void TakeDamage(float damage)
    {
        Debug.Log("Player took damage: " + damage);
        Debug.Log("Player health: " + playerHealth);
        playerHealth -= damage;
        Death();
    }
}