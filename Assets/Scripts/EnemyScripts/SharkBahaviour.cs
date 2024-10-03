using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SharkBahaviour : MonoBehaviour
{
    public float stoppingDistance;
    public float detectionDistance;
    public PlayerBehaviour player;
    public float speed;
    public float damage;
    public float health;
    public float maxHealth;

    // Start is called before the first frame update
    void Start()
    {
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
    public void Move()
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
    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
