using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy, Monster.IAttackable, Monster.IDamageable
{
    [SerializeField] Transform bulletPos;
    [SerializeField] Transform missilePos;

    private bool isAttacking;

    private bool canRushAttack;

    private void Start()
    {
        isAttacking = false;
        canRushAttack = false;
    }

    /// <summary>
    /// 1. 체력 > 90% 총알 발사 패턴만 사용
    /// 2. 체력 <= 90% 총알 발사와 미사일 발사를 같이 사용
    /// 3. 체력이 60%, 30% 이하로 떨어질때 마다 돌진 공격 <= 우선순위 가장 높게
    /// </summary>
    public bool Attack()
    {
        if (isAttacking)
            return false;

        if(canRushAttack)
        {
            RushAttack();
        }
        if(curHp / (EnemyData.MaxHp * 1.0f) > 0.9)
        {

        }

        return true;
    }

    public void TakeDamage(int damage)
    {
        curHp -= (EnemyData.Defence - damage);
        if (curHp <= 0)
        {
            Die();
        }
    }

    private void AttackPattern1()
    {
        isAttacking = true;
        // Todo: 총알 생성
        isAttacking = false;
    }

    private void AttackPattern2()
    {
        isAttacking = true;
        // Todo: 미사일 생성
        isAttacking = false;
    }

    private void AttackPattern3()
    {
        isAttacking = true;
        // Todo: 돌진 공격
        isAttacking = false;
    }

    private void RushAttack()
    {
        isAttacking = true;
    }
}
