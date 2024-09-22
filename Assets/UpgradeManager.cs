using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public float damageUpgrade;
    public float damageMax;
    public float healthUpgrade;
    public float speedUpgrade;
    public PlayerBehaviour player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
        damageUpgrade = 1;
        damageMax = 6;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpgradeDamage()
    {

        if(player.damage < damageMax)
        {
            player.damage += damageUpgrade;
            Debug.Log("Damage upgraded to: " + damageUpgrade);
        }
        else
        {
            Debug.Log("Max damage reached");
        }
    }
}
