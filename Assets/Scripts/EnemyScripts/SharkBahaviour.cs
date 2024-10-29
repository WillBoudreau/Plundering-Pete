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

    private PlayerBehaviour player;
    private Renderer renderer;
    private GameObject Gold;
    private Color originalColor;
    private float speed;
    private float health;
    private float damage;
    private int FlickerCount = 3;
    private float FlickerDuration = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        Gold = GameObject.FindGameObjectWithTag("CoinBag");
        originalColor = renderer.material.color;
        speed = 5;
        health = 2;
        maxHealth = 2;
        damage = 1;
        stoppingDistance = 2;
        detectionDistance = 6;
        player = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if(Gold == null)
        {
            Gold = GameObject.FindGameObjectWithTag("CoinBag");
        }
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
            targetPosition.z = -2;
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
        Debug.Log("Shark Health: " + health);
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
        Debug.Log("Shark Dead");
        Instantiate(Gold, transform.position, Quaternion.identity);
        Debug.Log("Gold Dropped");
        Destroy(gameObject);
        Debug.Log("Shark Kills" + player.SharkKills);
        player.SharkKills += 1;
    }
}