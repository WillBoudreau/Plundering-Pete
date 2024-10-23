using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Variables")]
    public float moveSpeed;
    [Header("Class Calls")]
    public PlayerBehaviour playerBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        playerBehaviour = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        moveSpeed = playerBehaviour.speed;
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }
}
