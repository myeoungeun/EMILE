using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhase2State : EnemyBaseState
{
    private bool is60Trigger;
    private bool is30Trigger;

    BossEnemy boss;

    public BossPhase2State(EnemyStateMachine owner) : base(owner)
    {
        boss = stateMachine.Enemy as BossEnemy;
        is60Trigger = false;
        is30Trigger = false;
    }

    public override void Enter()
    {
        base.Enter();
        // 페이즈2 패턴 시작
        boss?.Phase2();
    }

    public override void Update()
    {
        base.Update();
        
        if(!is60Trigger && stateMachine.Enemy.CurHp * 1.0f / stateMachine.Enemy.EnemyData.MaxHp <= 0.6)
        {
            stateMachine.ChangeState(Monster.EnemyStateType.Rush);
            is60Trigger = true;
        }
        else if(!is30Trigger && stateMachine.Enemy.CurHp * 1.0f / stateMachine.Enemy.EnemyData.MaxHp <= 0.3)
        {
            stateMachine.ChangeState(Monster.EnemyStateType.Rush);
            is30Trigger = true;
        }
    }

    public override void Exit() 
    {
        base.Exit(); 
        // 페이즈2 패턴 종료
        boss?.StopAllPattern();
    }
}
