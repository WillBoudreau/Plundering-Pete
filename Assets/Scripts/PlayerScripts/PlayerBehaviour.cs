using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerBehaviour : HealthManager
{
    public int Doubloons = 0;
    public float speed;
    public float fireRate;
    public float damage;
    public float bulletVelocity;
    public float playerHealth;

    public TextMeshProUGUI DoubloonText;

    public Camera mainCamera;
    public Rigidbody2D rb;
    public GameObject bulletPrefab;
    public bool Win;
    public InventoryManager inventoryManager;

    // Start is called before the first frame update
    void Start()
    { 
        fireRate = 0f;
        rb = GetComponent<Rigidbody2D>();
        speed = 10.0f;
        playerHealth = 100f;
        damage = 1f;
        bulletVelocity = 25f;
        Win = false;
        health = playerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        DoubloonText = GameObject.Find("DoubloonsText").GetComponent<TextMeshProUGUI>();
        playerhealth.value = playerHealth;
        HandleMovement();
        HandleShooting();
        UpdateCounter();
        Death();
    }

    void UpdateCounter()
    {
        DoubloonText.text = "Doubloons: " + Doubloons;
    }

    void HandleShooting()
    {
        //Handle player shooting by tracking the mouse pos
        fireRate -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse0) && fireRate <= 0)
        {
            Vector2 mouseWorldPOS = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mouseWorldPOS - (Vector2)transform.position).normalized;
            
            float bulletSpawnDist = 1.0f;
            Vector3 bulletSpawnPos = (Vector2)transform.position + direction * bulletSpawnDist;
            bulletSpawnPos.z = -2;


            GameObject bullet = Instantiate(bulletPrefab,bulletSpawnPos, Quaternion.identity);

            Vector2 shootdirection = (mouseWorldPOS - (Vector2)transform.position).normalized;
            bullet.GetComponent<Rigidbody2D>().velocity = shootdirection * bulletVelocity;
            Destroy(bullet, 2.0f);
            fireRate = 10f;
        }
    }

    void HandleMovement()
    {
        //Handle input using the old input system. TODO: change to new input system
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical) * speed;
        rb.velocity = movement;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with: " + other.gameObject.tag);
        if (other.gameObject.tag == "WinTrig")
        {
            Win = true;
            uIManager.SetGameState("Win");
        }
        if (other.gameObject.tag == "Gold")
        {
            Doubloons++;
            inventoryManager.coinCount = Doubloons;
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "Obstacle")
        {
            TakeDamage(other.gameObject.GetComponent<Obstacle>().damage);
        }
    }

    public override void Death()
    {
        if (playerHealth <= 0)
        {
            IsDead = true;
            playerHealth = 0;
            uIManager.SetGameState("GameOver");
            ResetHealth();
        }
    }

    public override void TakeDamage(float damage)
    {
        Debug.Log("Player took damage: " + damage);
        Debug.Log("Player health: " + playerHealth);
        playerHealth -= damage;
        Death();
    }

    private void ResetHealth()
    {
        playerHealth = 100f;
        health = playerHealth;
        IsDead = false;
    }
}