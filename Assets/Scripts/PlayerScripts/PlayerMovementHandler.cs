using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private MusicChanger musicManager;
    [SerializeField] private UIManager uIManager;
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private CheckpointManager checkpointManager;
    [SerializeField] private float time = 5.0f;
    [Header("Player Movement")]
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float startFireRate = 0.5f;
    [SerializeField] private float bulletVelocity = 10.0f;
    public GameObject bulletPrefab;
    public GameObject level2BulletPrefab;
    public GameObject level3BulletPrefab;
    public Camera mainCamera;
    public bool IsMoving;
    public Rigidbody2D rb;
    public bool IsFiring;

    // Handle the player movement( Called in PlayerBehaviour)
    public void HandlePlayerMovement()
    {
        HandleMovement();
        HandleShooting();
        StaywithinBounds();
    }

    //Handle the players shooting
    void HandleShooting()
    {
        //Handle player shooting by tracking the mouse pos
        playerStats.fireRate -= Time.deltaTime;
    
        if (Input.GetKeyDown(KeyCode.Mouse0) && playerStats.fireRate <= 0)
        {
            IsFiring = true;
            playerStats.fireRate = 0;
            Vector2 mouseWorldPOS = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mouseWorldPOS - (Vector2)transform.position).normalized;
            
            float bulletSpawnDist = 1.0f;
            Vector3 bulletSpawnPos = playerStats.firePoint.position + (playerStats.firePoint.forward * bulletSpawnDist);
            bulletSpawnPos.z = -2;

            musicManager.PlaySound(0);
            if(playerStats.IsLevel2)
            {
                for(int i = 0; i < 2; i++)
                {
                    GameObject bullet1 = Instantiate(level2BulletPrefab, bulletSpawnPos, Quaternion.identity); 
                    bullet1.GetComponent<Rigidbody2D>().velocity = Vector2.up * playerStats.bulletVelocity;
                    Destroy(bullet1, 2.0f);
                }
            }
            else if(playerStats.IsLevel3 | playerStats.Level == 3)
            {
                for(int i = 0; i < 3; i++)
                {
                    GameObject bullet2 = Instantiate(level3BulletPrefab, bulletSpawnPos, Quaternion.identity);
                    bullet2.GetComponent<Rigidbody2D>().velocity = Vector2.up * playerStats.bulletVelocity;
                    Destroy(bullet2, 2.0f);
                }
            }
            else
            {
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPos, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = Vector2.up * playerStats.bulletVelocity;
                Destroy(bullet, 2.0f);
            }
            playerStats.fireRate = playerStats.startFireRate;
        }
    }

    //Handle the player movement
    public void HandleMovement()
    {
        //Handle input using the old input system. TODO: change to new input system
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical) * speed;
        rb.velocity = movement;
        if(Input.GetKeyDown(KeyCode.E))
        {
            playerStats.TakeDamage(1);
        }
        if(rb.velocity.magnitude > 0)
        {
            IsMoving = true;
        }
        else
        {
            IsMoving = false;
        }
    }

    //Ensure the player remains within the camera bounds
    void StaywithinBounds()
    {
        // Ensure the player remains within the camera bounds
        Vector3 pos = transform.position;
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(pos);
        viewportPos.x = Mathf.Clamp(viewportPos.x, 0.05f, 0.95f);
        viewportPos.y = Mathf.Clamp(viewportPos.y, 0.05f, 0.95f);
        transform.position = mainCamera.ViewportToWorldPoint(viewportPos);
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
    void OnTriggerStay2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Level2":
            checkpointManager.UpdateCheckpointStatus(0, false);
            checkpointManager.UpdateCheckpointStatus(1, true);
            if(playerStats.IsLevel2 == false && playerStats.IsLevel3 == false)
            {
                time -= Time.deltaTime;
                Debug.Log(time);
                if (time <= 1)
                {
                    playerStats.TakeDamage(1f);
                    time = 5f;
                }
            }
            break;
            case "Level3":
            checkpointManager.UpdateCheckpointStatus(1, false);
            checkpointManager.UpdateCheckpointStatus(2, true);
            if(playerStats.IsLevel3 == false)
            {
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
