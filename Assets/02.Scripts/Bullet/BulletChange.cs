using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// public class BulletChange : MonoBehaviour
// {
//     [SerializeField] private AttackBase playerAttack;
//     [SerializeField] private int[] bulletIDs = { 501, 502, 503 }; //������� �ٲ� źâ ID
//     private int currentBulletIndex = 0;
//     
//     public void OnBulletChange(InputAction.CallbackContext context)
//     {
//         if (context.phase == InputActionPhase.Performed) //vŰ ������ ��
//         {
//             currentBulletIndex++;
//             if (currentBulletIndex >= bulletIDs.Length) //���� Ŀ���� 0���� �ʱ�ȭ(�ݺ�)
//             {
//                 currentBulletIndex = 0;
//             }
//             playerAttack.SetBulletByID(bulletIDs[currentBulletIndex]); //���� źâ ��ü
//         }
//     }
// }
