using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBallBahaviour : MonoBehaviour
{
    public enum CanonBallType
    {
        Player,
        Enemy
    }
    public CanonBallType canonBallType;
    public EnemyShipBehaviour enemyShip;
    public GameObject[] sharks;
    public GameObject[] serpents;
    public GameObject[] enemyShips;

    void Awake()
    {
        sharks = GameObject.FindGameObjectsWithTag("Shark");
        serpents = GameObject.FindGameObjectsWithTag("Serpent");
        enemyShips = GameObject.FindGameObjectsWithTag("EnemyShip");
    }
    void Start()
    {
        if(enemyShip != null)
        {
            Collider2D collider = GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(collider, enemyShip.GetComponent<Collider2D>());
        }
    }
    void Update()
    {
        sharks = GameObject.FindGameObjectsWithTag("Shark");
        serpents = GameObject.FindGameObjectsWithTag("Serpent");
        enemyShips = GameObject.FindGameObjectsWithTag("EnemyShip");
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            Debug.Log("Hit obstacle");
            Destroy(this.gameObject);
        }
        if(canonBallType == CanonBallType.Enemy)
        {
            if(collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<PlayerStats>().TakeDamage(enemyShip.damage);
                Destroy(this.gameObject);
            }
            if(collision.gameObject.tag == "Obstacle")
            {
                Destroy(this.gameObject);
            } 
            // if(collision.gameObject.tag =="Shark" || collision.gameObject.tag == "Serpent" || collision.gameObject.tag == "EnemyShip")
            // {
            //     Destroy(this.gameObject);
            // }
            foreach (GameObject shark in sharks)
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), shark.GetComponent<Collider2D>());
            }
            foreach (GameObject serpent in serpents)
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), serpent.GetComponent<Collider2D>());
            }
            foreach (GameObject enemyShip in enemyShips)
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), enemyShip.GetComponent<Collider2D>());
            }
        }
        if(canonBallType == CanonBallType.Player)
        {
            if (collision.gameObject.tag == "CanonBall")
            {
                Destroy(this.gameObject);
                Destroy(collision.gameObject);
            }
        }
    }
}
