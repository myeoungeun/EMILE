using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : BaseBullet
{
    private Transform target;

    public override void Init(Transform target)
    {
        this.target = target;
    }

    protected override void Move()
    {
        // 1초 간 위로 이동

        // 이후 조금씩 회전하면서 플레이어 추적
    }
}
