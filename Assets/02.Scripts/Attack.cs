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
    public Transform bulletStart; //�Ѿ��� �߻�Ǵ� ��ġ, ����
    public BulletType currentBulletType; //���� ������ źâ ����
    
    public void Shoot()
    {
        OnShot(currentBulletType);
        Debug.Log("�Ѿ� �߻�");
        GameObject bulletObj = Instantiate(bullet, bulletStart.position, bulletStart.rotation); //�Ѿ� ����
        bulletObj.GetComponent<Bullet>().Initialize(damage, shotScale, playerMovement.lookDirectionRight); //������, �Ѿ� ũ��, ������ Bullet���� ����
    }

    public void OnShot(BulletType currentBulletType) //�Ѿ�(źâ) ����
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
