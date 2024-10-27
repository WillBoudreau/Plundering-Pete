using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DistanceTracker : MonoBehaviour
{
    [Header("Class calls")]
    public PlayerBehaviour playerBehaviour;
    public CheckpointManager checkpointManager;
    [Header("Variables")]
    public float Distance;
    public Transform startPosition;
    public float playerDistance; 
    public Transform endPosition;
    public bool spawnlevel1;
    public bool spawnlevel2;
    public bool spawnlevel3;
    [Header("UI elements")]
    public Slider distanceTracker;
    public TextMeshProUGUI WarningText;
    void Start()
    {
        WarningText.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (startPosition == null || endPosition == null)
        {
            FindPositions();
        }
        checkpointManager.SetValues();
        SetValues();
        TrackDist();
        Checkpoint();
        DisplayWarning();
        playerDistance = Vector3.Distance(startPosition.position, playerBehaviour.transform.position);
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
            distanceTracker.value = playerDistance;
        }
    }
    void Checkpoint()
    {
            if (playerBehaviour.transform.position.y >=  checkpointManager.Checkpoint1 && !spawnlevel1)
            {
                spawnlevel1 = true;
                checkpointManager.UpdateCheckpointStatus(0, true);
            }
            if (playerBehaviour.transform.position.y >= checkpointManager.Checkpoint2)
            {
                checkpointManager.UpdateCheckpointStatus(1, true);
            }
            if (playerBehaviour.transform.position.y >= checkpointManager.Checkpoint3)
            {
                checkpointManager.UpdateCheckpointStatus(2, true);
            }
    }
    void DisplayWarning()
    {
        WarningText.gameObject.SetActive(false);
        if(playerBehaviour.IsLevel2 == false && playerBehaviour.transform.position.y >= checkpointManager.Checkpoint2)
        {
            // Display warning
            WarningText.gameObject.SetActive(true);
            WarningText.text = "YAARRR! Ye is about to enter dangerous waters! Your ship is not ready!";
        }
        else if(playerBehaviour.IsLevel3 == false && playerBehaviour.transform.position.y >= checkpointManager.Checkpoint3)
        {
            // Display warning
            WarningText.gameObject.SetActive(true);
            WarningText.text = "YAARRR! Ye is about to enter dangerous waters! Your ship is not ready!";
        }
    }
    public void SetFalse()
    {
        spawnlevel1 = false;
        spawnlevel2 = false;
        spawnlevel3 = false;
    }
}