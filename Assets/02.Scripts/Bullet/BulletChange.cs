using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BulletChange : MonoBehaviour
{
    [SerializeField] private int[] bulletIDs = { 501, 502, 503 }; //������� �ٲ� źâ ID
    private int currentBulletIndex = 0;
    
    public void OnChangeBullet(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed) //vŰ ������ ��
        {
            currentBulletIndex++; //�ε��� ����(źâ ����)
            if (currentBulletIndex >= bulletIDs.Length)
            {
                currentBulletIndex = 0;
            }

            // ���� źâ ��ü
            Attack.SetBulletByID(bulletIDs[currentBulletIndex]);
            Debug.Log($"źâ ��ü! ���� ID: {bulletIDs[currentBulletIndex]}");
        }
    }
}
