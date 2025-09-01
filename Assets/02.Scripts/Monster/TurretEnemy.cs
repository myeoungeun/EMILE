using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : Enemy, Monster.IAttackable
{
    [SerializeField] private Transform bulletPos;

    public void Attack()
    {
        // Todo: 오브젝트 풀에서 총알을 생성
        Debug.Log("공격!");
    }
}
