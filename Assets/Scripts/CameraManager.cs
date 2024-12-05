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
    public void ShakeCamera()
    {
        StartCoroutine(Shake());
    }
    IEnumerator Shake()
    {
        float shakeDuration = 0.1f;
        float shakeAmount = 0.1f;
        float decreaseFactor = 1.0f;
        Vector3 originalPos = transform.position;

        while (shakeDuration > 0)
        {
            transform.position = originalPos + Random.insideUnitSphere * shakeAmount;
            shakeDuration -= Time.deltaTime * decreaseFactor;
            yield return null;
        }
        transform.position = originalPos;
    }
}
