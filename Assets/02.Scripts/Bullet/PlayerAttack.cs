using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : AttackBase
{
    [SerializeField] private int[] bulletIDs = { 501, 502, 503 };
    private int currentBulletIndex = 0;

    public void OnBulletChange(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            currentBulletIndex++;
            if (currentBulletIndex >= bulletIDs.Length) //인덱스 넘어가면 0으로 초기화
            {
                currentBulletIndex = 0;
            }
            BulletData newBullet = GetBulletDataID(bulletIDs[currentBulletIndex]);
            if (newBullet != null)
            {
                currentBullet = newBullet; //총알 종류 교체
                SetBulletByID(newBullet.Id); //남은 탄 수 챙겨오기
                Debug.Log($"총알 교체: ID {newBullet.Id}");
            }
            else
            {
                Debug.LogWarning($"총알 ID {bulletIDs[currentBulletIndex]}를 찾을 수 없습니다.");
            }
        }
    }
}
