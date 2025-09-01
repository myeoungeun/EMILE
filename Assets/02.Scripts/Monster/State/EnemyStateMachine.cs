using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyStateMachine
{
    [SerializeField] private Enemy enemy;
    public Enemy Enemy { get { return enemy; } private set { enemy = value; } }

    private EnemyBaseState curState;
    private Dictionary<Monster.EnemyStateType, EnemyBaseState> enemyStates;

    public EnemyStateMachine(Enemy owner)
    {
        enemy = owner;
        Init();
    }

    private void Init()
    {
        enemyStates = new Dictionary<Monster.EnemyStateType, EnemyBaseState>();
        enemyStates[Monster.EnemyStateType.Idle] = new EnemyIdleState(this);
        enemyStates[Monster.EnemyStateType.Detect] = new EnemyDetectState(this);
        enemyStates[Monster.EnemyStateType.Attack] = new EnemyAttackState(this);

        curState = enemyStates[Monster.EnemyStateType.Idle];
        curState?.Enter();
    }

    public void ChangeState(Monster.EnemyStateType stateType)
    {
        curState?.Exit();
        curState = enemyStates[stateType];
        curState?.Enter();
    }

    public void Update()
    {
        curState?.Update();
    }

    public void FixedUpdate()
    {
        curState?.FixedUpdate();
    }
}
