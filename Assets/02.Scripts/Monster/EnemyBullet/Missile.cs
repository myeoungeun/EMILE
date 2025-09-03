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
        chasingTime = 1f;
    }

    private void OnEnable()
    {
        transform.localEulerAngles = Vector3.zero;
    }

    protected override void Move()
    {
        //위로 이동
        rb.velocity = transform.up * BulletData.Speed;
        // 0.5초 후 추적 시작, 1.5초 부터 추적 종료
        if(rotateCoroutine == null)
            rotateCoroutine = StartCoroutine(Rotate());
    }

    private IEnumerator Rotate()
    {
        yield return new WaitForSeconds(0.5f);

        float curTime = 0f;
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
    }
}
