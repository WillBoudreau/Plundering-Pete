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
        if (startPosition == null || endPosition == null)
        {
            FindPositions();
        }
        SetValues();
        TrackDist();
    }

    void FindPositions()
    {
        GameObject startObj = GameObject.Find("StartPos");
        GameObject endObj = GameObject.Find("EndPOS");

        if (startObj != null)
        {
            startPosition = startObj.transform;
        }

        if (endObj != null)
        {
            endPosition = endObj.transform;
        }
    }

    void SetValues()
    {
        // Set all starting values
        if (startPosition != null && endPosition != null)
        {
            Distance = Vector3.Distance(startPosition.position, endPosition.position);
            distanceTracker.maxValue = Distance;
        }
    }

    void TrackDist()
    {
        if (startPosition != null && playerBehaviour != null)
        {
            float playerDistance = Vector3.Distance(startPosition.position, playerBehaviour.transform.position);
            distanceTracker.value = playerDistance;
        }
    }
}