using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour, IAttackHandler
{
    public int baseDamage = 5;
    public float baseShotSpeed = 30f;
    public int baseShotInterval = 5;
    
    private int damage;
    private float shotSpeed; //총알 속도
    private int shotInterval; //발사 간격
    private int shotCount; //사용 가능한 총알 개수
    private bool isShooting = false;
    
    public PlayerMovement playerMovement;
    public GameObject bullet;
    public Transform bulletStart; //총알이 발사되는 위치
    [SerializeField] private BulletType currentBulletType; //현재 장착한 탄창 종류

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) //버튼 누르고 있는 동안에
        {
            StartShooting(); //총알 발사
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            StopShooting();
        }
    }

    public void Shoot()
    {
        OnShot(currentBulletType);
        Debug.Log("총알 발사");
        GameObject bulletObj = Instantiate(bullet, bulletStart.position, bulletStart.rotation); //총알 생성
        bulletObj.GetComponent<Bullet>().Initialize(damage, shotSpeed, shotInterval, shotCount, playerMovement.lookDirectionRight); //데미지, 총알 크기, 방향을 Bullet에게 전달
    }
    
    public void StartShooting()
    {
        if (!isShooting)
        {
            StartCoroutine(ShootingRoutine());
            
        }
    }

    public void StopShooting()
    {
        isShooting = false;
    }

    private IEnumerator ShootingRoutine()
    {
        isShooting = true;

        while (isShooting)
        {
            Shoot(); // 총알 발사
            yield return new WaitForSeconds(shotInterval * 0.05f); //shotInterval을 1초 단위로 변환
        }
    }

    public void OnShot(BulletType bulletType) //총알(탄창) 정보
    {
        switch(bulletType)
        {
            case BulletType.Normal:
                damage = baseDamage;
                shotSpeed = baseShotSpeed;
                shotInterval = baseShotInterval * 2;
                break;
            case BulletType.Pierce:
                //총알 이미지 변경
                damage = baseDamage * 2;
                shotSpeed = baseShotSpeed + 10f;
                shotInterval = baseShotInterval;
                break;
            case BulletType.Hollow:
                //총알 이미지 변경
                damage = baseDamage * 2;
                shotSpeed = baseShotSpeed;
                shotInterval = (int)(baseShotInterval * 1.5);
                break;
            default:
                damage = baseDamage;
                shotSpeed = baseShotSpeed;
                break;
        }
    }
}
