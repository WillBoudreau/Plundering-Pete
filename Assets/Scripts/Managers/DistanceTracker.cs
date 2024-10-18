using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DistanceTracker : MonoBehaviour
{
    [Header("Class calls")]
    public PlayerBehaviour playerBehaviour;
    public WaveManger waveManger;
    [Header("Variables")]
    public float Distance;
    public float GameTimer;
    public Transform startPosition;
    public Transform endPosition;
    [Header("UI elements")]
    public Slider distanceTracker;
    // Update is called once per frame
    void Update()
    {
        startPosition = GameObject.Find("StartPos").transform;
        endPosition = GameObject.Find("EndPOS").transform;
        SetValues();
        TrackDist();
    }

    void SetValues()
    {
        // Set all starting values
        Distance = Vector3.Distance(startPosition.position, endPosition.position);
        distanceTracker.maxValue = Distance;
        Debug.Log("Distance: " + Distance); 
        Debug.Log("Distval: " + distanceTracker.maxValue);
    }

    void TrackDist()
    {
        float playerDistance = Vector3.Distance(startPosition.position, playerBehaviour.transform.position);
        distanceTracker.value = playerDistance;
    }
}