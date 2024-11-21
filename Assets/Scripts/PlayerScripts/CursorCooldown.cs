using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorCooldown : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private PlayerMovementHandler playerMovement;
    [SerializeField] private Image cursorCooldown;


    void Update()
    {
        //SetCostomCursor();
        //Debug.Log($"Is firing: {playerMovement.IsFiring}, CooldownTime: {playerMovement.cooldownTimer}, FireRate: {playerStats.fireRate}");
        cursorCooldown.fillAmount = 1f;
        if(playerMovement.IsFiring)
        {
            float cooldownTime = playerStats.fireRate;
            cursorCooldown.fillAmount = cooldownTime;
        }
        else 
        {
            cursorCooldown.fillAmount = 1;
        }
    }
    void SetCostomCursor()
    {
        Vector2 cursorPOS = Input.mousePosition;
        cursorCooldown.rectTransform.position = cursorPOS;
    }
}
