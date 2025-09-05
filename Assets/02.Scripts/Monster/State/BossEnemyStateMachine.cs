using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Rendering.InspectorCurveEditor;

public class BossEnemyStateMachine : EnemyStateMachine
{
    public BossEnemyStateMachine(Enemy owner) : base(owner)
    {
    }

    protected override void Init()
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
