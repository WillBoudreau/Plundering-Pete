using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private MusicChanger musicManager;
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
    public float cooldownTimer = 0f;
    public void HandlePlayerMovement()
    {
        HandleMovement();
        HandleShooting();
        StaywithinBounds();
        cooldownTimer -= Time.deltaTime;
    }
    //Handle the players shooting
     void HandleShooting()
    {
        //Handle player shooting by tracking the mouse pos
        playerStats.fireRate -= Time.deltaTime;
    
        if (Input.GetKeyDown(KeyCode.Mouse0) && playerStats.fireRate <= 0)
        {
            IsFiring = true;
            cooldownTimer = playerStats.fireRate;
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
            else if(playerStats.IsLevel3)
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
    void StaywithinBounds()
    {
        // Ensure the player remains within the camera bounds
        Vector3 pos = transform.position;
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(pos);
        viewportPos.x = Mathf.Clamp(viewportPos.x, 0.05f, 0.95f);
        viewportPos.y = Mathf.Clamp(viewportPos.y, 0.05f, 0.95f);
        transform.position = mainCamera.ViewportToWorldPoint(viewportPos);
    }
}
