using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : BaseBullet
{
    private Transform target;
    private Coroutine rotateCoroutine;
    public override void Init(Transform target)
    {
        this.target = target;
        rotateCoroutine = null;
    }

    private void OnEnable()
    {
        transform.localEulerAngles = Vector3.zero;
    }

    protected override void Move()
    {
        //위로 이동
        rb.velocity = transform.up * BulletData.Speed;
        // 1초 뒤부터 조금씩 회전하면서 플레이어 추적
        if(rotateCoroutine == null )
            rotateCoroutine = StartCoroutine(Rotate());
    }

    private IEnumerator Rotate()
    {
        yield return new WaitForSeconds(1f);

        while(target != null)
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
            yield return null;
        }
    }
}
