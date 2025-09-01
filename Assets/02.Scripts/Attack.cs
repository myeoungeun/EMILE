using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum BulletType
{
    Normal,
    Strong,
    Wide
}

public class Attack : MonoBehaviour, IAttackHandler
{
    public int baseDamage = 10;
    public float baseShotAngle = 0f;
    public bool lookDirectionRight; //이거 플레이어쪽에서 판별해야 됨.

    public GameObject bullet;
    public Transform bulletStart; //총알이 발사되는 위치, 방향
    public BulletType currentBulletType; //탄창 종류
    
    public void Shoot()
    {
        Debug.Log("총알 발사");
        GameObject bulletObj = Instantiate(bullet, bulletStart.position, bulletStart.rotation); //총알 생성
        bulletObj.GetComponent<Bullet>().Initialize(this, lookDirectionRight); //공격자 정보(데미지, 발사 각도 등)를 bullet에게 전달
    }

    public void OnShot(out int damage, out float shotAngle) //총알(탄창) 정보
    {
        switch(currentBulletType)
        {
            case BulletType.Normal:
                damage = baseDamage;
                shotAngle = baseShotAngle;
                break;
            case BulletType.Strong:
                damage = baseDamage * 2;
                shotAngle = baseShotAngle + 5f;
                break;
            case BulletType.Wide:
                damage = baseDamage / 2;
                shotAngle = baseShotAngle + 15f;
                break;
            default:
                damage = baseDamage;
                shotAngle = baseShotAngle;
                break;
        }
    }
}
