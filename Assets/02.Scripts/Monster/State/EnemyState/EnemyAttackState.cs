using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    // 여기서 해야하는거
    // 플레이어에게 공격 실행
    // 플레이어가 공격 범위 밖으로 나가면 감지 상태로 변경
    public EnemyAttackState(EnemyStateMachine owner) : base(owner)
    {
    }
}
