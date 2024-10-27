using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [Header("Class calls")]
    public LevelManager levelManager;
    [Header("Bools")]
    //Bools for checkpoints
    public bool FirstCheckpoint;
    public bool SecondCheckpoint;
    public bool ThirdCheckpoint;
    [Header("Checkpoints")]
    //Checkpoints
    public float Checkpoint1;
    public float Checkpoint2;
    public float Checkpoint3;
    public void SetValues()
    {
        Checkpoint1 = -150;
        Checkpoint2 = 25;
        Checkpoint3 = 200;
    }
    public void UpdateCheckpointStatus(int checkpointIndex, bool status)
    {
        switch (checkpointIndex)
        {
            case 0:
                FirstCheckpoint = status;
                levelManager.UpdateObjects();
                Debug.Log("First Checkpoint: " + FirstCheckpoint);
                break;
            case 1:
                SecondCheckpoint = status;
                levelManager.UpdateObjects();
                Debug.Log("Second Checkpoint: " + SecondCheckpoint);
                break;
            case 2:
                ThirdCheckpoint = status;
                levelManager.UpdateObjects();
                Debug.Log("Third Checkpoint: " + ThirdCheckpoint);
                break;
            default:
                Debug.LogWarning("Invalid checkpoint index");
                break;
        }
    }
    public void SetFalse()
    {
        FirstCheckpoint = false;
        SecondCheckpoint = false;
        ThirdCheckpoint = false;
    }
}
