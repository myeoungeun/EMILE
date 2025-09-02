using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    // 플레이어를 레이어를 통해 판별
    private LayerMask layerMask = LayerMask.GetMask("Player");

    public EnemyIdleState(EnemyStateMachine owner) : base(owner)
    {

    }

    public override void Enter()
    {
        stateMachine.Enemy.ResetTarget();
        stateMachine.Enemy.Anim?.SetBool("InDetectRange", false);
    }

    public override void Update()
    {
        // 현재는 적의 수가 적어서 업데이트에서 OverlapCircle을 계속 호출 -> 이후에는 최적화 시킬 필요가 있음
        Collider2D player = Physics2D.OverlapCircle(stateMachine.Enemy.transform.position, stateMachine.Enemy.EnemyData.DetectRange, layerMask);
        if (player != null)
        {
            // Todo: 레이캐스트를 해서 중간에 막히지 않으면 플레이어를 타겟으로 지정하도록 수정 예정
            stateMachine.Enemy.SetTarget(player.transform);
            stateMachine.ChangeState(Monster.EnemyStateType.Detect);
        }
    }

}
