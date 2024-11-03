using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory Values")]
    public List<GameObject> coins;
    public int coinCount;
    public int maxCoins;
    public bool IsMax = false;
    public TextMeshProUGUI coinText;
    private Color originalColor;
    private bool isFlashing = false;

    // Start is called before the first frame update
    void Start()
    {
        coins = new List<GameObject>();
        originalColor = coinText.color;
        maxCoins = 25;
    }

    // Update is called once per frame
    void Update()
    {
        MaxCoins();
    }

    void MaxCoins()
    {
        if (coinCount >= maxCoins)
        {
            coinCount = maxCoins;
            IsMax = true;
            Debug.Log("Max Coins Reached"); 
            if (!isFlashing)
            {
                StartCoroutine(FlashRed());
            }
        }
        else
        {
            IsMax = false;
            coinText.color = originalColor;
            isFlashing = false;
        }
    }

    IEnumerator FlashRed()
    {
        isFlashing = true;
        int flashCount = 0;
        int maxFlashes = 10;

        while (IsMax && flashCount < maxFlashes)
        {
            coinText.color = Color.red;
            yield return new WaitForSeconds(0.5f);
            coinText.color = originalColor;
            yield return new WaitForSeconds(0.5f);
            flashCount++;
        }

        IsMax = false;
        isFlashing = false;
        coinText.color = originalColor;
    }
}
