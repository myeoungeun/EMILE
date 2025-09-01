using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy, Monster.IAttackable, Monster.IMovable
{
    [SerializeField] Transform bulletPos;
    [SerializeField] Transform missilePos;

    private List<System.Action> patterns;

    private bool isAttacking;
    private int patternIndex;

    private void Start()
    {
        patterns = new List<System.Action>();
        patterns.Add(AttackPattern1);
        patterns.Add(AttackPattern2);
        patterns.Add(AttackPattern3);

        patternIndex = 0;
        isAttacking = false;
    }

    /// <summary>
    /// 현재는 어택인덱스와 스위치-케이스 구문으로 다음 패턴을 실행
    /// 함수 리스트 같은 느낌으로 실행이 가능하지 않을까?
    /// </summary>
    public bool Attack()
    {
        if (isAttacking)
            return false;

        patterns[patternIndex]?.Invoke();
        patternIndex = (patternIndex + 1) % patterns.Count;

        return true;
    }

    public void Move()
    {
        // 보스는 딱히 안 쓸듯
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
}
