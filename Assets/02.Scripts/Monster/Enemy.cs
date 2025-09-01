using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected EnemyData _enemyData;

    private EnemyStateMachine stateMachine;
    public EnemyStateMachine StateMachine { get { return stateMachine; } private set { stateMachine = value; } }

    protected int curHp;

    private void Update()
    {
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    public void TakeDamage(int damage)
    {
        curHp -= damage;
        Die();
    }
    
    private void Die()
    {
        // 아마 풀로 돌아갈 듯
    }
}
