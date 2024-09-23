using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    public float speed;
    public float health;
    public float fireRate;
    public float damage;
    public Camera mainCamera;

    public Rigidbody2D rb;
    public GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    { 
        fireRate = 0f;
        rb = GetComponent<Rigidbody2D>();
        speed = 10.0f;
        health = 100.0f;
        damage = 10.0f;

    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleShooting();
        Death();
    }
    void HandleShooting()
    {
        fireRate -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse0) && fireRate <= 0)
        {
            Vector2 mouseWorldPOS = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            GameObject bullet = Instantiate(bulletPrefab, transform.position,Quaternion.identity);
            Vector2 shootdirection = (mouseWorldPOS - (Vector2)transform.position).normalized;
            bullet.GetComponent<Rigidbody>().velocity = shootdirection * 10.0f;
            Destroy(bullet, 2.0f);
            fireRate = 10f;
        }
    }
    void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical) * speed;
        rb.velocity = movement;
    }
    public void Death()
    {
        if(health <= 0)
        {
            Destroy(this.gameObject);   
        }
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}