using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    public float speed;
    public float health;
    public float fireRate;
    public float damage;

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
        KeyBinds();
        Death();
    }
    void KeyBinds()
    {
        fireRate -= Time.deltaTime;
        Debug.Log(fireRate);    
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed);
        if(Input.GetKeyDown(KeyCode.Mouse0) && fireRate <= 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.GetComponent<Rigidbody>().velocity = transform.up * 10.0f;
            Destroy(bullet, 2.0f);
        }
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
