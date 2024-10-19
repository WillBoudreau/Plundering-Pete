using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerpentBehaviour : Enemy
{
    [Header("Serpent")]
    [Header("Serpent Values")]
    //Serpent Values
    public float stoppingDistance;
    public float detectionDistance;
    public float maxHealth;
    float bottomY = -15f;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        originalColor = renderer.material.color;
        speed = 2;
        health = 2;
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
        if (transform.position.y <= bottomY)
        {
            Debug.Log("Dead");
            Destroy(gameObject);
        }
        else if (transform.position.y > bottomY)
        {
            Vector3 targetPosition = new Vector3(transform.position.x, bottomY, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, player.transform.position) < detectionDistance)
        {

            Vector3 targetPosition = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            targetPosition.z = -2;
            transform.position = targetPosition;
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
    public void Death()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
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
            Destroy(collision.gameObject);
            TakeDamage(player.damage);
        }
    }
}
