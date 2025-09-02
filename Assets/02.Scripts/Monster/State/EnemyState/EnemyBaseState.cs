using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : Monster.IState
{
    protected EnemyStateMachine stateMachine;

    public EnemyBaseState(EnemyStateMachine owner)
    {
        stateMachine = owner;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
}
