using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [Header("Class calls")]
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private PlayerBehaviour playerBehaviour;
    [Header("Variables")]
    public int checkpointbonus = 5;
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
        Checkpoint1 = -120;
        Checkpoint2 = 25;
        Checkpoint3 = 200;
    }
    void Update()
    {
        Checkpoint();
    }
    void Checkpoint()
    {
        if (playerBehaviour.transform.position.y >=  Checkpoint1)
        {
           UpdateCheckpointStatus(0, true);
        }
        if (playerBehaviour.transform.position.y >= Checkpoint2)
        {
            UpdateCheckpointStatus(1, true);
        }
        if (playerBehaviour.transform.position.y >= Checkpoint3)
        {
            UpdateCheckpointStatus(2, true);
        }
    }

    public void UpdateCheckpointStatus(int checkpointIndex, bool status)
    {
        if(levelManager.levelName == "GameTestScene")
        {
            switch (checkpointIndex)
            {
                case 0:
                    FirstCheckpoint = status;
                    levelManager.UpdateObjects();
                break;
                case 1:
                    SecondCheckpoint = status;
                    levelManager.UpdateObjects();
                break;
                case 2:
                    ThirdCheckpoint = status;
                    levelManager.UpdateObjects();
                break;
                default:
                    Debug.LogWarning("Invalid checkpoint index");
                break;
            }
        }
    }

    public void SetFalse()
    {
        FirstCheckpoint = false;
        SecondCheckpoint = false;
        ThirdCheckpoint = false;
    }
}
