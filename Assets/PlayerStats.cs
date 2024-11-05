using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Class Calls")]
    public HealthManager healthManager;
    [SerializeField] private UIManager uIManager;
    [SerializeField] private MusicChanger musicManager;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private CheckpointManager checkpointManager;
    [SerializeField] private DistanceTracker distanceTracker;
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
    public Transform firePoint;
    public bool PlayerPlaced;
    [Header("Player Levels")]
    public bool IsLevel2;
    public bool IsLevel3; 
    public int Level;
    public List<GameObject> PlayerLevels = new List<GameObject>();
    [Header("Renderer Calls")]
    public Renderer renderer;
    private Color originalColor;
    public float FlickerDuration = 0.1f; 
    public int FlickerCount = 5;
    // Start is called before the first frame update
    void Start()
    {
        SetValues();
        renderer = GetComponentInChildren<Renderer>();
        originalColor = GetComponentInChildren<Renderer>().material.color;
    }
    public void HandlePlayer()
    {
        HandleHealthBar();
        HandleMagnit();
    }
    void HandleHealthBar()
    {
        healthManager.health = playerHealth;
        healthManager.playerhealth.maxValue = playerHealth;
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
}
