using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<GameObject> coins;
    public int coinCount;
    public int maxCoins;
    public bool IsMax = false;
    // Start is called before the first frame update
    void Start()
    {
        coins = new List<GameObject>();
        maxCoins = 10;
    }

    // Update is called once per frame
    void Update()
    {
        //coins.Add(GameObject.Find("Coin"));
        MaxCoins();
    }
    void MaxCoins()
    {
        if(coinCount >= maxCoins)
        {
            IsMax = true;
            coinCount = maxCoins;
        }
    }
    
}
