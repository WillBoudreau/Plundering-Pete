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
    float bottomY = -15f;
    public GameObject doubloonPrefab;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        Gold = GameObject.FindGameObjectWithTag("Gold");
        originalColor = renderer.material.color;
        speed = 2;
        health = 2;
        maxHealth = 2;
        damage = 1;
        stoppingDistance = 2;
        detectionDistance = 10;
        //bottomY = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0)).y;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
        Move();
        if(Gold == null)
        {
            Gold = GameObject.FindGameObjectWithTag("Gold");
        }
    }
    public override void Move()
    {
        if(transform.position.y <= bottomY)
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
    void Death()
    {
        Instantiate(Gold, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Debug.Log("Shark Kills" + player.SharkKills);
        player.SharkKills += 1;
    }
}