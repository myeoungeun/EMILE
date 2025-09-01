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
    public bool lookDirectionRight; //�̰� �÷��̾��ʿ��� �Ǻ��ؾ� ��.

    public GameObject bullet;
    public Transform bulletStart; //�Ѿ��� �߻�Ǵ� ��ġ, ����
    public BulletType currentBulletType; //źâ ����
    
    public void Shoot()
    {
        Debug.Log("�Ѿ� �߻�");
        GameObject bulletObj = Instantiate(bullet, bulletStart.position, bulletStart.rotation); //�Ѿ� ����
        bulletObj.GetComponent<Bullet>().Initialize(this, lookDirectionRight); //������ ����(������, �߻� ���� ��)�� bullet���� ����
    }

    public void OnShot(out int damage, out float shotAngle) //�Ѿ�(źâ) ����
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
