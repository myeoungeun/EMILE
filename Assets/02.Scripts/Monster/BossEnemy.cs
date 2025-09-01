using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy, Monster.IAttackable, Monster.IMovable
{
    [SerializeField] Transform bulletPos;
    [SerializeField] Transform missilePos;

    private int attackIndex;

    private void Start()
    {
        attackIndex = 0;
    }

    /// <summary>
    /// 현재는 어택인덱스와 스위치-케이스 구문으로 다음 패턴을 실행
    /// 함수 리스트 같은 느낌으로 실행이 가능하지 않을까?
    /// </summary>
    public void Attack()
    {
        switch (attackIndex)
        {
            case 0:
                AttackPattern1();
                break;
            case 1:
                AttackPattern2();
                break;
            case 2:
                AttackPattern3();
                break;
        }

        attackIndex = attackIndex == 2 ? 0 : attackIndex + 1; 
    }

    public void Move()
    {
        
    }

    private void AttackPattern1()
    {
        // Todo: 총알 생성
    }

    private void AttackPattern2()
    {
        // Todo: 미사일 생성
    }

    private void AttackPattern3()
    {
        // Todo: 돌진 공격
    }
}
