using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Class Calls")]
    public HealthManager healthManager;
    [SerializeField] private UIManager uIManager;
    [SerializeField] private MusicChanger musicManager;
    [SerializeField] private CheckpointManager checkpointManager;
    [SerializeField] private SpawnManager spawnManager;
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
    public bool Win;
    public LayerMask groundLayer;
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
    // void Update()
    // {
    //     HandlePlayer();
    // }
    public void HandlePlayer()
    {
        HandleHealthBar();
        HandleMagnit();
    }
    void HandleHealthBar()
    {
        healthManager.health = playerHealth;
        healthManager.playerhealth.maxValue = startHealth;
        healthManager.playerhealth.value = playerHealth;
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
        if(Level < PlayerLevels.Count)
        {
            Level++;
            for(int i = 0; i < PlayerLevels.Count; i++)
            {
                PlayerLevels[i].SetActive( i == Level - 1);
            }
            IsLevel2 = (Level == 2);
            IsLevel3 = (Level == 3);
        }
        else
        {
            Level = PlayerLevels.Count;
            Debug.Log("Max Level Reached");
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

        HandleHealthBar();
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
            playerHealth = 0;
            uIManager.SetGameState("GameOver");
            ResetHealth();
            Respawn();
            checkpointManager.SetFalse();
        }
    }

    void Respawn()
    {
        spawnManager.PlacePlayer();
        healthManager.IsDead = false;
    }

    private void ResetHealth()
    {
        playerHealth = startHealth;
        healthManager.health = playerHealth;
    }
}
