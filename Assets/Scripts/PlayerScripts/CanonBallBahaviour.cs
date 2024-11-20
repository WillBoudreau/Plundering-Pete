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
        enemyShip = GameObject.Find("EnemyShip").GetComponent<EnemyShipBehaviour>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(canonBallType == CanonBallType.Enemy)
        {
            if(collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<PlayerStats>().TakeDamage(enemyShip.damage);
                Destroy(this.gameObject);
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
        if(collision.gameObject.tag == "obstacle")
        {
            Destroy(this.gameObject);
        }
    }
}
