using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : AttackBase
{
    public GameObject bulletPrefab;
    public Transform bulletStartTransform;
    public Transform target; // 플레이어 타겟

    private void Update()
    {
        if (CanShoot())
        {
            StartShooting();
        }
        else
        {
            StopShooting();
        }
    }

    protected override Vector3 GetShootDirection()
    {
        if (target != null)
            return (target.position - bulletStartTransform.position).normalized;
        return Vector3.left;
    }

    protected override Transform GetBulletStart()
    {
        return bulletStartTransform;
    }

    protected override GameObject GetBulletPrefab()
    {
        return bulletPrefab;
    }

    private bool CanShoot()
    {
        //ex : 플레이어가 일정 거리 안에 있으면 발사
        return Vector3.Distance(bulletStartTransform.position, target.position) < 30f;
    }
}
