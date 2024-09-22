using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkBahaviour : MonoBehaviour
{
    public float stoppingDistance;
    public float detectionDistance;
    public Transform player;

    public float speed;
    public float damage;
    public float health;
    public float maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        speed = 2;
        health = 100;
        maxHealth = 100;
        damage = 10;
        stoppingDistance = 2;
        detectionDistance = 10;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Player").transform;
        Move();
    }
    public void Move()
    {
        if(Vector2.Distance(transform.position, player.position) < detectionDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerBehaviour>().TakeDamage(damage);
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
