using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectState : EnemyBaseState
{
    // 여기서 해야하는 거
    // 플레이어가 공격범위 내로 들어오면 공격 상태로 변경
    // 플레이어가 추적 범위 밖으로 나가면

    private float attackRange;
    private float detectRange;

    public EnemyDetectState(EnemyStateMachine owner) : base(owner)
    {
    }
}
