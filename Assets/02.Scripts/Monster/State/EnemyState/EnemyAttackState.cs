using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private float curAttackCoolTime;
    private Monster.IAttackable attacker;

    // 여기서 해야하는거
    // 플레이어에게 공격 실행
    // 플레이어가 공격 범위 밖으로 나가면 감지 상태로 변경
    public EnemyAttackState(EnemyStateMachine owner) : base(owner)
    {
    }

    public override void Enter()
    {
        if(stateMachine.Enemy is Monster.IAttackable)
        {
            attacker = stateMachine.Enemy as Monster.IAttackable;
        }
        curAttackCoolTime = 1f / stateMachine.Enemy.EnemyData.AttackSpeed;
        stateMachine.Enemy.Anim?.SetBool("InAttackRange", true);
    }

    public override void Update()
    {
        stateMachine.Enemy.LookTarget();
        // 공격 범위 밖으로 나가면 '인식 상태'로 전환
        if (stateMachine.Enemy.GetDistanceToTarget() > stateMachine.Enemy.EnemyData.AttackRange)
        {
            stateMachine.ChangeState(Monster.EnemyStateType.Detect);
        }

        // 공격 쿨타임이 지나면 공격
        if(curAttackCoolTime >= 1f / stateMachine.Enemy.EnemyData.AttackSpeed)
        {
            bool isAttacked = attacker.Attack();

            // 공격을 정상적으로 수행했을 때만 쿨타임 초기화
            if(isAttacked)
                curAttackCoolTime = 0f;
        }
        curAttackCoolTime += Time.deltaTime;
    }

    public override void Exit()
    {
        attacker = null;
        stateMachine.Enemy.Anim?.SetBool("InAttackRange", false);
    }
}
