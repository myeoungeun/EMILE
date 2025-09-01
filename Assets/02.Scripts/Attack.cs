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
    private float shotSpeed; //�Ѿ� �ӵ�
    private int shotInterval; //�߻� ����
    private int shotCount; //��� ������ �Ѿ� ����
    private bool isShooting = false;
    
    public PlayerMovement playerMovement;
    public GameObject bullet;
    public Transform bulletStart; //�Ѿ��� �߻�Ǵ� ��ġ
    [SerializeField] private BulletType currentBulletType; //���� ������ źâ ����

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) //��ư ������ �ִ� ���ȿ�
        {
            StartShooting(); //�Ѿ� �߻�
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            StopShooting();
        }
    }

    public void Shoot()
    {
        OnShot(currentBulletType);
        Debug.Log("�Ѿ� �߻�");
        GameObject bulletObj = Instantiate(bullet, bulletStart.position, bulletStart.rotation); //�Ѿ� ����
        bulletObj.GetComponent<Bullet>().Initialize(damage, shotSpeed, shotInterval, shotCount, playerMovement.lookDirectionRight); //������, �Ѿ� ũ��, ������ Bullet���� ����
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
            Shoot(); // �Ѿ� �߻�
            yield return new WaitForSeconds(shotInterval * 0.05f); //shotInterval�� 1�� ������ ��ȯ
        }
    }

    public void OnShot(BulletType bulletType) //�Ѿ�(źâ) ����
    {
        switch(bulletType)
        {
            case BulletType.Normal:
                damage = baseDamage;
                shotSpeed = baseShotSpeed;
                shotInterval = baseShotInterval * 2;
                break;
            case BulletType.Pierce:
                //�Ѿ� �̹��� ����
                damage = baseDamage * 2;
                shotSpeed = baseShotSpeed + 10f;
                shotInterval = baseShotInterval;
                break;
            case BulletType.Hollow:
                //�Ѿ� �̹��� ����
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
