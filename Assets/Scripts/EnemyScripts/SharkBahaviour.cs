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
        detectionDistance = 10;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
        Move();
    }
    public override void Move()
    {
        if(Vector2.Distance(transform.position, player.transform.position) < detectionDistance)
        {
            float DistanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            if(DistanceToPlayer < detectionDistance)
            {
                Vector3 targetPosition = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                targetPosition.z = -2;
                transform.position = targetPosition;
            }   
        }
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
            Destroy(collision.gameObject);
        }
    }
    public override void TakeDamage(float damage)
    {
        StartCoroutine(Flicker());
        health -= damage;
        if(health <= 0)
        {
            player.SharkKills++;
            Destroy(gameObject);
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
}
