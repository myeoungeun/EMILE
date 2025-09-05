using Monster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectState : EnemyBaseState
{
    // 여기서 해야하는 거
    // 이동 가능한 적이라면 기본적으로 대상을 추적하여 이동
    // 플레이어가 공격범위 내로 들어오면 공격 상태로 변경
    // 플레이어가 추적 범위 밖으로 나가면 대기 상태로 변경

    bool canMove;
    IMovable movable;

    public EnemyDetectState(EnemyStateMachine owner) : base(owner)
    {
    }

    public override void Enter()
    {
        canMove = stateMachine.Enemy.EnemyData.MoveSpeed > 0 ? true : false;
        if (stateMachine.Enemy is IMovable)
            movable = stateMachine.Enemy as IMovable;
        stateMachine.Enemy.Anim?.SetBool(Monster.AnimatorParams.InDetectRange, true);
    }

    public override void Update()
    {
        stateMachine.Enemy.LookTarget();
        // 공격 범위에 들어오면 공격 상태로 전환
        if (stateMachine.Enemy.GetDistanceToTarget() <= stateMachine.Enemy.EnemyData.AttackRange)
        {
            stateMachine.ChangeState(Monster.EnemyStateType.Attack);
        }
        // 탐지 범위를 벗어 나면 기본 상태로 전환
        else if(stateMachine.Enemy.GetDistanceToTarget() > stateMachine.Enemy.EnemyData.DetectRange)
        {
            stateMachine.ChangeState(Monster.EnemyStateType.Idle);
        }
    }

    public override void FixedUpdate()
    {
        // 이동 가능한 적이라면 플레이어에게 접근
        if (canMove && movable != null)
        {
            movable.Move();
        }
    }

    public override void Exit()
    {
        movable = null;
    }
}
