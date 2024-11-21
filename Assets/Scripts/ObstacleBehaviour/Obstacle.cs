using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public enum ObstacleType
    {
        Rock,
        Iceberg,
        Debris
    }
    public ObstacleType obstacleType;
    public float damage = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChooseDamage();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
        }
    }
    void ChooseDamage()
    {
        switch(obstacleType)
        {
            case ObstacleType.Rock:
                damage = 1f;
                break;
            case ObstacleType.Iceberg:
                damage = 5f;
                break;
            case ObstacleType.Debris:
                damage = 10f;
                break;
        }
    }
}
