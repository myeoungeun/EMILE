using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour, IAttackHandler
{
    public int damage;
    public float shotScale;
    public int baseDamage = 10;
    public float baseShotScale = 0f;
    public PlayerMovement playerMovement;

    public GameObject bullet;
    public Transform bulletStart; //총알이 발사되는 위치, 방향
    public BulletType currentBulletType; //현재 장착한 탄창 종류
    
    public void Shoot()
    {
        OnShot(currentBulletType);
        Debug.Log("총알 발사");
        GameObject bulletObj = Instantiate(bullet, bulletStart.position, bulletStart.rotation); //총알 생성
        bulletObj.GetComponent<Bullet>().Initialize(damage, shotScale, playerMovement.lookDirectionRight); //데미지, 총알 크기, 방향을 Bullet에게 전달
    }

    public void OnShot(BulletType currentBulletType) //총알(탄창) 정보
    {
        switch(currentBulletType)
        {
            case BulletType.Normal:
                damage = baseDamage;
                shotScale = baseShotScale;
                break;
            case BulletType.Strong:
                damage = baseDamage * 2;
                shotScale = baseShotScale + 5f;
                break;
            case BulletType.Wide:
                damage = baseDamage / 2;
                shotScale = baseShotScale + 15f;
                break;
            default:
                damage = baseDamage;
                shotScale = baseShotScale;
                break;
        }
    }
}
