using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private EnemyBaseState curState;

    public EnemyStateMachine(Enemy owner)
    {
        enemy = owner;
    }

    public void ChangeState(EnemyBaseState state)
    {
        curState?.Exit();
        curState = state;
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
