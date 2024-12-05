using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckpointManager : MonoBehaviour
{
    [Header("Class calls")]
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private PlayerBehaviour playerBehaviour;
    [Header("Variables")]
    public int checkpointbonus = 5;
    public TextMeshProUGUI checkpointText;
    [Header("Bools")]
    //Bools for checkpoints
    public bool FirstCheckpoint;
    public bool SecondCheckpoint;
    public bool ThirdCheckpoint;
    //Bool for checkpoint bonus
    public bool CheckpointBonus2;
    public bool CheckpointBonus3;
    [Header("Checkpoints")]
    //Checkpoints
    public float Checkpoint1;
    public float Checkpoint2;
    public float Checkpoint3;
    public float Checkpoint4;

    public void SetValues()
    {
        Checkpoint1 = -120;
        Checkpoint2 = 25;
        Checkpoint3 = 200;
        Checkpoint4 = 300;
    }
    void Update()
    {
        Checkpoint();
    }
    IEnumerator UpdateText()
    {
        checkpointText.text = "Checkpoint Bonus!: " + checkpointbonus;
        yield return new WaitForSeconds(5);
        checkpointText.text = "";
    }
    //Check the player's position and update the checkpoint status
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
        if(playerBehaviour.transform.position.y >= Checkpoint4)
        {
            
        }
    }
    //Update the checkpoint status
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
                    if(!CheckpointBonus2 && status)
                    {
                        StartCoroutine(UpdateText());
                        CheckpointBonus(checkpointbonus, status);
                        CheckpointBonus2 = true;
                    }
                    levelManager.UpdateObjects();
                break;
                case 2:
                    ThirdCheckpoint = status;
                    if(!CheckpointBonus3 && status)
                    {
                        StartCoroutine(UpdateText());
                        CheckpointBonus(checkpointbonus, status);
                        CheckpointBonus3 = true;
                    }
                    levelManager.UpdateObjects();
                break;
                default:
                    Debug.LogWarning("Invalid checkpoint index");
                break;
            }
        }
    }
    //Add the checkpoint bonus to the player's coin count
    void CheckpointBonus(int checkpointbonus, bool status)
    {
        if(status)
        {
            status = false;
            inventoryManager.coinCount += checkpointbonus;
        }
    }
    //Set the checkpoint booleans to false
    public void SetFalse()
    {
        FirstCheckpoint = false;
        SecondCheckpoint = false;
        ThirdCheckpoint = false;
    }
}
