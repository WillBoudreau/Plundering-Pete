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
    [SerializeField] private CameraManager cameraManager;
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
    const float MinFireRate = 0.1f;
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
    
    void Awake()
    {
        startHealth = 5;
        playerHealth = startHealth;
        magnet = 3f;
        StartSpeed = 6;
        speed = StartSpeed;
    }
    // Start is called before the first frame update
    void Start()
    {
        SetValues();
        renderer = GetComponentInChildren<Renderer>();
        originalColor = GetComponentInChildren<Renderer>().material.color;
    }

    //Hanlde the player (Called in PlayerBehaviour)
    public void HandlePlayer()
    {
        HandleMagnit();
    }

    //Set the starting values for the player
    void SetValues()
    {
        //Set starting values
        Level = 1;
        //startHealth = 5;
        startdamage = 1;
        //StartSpeed = 6;
        startFireRate = 2f;
        bulletVelocity = 25f;
        healthManager.health = playerHealth;
        //magnet = 3f;
        //Assign values to be the starting values
        //playerHealth = startHealth;
        healthManager.playerhealth.fillAmount = playerHealth;
        //speed = StartSpeed;
        damage = startdamage;
        fireRate = startFireRate;
        //Set Win Bool
        Win = false;
    }
    public void AdjustFireRate(float increment)
    {
        fireRate = Mathf.Clamp(fireRate + increment, MinFireRate, startFireRate);
        startFireRate += increment;
        // startFireRate = fireRate;
        if(fireRate < MinFireRate)
        {
            fireRate = MinFireRate;
        }
        Debug.Log("Fire RateFromStats: " + fireRate + " Start Fire RateFromStats: " + startFireRate);
    }

    //Level up the player(Called in ShipUpgrade)
    public void LevelUp()
    {
        if(Level < PlayerLevels.Count)
        {
            Level++;
            if(Level == 2)
            {
                IsLevel2 = true;
                PlayerLevels[0].SetActive(false);
                PlayerLevels[1].SetActive(true);
            }
            else if(Level == 3)
            {
                IsLevel3 = true;
                IsLevel2 = false;
                PlayerLevels[1].SetActive(false);
                PlayerLevels[2].SetActive(true);
            }
            for(int i = 0; i < PlayerLevels.Count; i++)
            {
                PlayerLevels[i].SetActive( i == Level - 1);
            }
        }
        else
        {
            Level = PlayerLevels.Count;
            Debug.Log("Max Level Reached");
        }
    }

    //Handle the magnet power of the ship
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

    //Take damage from the player(Called in PlayerBehaviour)
    public void TakeDamage(float damage)
    {
        StartCoroutine(Flicker());
        ChooseDamageSound();
        playerHealth -= damage;
        healthManager.HandlePlayerHealthBar(playerHealth, startHealth);
        Death();
        cameraManager.ShakeCamera();
    }
    void ChooseDamageSound()
    {
        System.Random random = new System.Random();
        int randomNumber = random.Next(0, musicManager.damageEffects.Length);
        musicManager.PlayDamageSound(randomNumber);
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

    //Death of the player
    public void Death()
    {
        if (playerHealth <= 0)
        {
            playerHealth = 0;
            musicManager.PlaySound(3);
            uIManager.SetGameState("GameOver");
            ResetHealth();
            Respawn();
            checkpointManager.SetFalse();
        }
    }

    //Respawn the player
    void Respawn()
    {
        spawnManager.PlacePlayer();
        healthManager.IsDead = false;
    }

    //Reset the health of the player
    private void ResetHealth()
    {
        playerHealth = startHealth;
        healthManager.health = playerHealth;
        healthManager.HandlePlayerHealthBar(playerHealth, startHealth);
    }
}
