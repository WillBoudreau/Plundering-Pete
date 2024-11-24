using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class SharkBahaviour : Enemy
{
    [Header("Shark")]
    [Header("Shark Values")]
    //Shark Values
    public float stoppingDistance;
    public float detectionDistance;
    public float maxHealth;
    float bottomY = -140f;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponentInChildren<Renderer>();
        Gold = GameObject.FindGameObjectWithTag("Gold");
        SetStats();
        AdhustHealthBar();
    }
    void SetStats()
    {
        originalColor = renderer.material.color;
        speed = 5;
        health = 2;
        maxHealth = 2;
        damage = 1;
        stoppingDistance = 2;
        detectionDistance = 10;
        FlickerCount = 3;
        FlickerDuration = 0.1f;
        AttackTimer = 1;
        StartingAttackTimer = 1;
        AttackDistance = 5;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Player");
        Move();
        Timer();
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        Gold = GameObject.FindGameObjectWithTag("Gold");
    }

    public override void Move()
    {
        if(transform.position.y <= bottomY)
        {
            Debug.Log("Dead");
            Destroy(gameObject);
        }
        else if (Vector2.Distance(transform.position, player.transform.position) < detectionDistance && PlayerInFront())
        {
            Vector3 targetPosition = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            targetPosition.z = StartPOSZ;
            transform.position = targetPosition;
            Attack();
        }
        else if (transform.position.y > bottomY)
        {
            Vector3 targetPosition = new Vector3(transform.position.x, bottomY, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }
    bool PlayerInFront()
    {
        Vector2 toPlayer =(player.transform.position - transform.position).normalized;
        float dot = Vector2.Dot(toPlayer, transform.up);
        if(dot > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public override void Attack()
    {
        if(AttackTimer <= 0)
        {
            if(Vector2.Distance(transform.position, player.transform.position) < AttackDistance)
            {
                playerStats.TakeDamage(damage);
                AttackTimer = StartingAttackTimer;
            }
        }
    }

    void Timer()
    {
        AttackTimer -= Time.deltaTime;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            TakeDamage(playerStats.damage);
            Destroy(collision.gameObject);
        }
    }

    public override void TakeDamage(float damage)
    {
        StartCoroutine(Flicker());
        health -= damage;
        AdhustHealthBar();
        if(health <= 0)
        {
            Death();
        }
    }
    void AdhustHealthBar()
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = health;
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

    void Death()
    {
        Instantiate(Gold, transform.position, Quaternion.identity);
        Destroy(gameObject);
        playerStats.SharkKills += 1;
    }
}