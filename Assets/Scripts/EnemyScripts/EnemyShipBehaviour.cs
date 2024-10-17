using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipBehaviour : Enemy
{
    [Header("EnemyShip")]
    [Header("EnemyShip Values")]
    //EnemyShip Values
    public float stoppingDistance;
    public float maxHealth;
    public float CanonVelocity;
    public float fireRate;
    public GameObject CanonBall;
    public Transform CanonFirePoint;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        originalColor = renderer.material.color;
        speed = 2;
        health = 2;
        maxHealth = 2;
        damage = 1;
        stoppingDistance = 2;
        CanonVelocity = 25f;
        fireRate = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        fireRate -= Time.deltaTime;
        player = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
        Move();
        HandleShooting();
    }
    public override void Move()
    {
        
    }
    void HandleShooting()
    {
        if(fireRate <= 0)
        {
            float bulletSpawnDist = 1.0f;
            Vector3 CanonBallSpawnPos = CanonFirePoint.position + (CanonFirePoint.forward * bulletSpawnDist);
            CanonBallSpawnPos.z = -2;
        
            //musicManager.PlaySound(0);
            GameObject CannonBall = Instantiate(CanonBall,CanonBallSpawnPos, Quaternion.identity);
            //Vector2 shootdirection = (mouseWorldPOS - (Vector2)transform.position).normalized;
            CannonBall.GetComponent<Rigidbody2D>().velocity = Vector2.up * CanonVelocity;
            Destroy(CannonBall, 5.0f);
            fireRate = 5f;     
        }
    }
    public override void TakeDamage(float damage)
    {
        StartCoroutine(Flicker());
        health -= damage;
        if(health <= 0)
        {
            Death();
        }
    }
    public override IEnumerator Flicker()
    {
        for(int i = 0; i < FlickerCount; i++)
        {
            renderer.material.color = Color.red;
            yield return new WaitForSeconds(FlickerDuration);
            renderer.material.color = originalColor;
            yield return new WaitForSeconds(FlickerDuration);
        }
        renderer.material.color = originalColor;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerBehaviour>().TakeDamage(damage);
        }
        if(collision.gameObject.tag == "Bullet")
        {
            TakeDamage(player.damage);
        }
    }
    public void Death()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
