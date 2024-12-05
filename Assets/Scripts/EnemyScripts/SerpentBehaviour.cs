using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SerpentBehaviour : Enemy
{
    [Header("Serpent")]
    [Header("Serpent Values")]
    //Serpent Values
    public float stoppingDistance;
    public float detectionDistance;
    public float maxHealth;
    float bottomY = -15f;
    bool canAttack = true;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponentInChildren<Renderer>();
        deathAnim = GetComponentInChildren<Animator>();
        originalColor = renderer.material.color;
        speed = 8;
        health = 10;
        damage = 1;
        stoppingDistance = 2;
        detectionDistance = 20;
        AttackTimer = 1;
        StartingAttackTimer = 1;
        AdhustHealthBar();
        GoldBag = GameObject.FindGameObjectWithTag("CoinBag");
        AttackDistance = 2f;
        player = GameObject.Find("Player");
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }


    // Update is called once per frame
    void Update()
    {
        AttackCooldownTimer();
        Move();
    }
    public override void Move()
    {
        if (transform.position.y <= bottomY)
        {
            Debug.Log("Dead");
            Destroy(gameObject);
        }
        else if (transform.position.y > bottomY)
        {
            Vector3 targetPosition = new Vector3(transform.position.x, bottomY, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, player.transform.position) < detectionDistance)
            {
                targetPosition = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                targetPosition.z = StartPOSZ;
                transform.position = targetPosition;
                Attack();
            }
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
    public override void Attack()
    {
        if(canAttack)
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
    }
    void AttackCooldownTimer()
    {
        AttackTimer -= Time.deltaTime;
    }
    void AdhustHealthBar()
    {
        healthBar.value = health / maxHealth;
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
        DeathEffect();
        playerStats.SerpentKills++;
        StartCoroutine(DeathDelay());
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Debug.Log("Bullet Hit");
            TakeDamage(playerStats.damage);
            Destroy(collision.gameObject);
        }
    }
    IEnumerator DeathDelay()
    {
        if(deathAnim != null)
        {
            yield return new WaitForSeconds(deathAnim.GetCurrentAnimatorStateInfo(0).length);
        }
        Instantiate(GoldBag, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
