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
        renderer = GetComponent<Renderer>();
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
        detectionDistance = 6;
        FlickerCount = 3;
        FlickerDuration = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Player");
        Move();
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
        else if (Vector2.Distance(transform.position, player.transform.position) < detectionDistance)
        {
            Vector3 targetPosition = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            targetPosition.z = StartPOSZ;
            transform.position = targetPosition;
        }
        else if (transform.position.y > bottomY)
        {
            Vector3 targetPosition = new Vector3(transform.position.x, bottomY, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
        }
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
        Debug.Log("Shark Health: " + health);
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