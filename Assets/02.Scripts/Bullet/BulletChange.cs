using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BulletChange : MonoBehaviour
{
    [SerializeField] private int[] bulletIDs = { 501, 502, 503 }; //¼ø¼­´ë·Î ¹Ù²ð ÅºÃ¢ ID
    private int currentBulletIndex = 0;
    
    public void OnChangeBullet(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed) //vÅ° ´­·¶À» ¶§
        {
            currentBulletIndex++; //ÀÎµ¦½º Áõ°¡(ÅºÃ¢ º¯°æ)
            if (currentBulletIndex >= bulletIDs.Length)
            {
                currentBulletIndex = 0;
            }

            // ½ÇÁ¦ ÅºÃ¢ ±³Ã¼
            Attack.SetBulletByID(bulletIDs[currentBulletIndex]);
            Debug.Log($"ÅºÃ¢ ±³Ã¼! ÇöÀç ID: {bulletIDs[currentBulletIndex]}");
        }
    }
}
