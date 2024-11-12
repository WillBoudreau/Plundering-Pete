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
        SetCostomCursor();
        //Debug.Log($"Is firing: {playerMovement.IsFiring}, CooldownTime: {playerMovement.cooldownTimer}, FireRate: {playerStats.fireRate}");
        if(playerMovement.cooldownTimer >= 0f)
        {
            float FillAmount = playerMovement.cooldownTimer/playerStats.fireRate;
           //Debug.Log(FillAmount);
            cursorCooldown.fillAmount = FillAmount;
            //Debug.Log(cursorCooldown.fillAmount);
            //Debug.Log(FillAmount);
        }
        else
        {
            cursorCooldown.fillAmount = 1f;
        }
    }
    void SetCostomCursor()
    {
        Vector2 cursorPOS = Input.mousePosition;
        cursorCooldown.rectTransform.position = cursorPOS;
    }
}
