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
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Player Hit");
        }
    }
    void ChooseDamage()
    {
        switch(obstacleType)
        {
            case ObstacleType.Rock:
                damage = 10f;
                break;
            case ObstacleType.Iceberg:
                damage = 20f;
                break;
            case ObstacleType.Debris:
                damage = 30f;
                break;
        }
    }
}
