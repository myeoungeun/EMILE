using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : Enemy, Monster.IAttackable
{
    [SerializeField] private Transform bulletPos;

    public GameObject bullet;

    public bool Attack()
    {
        // Todo: 오브젝트 풀에서 총알을 생성
        Vector2 dir = (target.position - transform.position).normalized;

        Instantiate(bullet, bulletPos.position, Quaternion.identity).GetComponent<TestBullet>().Init(5f, EnemyData.AttackPower, dir);
        Debug.Log("공격!");
        return true;
    }
}
