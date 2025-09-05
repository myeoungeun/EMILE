using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhase1State : EnemyBaseState
{
    BossEnemy boss;
    public BossPhase1State(EnemyStateMachine owner) : base(owner)
    {
        boss = stateMachine.Enemy as BossEnemy;
    }

    public override void Enter()
    {
        // 페이즈1 패턴 시작
        boss?.Phase1();
    }

    public override void Update()
    {
        if(stateMachine.Enemy.CurHp * 1.0f / stateMachine.Enemy.EnemyData.MaxHp < 0.9)
        {
            stateMachine.ChangeState(Monster.EnemyStateType.Phase2);
        }
    }

    public override void Exit()
    {
        base.Exit();
        // 페이즈1 패턴 종료
        boss?.StopAllPattern();
    }
}
