using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
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
        
        stateMachine.Enemy.Anim?.SetBool(Monster.AnimatorParams.InAttackRange, true);
        attacker.StartAttack();
    }

    public override void Update()
    {
        stateMachine.Enemy.LookTarget();
        // 공격 범위 밖으로 나가면 '인식 상태'로 전환
        if (stateMachine.Enemy.GetDistanceToTarget() > stateMachine.Enemy.EnemyData.AttackRange)
        {
            stateMachine.ChangeState(Monster.EnemyStateType.Detect);
        }
    }

    public override void Exit()
    {
        attacker.StopAttack();
        attacker = null;
        stateMachine.Enemy.Anim?.SetBool(Monster.AnimatorParams.InAttackRange, false);
    }
}
