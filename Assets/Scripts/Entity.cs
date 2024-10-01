using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    HealthManager healthmanager;
    // Start is called before the first frame update
    void Start()
    {
        healthmanager = GetComponent<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
