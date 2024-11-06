using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    // Start is called before the first frame update
    public InventoryManager inventory;
    public PlayerBehaviour playerBehaviour;
    public PlayerStats playerStats;
    public PlayerMovementHandler playerMovementHandler;
    public int cost;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void CostCheck()
    {
        Debug.Log("Checking Cost");
    }
    public virtual void UpgradePlayer()
    {
        Debug.Log("Upgrading Player");
    }
    public virtual void Reset()
    {
        Debug.Log("Resetting Player");
    }
}
