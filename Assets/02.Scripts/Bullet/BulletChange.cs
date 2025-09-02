using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BulletChange : MonoBehaviour
{
    [SerializeField] private int[] bulletIDs = { 501, 502, 503 }; //순서대로 바뀔 탄창 ID
    private int currentBulletIndex = 0;
    
    public void OnBulletChange(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed) //v키 눌렀을 때
        {
            currentBulletIndex++;
            if (currentBulletIndex >= bulletIDs.Length) //숫자 커지면 0으로 초기화(반복)
            {
                currentBulletIndex = 0;
            }
            Attack.Instance.SetBulletByID(bulletIDs[currentBulletIndex]); //실제 탄창 교체
        }
    }
}
