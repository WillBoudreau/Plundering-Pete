using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SharkBahaviour : MonoBehaviour
{
    public float stoppingDistance;
    public float detectionDistance;
    public Transform player;
    public NavMeshAgent agent;

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
        player = GameObject.Find("Player").transform;
        Move();
    }
    public void Move()
    {
        float DistanceToPlayer = Vector2.Distance(transform.position, player.position);
        if(DistanceToPlayer < detectionDistance)
        {
            agent.SetDestination(player.position);
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        switch(other)
        {
            //case player:
            //player.TakeDamage(damage);
            //break;
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
