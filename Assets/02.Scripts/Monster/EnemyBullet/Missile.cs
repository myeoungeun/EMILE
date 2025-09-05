using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : BaseBullet
{
    private Transform target;
    private Coroutine rotateCoroutine;
    private float chasingTime;

    public override void Init(Transform target)
    {
        this.target = target;
        rotateCoroutine = null;
        chasingTime = 2f;
    }

    // 풀에서 꺼낼 때, 각도 초기화
    private void OnEnable()
    {
        transform.localEulerAngles = Vector3.zero;
    }

    protected override void Move()
    {
        // 지속적으로 자신의 위 방향으로 이동함
        rb.velocity = transform.up * BulletData.Speed;

        if(rotateCoroutine == null)
            rotateCoroutine = StartCoroutine(Rotate());
    }

    private IEnumerator Rotate()
    {
        // 1. 0.5초 대기
        yield return new WaitForSeconds(0.5f);

        float curTime = 0f;
        // 2. 1초 동안 플레이어 추적
        while(target != null && curTime <= chasingTime)
        {
            Vector2 dir = ((Vector2)target.position - rb.position).normalized;

            // 목표 회전값
            float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

            // 현재 회전에서 목표 회전으로 부드럽게 회전
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                BulletData.RotationSpeed * Time.deltaTime
            );

            curTime += Time.deltaTime;
            yield return null;
        }
        // 3. 생성 이후 1.5초가 지나면 그냥 직진
    }
}
