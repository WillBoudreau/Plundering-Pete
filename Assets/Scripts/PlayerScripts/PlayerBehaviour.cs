using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Number of Kills")]
    public int SharkKills;
    public int SerpentKills;
    public int ShipKills;
    [Header("Starting values")]
    //Starting values
    public float startHealth;
    public float startdamage;
    public float StartSpeed;
    public float startFireRate;
    [Header("Player values")]
    //Player values 
    public float speed;
    public float fireRate;
    public float damage;
    public float playerHealth;
    public float magnet;
    public float bulletVelocity;
    public int checkpoint;
    public LayerMask groundLayer;
    public bool Win;
    public bool IsLevel2;
    public bool IsLevel3; 
    public bool IsMoving;
    public TextMeshProUGUI DoubloonText;
    public Transform firePoint;
    public int Level;
    public Camera mainCamera;
    public Rigidbody2D rb;
    public GameObject bulletPrefab;
    public GameObject level2BulletPrefab;
    public float time = 5.0f;
    public GameObject level3BulletPrefab;
    public bool PlayerPlaced = false;
    [Header("Renderer Calls")]
    public Renderer renderer;
    private Color originalColor;
    public float FlickerDuration = 0.1f; 
    public int FlickerCount = 5;
    public List<GameObject> PlayerLevels = new List<GameObject>();

    [Header("Class calls")]
    public InventoryManager inventoryManager;
    public LevelManager levelManager;
    public UIManager uIManager;
    public HealthManager healthManager;
    public MusicChanger musicManager;
    public CheckpointManager checkpointManager;
    public DistanceTracker distanceTracker;

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
        HandlePlayer();
        LevelUp();
        GetPlayerLayerMask();
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        DoubloonText = GameObject.Find("DoubloonsText").GetComponent<TextMeshProUGUI>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        healthManager.playerhealth.value = playerHealth;

        //SetValues();
    }
    void HandlePlayer()
    {
        StaywithinBounds();
        HandleMovement();
        HandleShooting();
        UpdateCounter();
        HandleMagnit();
    }
    void SetValues()
    {
        //Set starting values
        Level = 1;
        startHealth = 5;
        startdamage = 1;
        StartSpeed = 6;
        startFireRate = 2;
        bulletVelocity = 25f;
        healthManager.health = playerHealth;
        magnet = 3f;
        //Assign values to be the starting values
        playerHealth = startHealth;
        healthManager.playerhealth.maxValue = playerHealth;
        speed = StartSpeed;
        damage = startdamage;
        fireRate = startFireRate;
        //Set Win Bool
        Win = false;

    }
    public void LevelUp()
    {
        if(Level == 1)
        {
            Level = 1;
            PlayerLevels[0].SetActive(true);
        }
        else if(Level == 2 || IsLevel2)
        {
            Level ++;
            if(Level == 2)
            {
                IsLevel2 = true;
            }
            else if(Level >= 2)
            {
                Level = 2;
            }
            PlayerLevels[0].SetActive(false);
            PlayerLevels[1].SetActive(true);
        }
        else if(Level == 3 || IsLevel3)
        {
            Level = 3;
            if(Level == 3)
            {
                IsLevel3 = true;
            }
            else if(Level >= 3)
            {
                Level = 3;
            }
            PlayerLevels[0].SetActive(false);
            PlayerLevels[1].SetActive(false);
            PlayerLevels[2].SetActive(true);
        }
    }
    void UpdateCounter()
    {
        DoubloonText.text = "Doubloons: " + inventoryManager.coinCount;
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
            if(IsLevel2)
            {
                for(int i = 0; i < 2; i++)
                {
                    GameObject bullet1 = Instantiate(level2BulletPrefab, bulletSpawnPos, Quaternion.identity); 
                    bullet1.GetComponent<Rigidbody2D>().velocity = Vector2.up * bulletVelocity;
                    Destroy(bullet1, 2.0f);
                }
            }
            else if(IsLevel3)
            {
                for(int i = 0; i < 3; i++)
                {
                    GameObject bullet2 = Instantiate(level3BulletPrefab, bulletSpawnPos, Quaternion.identity);
                    bullet2.GetComponent<Rigidbody2D>().velocity = Vector2.up * bulletVelocity;
                    Destroy(bullet2, 2.0f);
                }
            }
            else
            {
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPos, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = Vector2.up * bulletVelocity;
                Destroy(bullet, 2.0f);
            }
            fireRate = startFireRate;
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
        if(rb.velocity.magnitude > 0)
        {
            IsMoving = true;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(damage);
        }
        else
        {
            IsMoving = false;
        }
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
        switch(other.gameObject.tag)
        {
            case "WinTrig":
            Win = true;
            uIManager.SetGameState("Win");
            break;
            case "Gold":
            if(inventoryManager.IsMax == false)
            {
                inventoryManager.coinCount ++;
                other.gameObject.SetActive(false);
            }
            else
            {
                other.gameObject.SetActive(true);
                magnet = 0;
            }
            break;
            case "CoinBag":
            if(inventoryManager.IsMax == false)
            {
                inventoryManager.coinCount += 5;
                other.gameObject.SetActive(false);
            }
            else
            {
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
            case "Serpent":
            TakeDamage(other.gameObject.GetComponent<SerpentBehaviour>().damage);
            break;
            case "EnemyShip":
            TakeDamage(other.gameObject.GetComponent<EnemyShipBehaviour>().damage);
            break;
            case "CanonBall":
            var enemyShip = other.gameObject.GetComponent<EnemyShipBehaviour>();
            if (enemyShip != null)
            {
                TakeDamage(enemyShip.damage);
                Destroy(other.gameObject);
            }
            break;
        }
    }
    public void Death()
    {
        if (playerHealth <= 0)
        {
            // healthManager.IsDead = true;
            playerHealth = 0;
            uIManager.SetGameState("GameOver");
            ResetHealth();
            Debug.Log("Player is Dead");
            levelManager.SetFalse();
            levelManager.ClearScene();
            Debug.Log("Setting levelMan False");
            checkpointManager.SetFalse();
            Debug.Log("Setting Checkpoint False");
            distanceTracker.SetFalse();
            Debug.Log("Setting Distance False");
            Respawn();
        }
    }
    void Respawn()
    {
        levelManager.PlacePlayer();
        healthManager.IsDead = false;
    }
    private void ResetHealth()
    {
        Debug.Log("Resetting Health");
        playerHealth = startHealth;
        Debug.Log("Player Health: " + playerHealth);
        healthManager.health = playerHealth;
        Debug.Log("Health Manager Health: " + healthManager.health);
    }
    public void TakeDamage(float damage)
    {
        StartCoroutine(Flicker());
        musicManager.PlaySound(2);
        playerHealth -= damage;
        Death();
    }

    //Flicker for damage
    IEnumerator Flicker()
    {
        for(int i = 0; i < FlickerCount; i++)
        {
            GetComponentInChildren<Renderer>().material.color = Color.red;
            yield return new WaitForSeconds(FlickerDuration);
            GetComponentInChildren<Renderer>().material.color = originalColor;
            yield return new WaitForSeconds(FlickerDuration);
            i++;
        }
        GetComponentInChildren<Renderer>().material.color = originalColor;
    }

    public void GetPlayerLayerMask()
    {
        float dist = 5.0f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.forward, dist, groundLayer);
        Debug.DrawRay(transform.position, Vector3.forward * dist, Color.red);
        if (hit.collider != null)
        {
            Debug.Log("Hit Object: " + hit.collider.gameObject.name);
            switch (hit.collider.gameObject.name)
            {
                case "MedLevel":
                    checkpointManager.UpdateCheckpointStatus(0, false);
                    checkpointManager.UpdateCheckpointStatus(1, true);
                    if (!IsLevel2 && !IsLevel3)
                    {
                        Debug.Log("Ship is too weak");
                        time -= Time.deltaTime;
                        Debug.Log(time);
                        if (time <= 1)
                        {
                            TakeDamage(1f);
                            time = 5f;
                        }
                    }
                    break;
                case "HardLevel":
                    Debug.Log("Hard Level");
                    checkpointManager.UpdateCheckpointStatus(1, false);
                    checkpointManager.UpdateCheckpointStatus(2, true);
                    if (!IsLevel3)
                    {
                        Debug.Log("Ship is too weak");
                        time -= Time.deltaTime;
                        Debug.Log(time);
                        if (time <= 1)
                        {
                            TakeDamage(1f);
                            time = 5f;
                        }
                    }
                    break;
            }
        }
    }
}
