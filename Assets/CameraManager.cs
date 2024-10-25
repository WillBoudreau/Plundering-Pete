using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float upwardSpeed = 2f;
    [Header("Class Calls")]
    public PlayerBehaviour playerBehaviour;
    public GameObject player;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        playerBehaviour = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        MoveWithPlayer();
    }

    void MoveWithPlayer()
    {
        Vector3 targetPOS = player.transform.position + offset;
        targetPOS.y += upwardSpeed * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, targetPOS, moveSpeed * Time.deltaTime);
    }
}
