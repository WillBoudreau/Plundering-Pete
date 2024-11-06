using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private  PlayerStats playerStats;
    [Header("Player Movement")]
    [SerializeField] private PlayerMovementHandler playerMovementHandler;
    public TextMeshProUGUI DoubloonText;
    public float time = 5.0f;
    [Header("Class calls")]
    public InventoryManager inventoryManager;
    public LevelManager levelManager;
    public UIManager uIManager;
    public CheckpointManager checkpointManager;

    // Start is called before the first frame update
    void Start()
    { 
        playerMovementHandler.rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandlePlayer();
        GetPlayerLayerMask();
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        DoubloonText = GameObject.Find("DoubloonsText").GetComponent<TextMeshProUGUI>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }
    void HandlePlayer()
    {
        UpdateCounter();
        playerMovementHandler.HandlePlayerMovement();
        playerStats.HandlePlayer();
    }
    void UpdateCounter()
    {
        DoubloonText.text = "Doubloons: " + inventoryManager.coinCount;
    }
    //Handle the player colliding with objects
    void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.gameObject.tag)
        {
            case "WinTrig":
            playerStats.Win = true;
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
                playerStats.magnet = 0;
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
                playerStats.magnet = 0;
            }
            break;
            case "Obstacle":
            playerStats.TakeDamage(other.gameObject.GetComponent<Obstacle>().damage);
            break;
            case "Shark":
            playerStats.TakeDamage(other.gameObject.GetComponent<SharkBahaviour>().damage);
            break;
            case "Serpent":
            playerStats.TakeDamage(other.gameObject.GetComponent<SerpentBehaviour>().damage);
            break;
            case "EnemyShip":
            playerStats.TakeDamage(other.gameObject.GetComponent<EnemyShipBehaviour>().damage);
            break;
            case "CanonBall":
            var enemyShip = other.gameObject.GetComponent<EnemyShipBehaviour>();
            if (enemyShip != null)
            {
                playerStats.TakeDamage(enemyShip.damage);
                Destroy(other.gameObject);
            }
            break;
        }
    }
    public void GetPlayerLayerMask()
    {
        float dist = 5.0f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.forward, dist, playerStats.groundLayer);
        Debug.DrawRay(transform.position, Vector3.forward * dist, Color.red);
        if (hit.collider != null)
        {
            Debug.Log("Hit Object: " + hit.collider.gameObject.name);
            switch (hit.collider.gameObject.name)
            {
                case "MedLevel":
                    checkpointManager.UpdateCheckpointStatus(0, false);
                    checkpointManager.UpdateCheckpointStatus(1, true);
                    if (!playerStats.IsLevel2 && !playerStats.IsLevel3)
                    {
                        Debug.Log("Ship is too weak");
                        time -= Time.deltaTime;
                        Debug.Log(time);
                        if (time <= 1)
                        {
                            playerStats.TakeDamage(1f);
                            time = 5f;
                        }
                    }
                    break;
                case "HardLevel":
                    Debug.Log("Hard Level");
                    checkpointManager.UpdateCheckpointStatus(1, false);
                    checkpointManager.UpdateCheckpointStatus(2, true);
                    if (!playerStats.IsLevel3)
                    {
                        Debug.Log("Ship is too weak");
                        time -= Time.deltaTime;
                        Debug.Log(time);
                        if (time <= 1)
                        {
                            playerStats.TakeDamage(1f);
                            time = 5f;
                        }
                    }
                    break;
            }
        }
    }
}
