using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyStateMachine : EnemyStateMachine
{
    public BossEnemyStateMachine(Enemy owner) : base(owner)
    {
    }

    // 보스는 일반 공격 대신 패턴을 진행하도록 수정
    public override void Init()
    {
        enemyStates = new Dictionary<Monster.EnemyStateType, EnemyBaseState>();
        enemyStates[Monster.EnemyStateType.Idle] = new EnemyIdleState(this);
        enemyStates[Monster.EnemyStateType.Detect] = new EnemyDetectState(this);
        BossPhase1State phase1State = new BossPhase1State(this);
        enemyStates[Monster.EnemyStateType.Attack] = phase1State;
        enemyStates[Monster.EnemyStateType.Phase1] = phase1State;
        enemyStates[Monster.EnemyStateType.Phase2] = new BossPhase2State(this);
        enemyStates[Monster.EnemyStateType.Rush] = new BossRushPatternState(this);

        curState = enemyStates[Monster.EnemyStateType.Idle];
        curState?.Enter();
    }
}
