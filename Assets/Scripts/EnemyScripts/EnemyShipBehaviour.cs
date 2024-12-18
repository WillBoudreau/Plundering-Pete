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

    private Vector3 startPosition;
    private bool movingRight = true;
    public float moveDistance = 5f;
    public float moveSpeed = 2f;
    bool canAttack = true;
    float minX = -20f;
    float maxX = 20f;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        deathAnim = GetComponent<Animator>();
        originalColor = renderer.material.color;
        speed = 2;
        health = 20;
        maxHealth = 20;
        damage = 1;
        stoppingDistance = 2;
        CanonVelocity = 25f;
        fireRate = 5f;
        startPosition = transform.position;
        AdhustHealthBar();
        GoldBag = GameObject.FindGameObjectWithTag("CoinBag");
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        Move();
        HandleShooting();
    }

    public override void Move()
    {
        if (movingRight)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            if(transform.position.x >= Mathf.Min(startPosition.x + moveDistance,maxX))
            {
                movingRight = false;
            }
        }
        else
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            if (transform.position.x <= Mathf.Max(startPosition.x - moveDistance,minX))
            {
                movingRight = true;
            }
        }
    }

    void HandleShooting()
    {
        if(canAttack)
        {
            if (fireRate <= 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    float bulletSpawnDist = 1.0f;
                    Vector3 CanonBallSpawnPos = CanonFirePoint.position + (CanonFirePoint.forward * bulletSpawnDist);
                    CanonBallSpawnPos.z = -2;
                    GameObject CannonBall = Instantiate(CanonBall, CanonBallSpawnPos, Quaternion.identity);
                    CannonBall.GetComponent<Rigidbody2D>().velocity = Vector2.up * CanonVelocity;
                    Destroy(CannonBall, 5.0f);
                    fireRate = 5f;
                }
            }
        } 
    }
    public override void Attack()
    {
        fireRate -= Time.deltaTime;
    }

    public override void TakeDamage(float damage)
    {
        StartCoroutine(Flicker());
        health -= damage;
        AdhustHealthBar();
        if (health <= 0)
        {
            Death();
        }
    }

    void AdhustHealthBar()
    {
        healthBar.value = health / maxHealth;
    }
    public override IEnumerator Flicker()
    {
        for (int i = 0; i < FlickerCount; i++)
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
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
        }
        if (collision.gameObject.tag == "Bullet")
        {
            TakeDamage(playerStats.damage);
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.tag == "Border")
        {
            if(movingRight)
            {
                Debug.Log("Moving Left");
                movingRight = false;
            }
            else if(!movingRight)
            {
                Debug.Log("Moving Right");
                movingRight = true;
            }
        }
    }
    void DeathEffect()
    {
        deathAnim.SetTrigger("Death");
        canAttack = false;
        damage = 0;
        speed = 0;
        this.gameObject.GetComponent<Collider2D>().enabled = false;
    }

    public void Death()
    {
        if (health <= 0)
        {
            DeathEffect();
            playerStats.ShipKills++;
            StartCoroutine(DeathDelay());
        }
    }
    IEnumerator DeathDelay()
    {
        if (deathAnim != null)
        {
            yield return new WaitForSeconds(deathAnim.GetCurrentAnimatorStateInfo(0).length);
        }
        Instantiate(GoldBag, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
