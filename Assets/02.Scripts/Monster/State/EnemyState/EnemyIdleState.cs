using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    // 여기서 해야하는 거
    // 업데이트에서 속도가 0보다 크면 랜덤하게 이동
    // 플레이어를 감지하면 추적 상태로 변경

    public EnemyIdleState(EnemyStateMachine owner) : base(owner)
    {
    }

    public override void Update()
    {
        
    }

}
