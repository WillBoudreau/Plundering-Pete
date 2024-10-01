using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public enum CollectableType
    {
        Doubloon,
    }
    public CollectableType collectableType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(collectableType == CollectableType.Doubloon)
            {
                FindObjectOfType<CollectorManager>().Doubloons.Add(gameObject);
                gameObject.SetActive(false);
            }
        }
    }

}
