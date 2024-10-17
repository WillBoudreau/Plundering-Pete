using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerBehaviour : MonoBehaviour
{

    //Player values 
    public float speed;
    public float fireRate;
    public float damage;
    public float playerHealth;
    public float magnet;
    public int Doubloons = 0;
    [Header("Number of Kills")]
    public int SharkKills;
    public int SerpentKills;
    public int ShipKills;
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
    public Renderer renderer;
    private Color originalColor;
    public float FlickerDuration = 0.1f; 
    public int FlickerCount = 5;

    [Header("Class calls")]
    public InventoryManager inventoryManager;
    public LevelManager levelManager;
    public UIManager uIManager;
    public HealthManager healthManager;
    public MusicChanger musicManager;

    // Start is called before the first frame update
    void Start()
    { 
        renderer = GetComponentInChildren<Renderer>();
        originalColor = GetComponentInChildren<Renderer>().material.color;
        rb = GetComponent<Rigidbody2D>();
        SetValues();
    }

    // Update is called once per frame
    void Update()
    {
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        DoubloonText = GameObject.Find("DoubloonsText").GetComponent<TextMeshProUGUI>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        healthManager.playerhealth.value = playerHealth;
        //SetValues();
        HandlePlayer();
    }
    void HandlePlayer()
    {
        StaywithinBounds();
        HandleMovement();
        HandleShooting();
        UpdateCounter();
        HandleMagnit();
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
        magnet = 2f;
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
            fireRate = 0;
            Vector2 mouseWorldPOS = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mouseWorldPOS - (Vector2)transform.position).normalized;
            
            float bulletSpawnDist = 1.0f;
            Vector3 bulletSpawnPos = firePoint.position + (firePoint.forward * bulletSpawnDist);
            bulletSpawnPos.z = -2;

            musicManager.PlaySound(0);
            GameObject bullet = Instantiate(bulletPrefab,bulletSpawnPos, Quaternion.identity);

            //Vector2 shootdirection = (mouseWorldPOS - (Vector2)transform.position).normalized;
            bullet.GetComponent<Rigidbody2D>().velocity = Vector2.up * bulletVelocity;
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
    void StaywithinBounds()
    {
        // Ensure the player remains within the camera bounds
        Vector3 pos = transform.position;
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(pos);
        viewportPos.x = Mathf.Clamp(viewportPos.x, 0.05f, 0.95f);
        viewportPos.y = Mathf.Clamp(viewportPos.y, 0.05f, 0.95f);
        transform.position = mainCamera.ViewportToWorldPoint(viewportPos);
    }
    void HandleMagnit()
    {
        //Handle the magnet powerup
        foreach (GameObject coin in GameObject.FindGameObjectsWithTag("Gold"))
        {
            if (Vector2.Distance(transform.position, coin.transform.position) < magnet)
            {
                coin.transform.position = Vector2.MoveTowards(coin.transform.position, transform.position, 0.1f);
            }
        }
    }
    //Handle the player colliding with objects
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with: " + other.gameObject.tag);
        switch(other.gameObject.tag)
        {
            case "WinTrig":
            Win = true;
            uIManager.SetGameState("Win");
            break;
            case "Gold":
            if(inventoryManager.IsMax == false)
            {
                Doubloons++;
                inventoryManager.coinCount = Doubloons;
                other.gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("Max coins reached");
                other.gameObject.SetActive(true);
                magnet = 0;
            }
            break;
            case "Obstacle":
            TakeDamage(other.gameObject.GetComponent<Obstacle>().damage);
            break;
            case "Shark":
            TakeDamage(other.gameObject.GetComponent<SharkBahaviour>().damage);
            break;
        }
    }

    public void Death()
    {
        if (playerHealth <= 0)
        {
            healthManager.IsDead = true;
            playerHealth = 0;
            //gameObject.SetActive(false);
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
            GetComponentInChildren<Renderer>().material.color = Color.red;
            yield return new WaitForSeconds(FlickerDuration);
            GetComponentInChildren<Renderer>().material.color = originalColor;
            yield return new WaitForSeconds(FlickerDuration);
        }
        GetComponentInChildren<Renderer>().material.color = originalColor;
    }
}