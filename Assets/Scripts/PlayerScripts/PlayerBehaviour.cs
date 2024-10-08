using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerBehaviour : MonoBehaviour
{
    public int Doubloons = 0;

    //Player values 
    public float speed;
    public float fireRate;
    public float damage;
    public float playerHealth;
    
    
    //Starting values
    public float startHealth;
    public float startdamage;
    public float StartSpeed;
    public float startFireRate;

    public float bulletVelocity;
    public bool Win;

    public TextMeshProUGUI DoubloonText;
    public Transform firePoint;

    public Camera mainCamera;
    public Rigidbody2D rb;
    public GameObject bulletPrefab;
    [Header("Renderer Calls")]
    Renderer renderer;
    private Color originalColor;
    public float FlickerDuration = 0.1f; 
    public int FlickerCount = 5;

    [Header("Class calls")]
    public InventoryManager inventoryManager;
    public UIManager uIManager;
    public HealthManager healthManager;
    public MusicChanger musicManager;

    // Start is called before the first frame update
    void Start()
    { 
        renderer = GetComponent<Renderer>();
        originalColor = GetComponent<Renderer>().material.color;
        rb = GetComponent<Rigidbody2D>();
        SetValues();
    }

    // Update is called once per frame
    void Update()
    {
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        DoubloonText = GameObject.Find("DoubloonsText").GetComponent<TextMeshProUGUI>();
        healthManager.playerhealth.value = playerHealth;
        //SetValues();
        HandleMovement();
        HandleShooting();
        UpdateCounter();
        Death();
    }
    void SetValues()
    {
        //Set starting values
        startHealth = 5;
        startdamage = 1;
        StartSpeed = 10;
        startFireRate = 2;
        bulletVelocity = 25f;
        healthManager.health = playerHealth;
        //Assign values to be the starting values
        playerHealth = startHealth;
        speed = StartSpeed;
        damage = startdamage;
        fireRate = startFireRate;
        //Set Win Bool
        Win = false;

    }
    //Update the Doubloon counter
    void UpdateCounter()
    {
        DoubloonText.text = "Doubloons: " + Doubloons;
    }
    //Handle the players shooting
    void HandleShooting()
    {
        //Handle player shooting by tracking the mouse pos
        fireRate -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse0) && fireRate <= 0)
        {
            Vector2 mouseWorldPOS = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mouseWorldPOS - (Vector2)transform.position).normalized;
            
            float bulletSpawnDist = 1.0f;
            Vector3 bulletSpawnPos = firePoint.position + (firePoint.forward * bulletSpawnDist);
            bulletSpawnPos.z = -2;

            musicManager.PlaySound(0);
            GameObject bullet = Instantiate(bulletPrefab,bulletSpawnPos, Quaternion.identity);

            Vector2 shootdirection = (mouseWorldPOS - (Vector2)transform.position).normalized;
            bullet.GetComponent<Rigidbody2D>().velocity = shootdirection * bulletVelocity;
            Destroy(bullet, 2.0f);
            fireRate = 2f;
        }
    }
    //Handle the player movement
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
        if(other.gameObject.tag == "Enemy")
        {
            TakeDamage(other.gameObject.GetComponent<SharkBahaviour>().damage);
        }
    }

    public void Death()
    {
        if (playerHealth <= 0)
        {
            healthManager.IsDead = true;
            playerHealth = 0;
            uIManager.SetGameState("GameOver");
            ResetHealth();
        }
    }

    public void TakeDamage(float damage)
    {
        StartCoroutine(Flicker());
        playerHealth -= damage;
        Death();
    }

    private void ResetHealth()
    {
        playerHealth = startHealth;
        healthManager.health = playerHealth;
        healthManager.IsDead = false;
    }
    //Flicker for damaage
    IEnumerator Flicker()
    {
        for(int i = 0; i < FlickerCount; i++)
        {
            GetComponent<Renderer>().material.color = Color.red;
            yield return new WaitForSeconds(FlickerDuration);
            GetComponent<Renderer>().material.color = originalColor;
            yield return new WaitForSeconds(FlickerDuration);
        }
        GetComponent<Renderer>().material.color = originalColor;
    }
}