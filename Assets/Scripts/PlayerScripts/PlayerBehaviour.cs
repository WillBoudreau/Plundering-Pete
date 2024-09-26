using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : HealthManager
{

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
        playerHealth = 100f;
        damage = 10.0f;
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
            TakeDamage(50);
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
        if(other.gameObject.tag == "WinTrig")
        {
            Win = true;
            Debug.Log("You Win!");
        }
    }

    public override void Death()
    {
        if(playerHealth <= 0)
        {
            IsDead = true;
        }
    }
    public override void TakeDamage(float damage)
    {
        playerHealth -= damage;
        Death();
    }
}