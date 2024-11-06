using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] float moveSpeed;
    [SerializeField] Vector3 startPosition;
    [Header("Class Calls")]
    [SerializeField] private PlayerMovementHandler playerMovementHandler;
    // Update is called once per frame
    void FixedUpdate()
    {
        MoveWithPlayer();
    }

    void MoveWithPlayer()
    {
        if(playerMovementHandler.IsMoving == true)
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }
    }
}
