using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<GameObject> coins;
    public int coinCount;
    // Start is called before the first frame update
    void Start()
    {
        coins = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        //coins.Add(GameObject.Find("Coin"));
    }
    
}
