using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DistanceTracker : MonoBehaviour
{
    [Header("Class calls")]
    public PlayerBehaviour playerBehaviour;
    public PlayerMovementHandler playerMovementHandler;
    public PlayerStats playerStats;
    public CheckpointManager checkpointManager;
    public LevelManager levelManager;
    [Header("Variables")]
    public float Distance;
    public Transform startPosition;
    public float playerDistance; 
    public Transform endPosition;
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
        //Checkpoint();
        if (startPosition == null || endPosition == null)
        {
            FindPositions();
        }
        checkpointManager.SetValues();
        SetValues();
        TrackDist();
        DisplayWarning();
    }

    void FindPositions()
    {
        if(levelManager.levelName == "GameTestScene")
        {
            startPosition = GameObject.Find("StartPos").transform;
            endPosition = GameObject.Find("EndPOS").transform;
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
        if(playerMovementHandler.IsMoving)
        {
            playerDistance = Vector3.Distance(startPosition.position, playerBehaviour.transform.position);
            if (startPosition != null && playerBehaviour != null)
            {
                distanceTracker.value = playerDistance;
            }
        }
    }

    void DisplayWarning()
    {
        WarningText.gameObject.SetActive(false);
        float PlayerDist = playerBehaviour.transform.position.y;
        bool IsLevel2 = playerStats.IsLevel2;
        bool IsLevel3 = playerStats.IsLevel3;
        if(!IsLevel2 && !IsLevel3 && PlayerDist >= checkpointManager.Checkpoint2)
        {
            // Display warning
            WarningText.gameObject.SetActive(true);
            WarningText.text = "YAARRR! Ye is about to enter dangerous waters! Your ship is not ready!";
        }
        else if(IsLevel3 == false && PlayerDist >= checkpointManager.Checkpoint3)
        {
            // Display warning
            WarningText.gameObject.SetActive(true);
            WarningText.text = "YAARRR! Ye is about to enter dangerous waters! Your ship is not ready!";
        }
    }
}