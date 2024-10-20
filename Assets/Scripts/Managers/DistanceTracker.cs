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
    public float Checkpoint1;
    public float Checkpoint2;
    public float Checkpoint3;

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
        Checkpoint();
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
        Checkpoint1 = 100;
        Checkpoint2 = 200;
        Checkpoint3 = 300;
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
    void Checkpoint()
    {
        if (playerBehaviour != null)
        {
            if (playerBehaviour.transform.position.y >= Checkpoint1)
            {
                waveManger.UpdateCheckpointStatus(0, true);
            }
            if (playerBehaviour.transform.position.y >= Checkpoint2)
            {
                waveManger.UpdateCheckpointStatus(1, true);
            }
            if (playerBehaviour.transform.position.y >= Checkpoint3)
            {
                waveManger.UpdateCheckpointStatus(2, true);
            }
        }
    }
}