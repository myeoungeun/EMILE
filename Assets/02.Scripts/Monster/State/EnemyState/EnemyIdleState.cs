using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    // 여기서 해야하는 거
    // 평소에는 그냥 가만히 있음
    // 플레이어를 감지하면 추적 상태로 변경
    private LayerMask layerMask = LayerMask.GetMask("Player");

    public EnemyIdleState(EnemyStateMachine owner) : base(owner)
    {

    }

    public override void Enter()
    {
        stateMachine.Enemy.ResetTarget();
    }

    public override void Update()
    {
        Collider2D player = Physics2D.OverlapCircle(stateMachine.Enemy.transform.position, stateMachine.Enemy.EnemyData.DetectRange, layerMask);
        if (player != null)
        {
            stateMachine.Enemy.SetTarget(player.transform);
            stateMachine.ChangeState(Monster.EnemyStateType.Detect);
        }
    }

}
