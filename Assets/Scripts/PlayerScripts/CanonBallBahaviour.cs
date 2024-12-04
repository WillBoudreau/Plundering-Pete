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
    [SerializeField] private EnemyShipBehaviour enemyShip;
    void Start()
    {
        if(enemyShip != null)
        {
            Collider2D collider = GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(collider, enemyShip.GetComponent<Collider2D>());
        }
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
            // if(collision.gameObject.tag == "CanonBall")
            // {
            //     Destroy(this.gameObject);
            //     Destroy(collision.gameObject);
            // }
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
