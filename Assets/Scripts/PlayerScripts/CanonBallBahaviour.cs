using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBallBahaviour : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "CanonBall")
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
