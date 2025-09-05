using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRushPatternState : EnemyBaseState
{
    BossEnemy boss;

    public BossRushPatternState(EnemyStateMachine owner) : base(owner)
    {
        boss = stateMachine.Enemy as BossEnemy;
    }

    public override void Enter()
    {
        base.Enter();
        boss.StartRushPattern();
    }

    public override void Update()
    {
        base.Update();
        if(!boss.IsAttacking)
        {
            stateMachine.ChangeState(Monster.EnemyStateType.Phase2);
        }
    }

    public override void Exit()
    {
        base.Exit();
        (stateMachine.Enemy as BossEnemy).StopAllPattern();
    }
}
