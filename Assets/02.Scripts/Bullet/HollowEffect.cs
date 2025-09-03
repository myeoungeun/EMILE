using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using Monster;
using UnityEngine;

public class HollowEffect : MonoBehaviour
{
    private IDamageable target;
    private float totalDuration = 10f;  // 상태 지속 시간
    private float tickInterval = 1f;    // 1초마다 피해
    private int damagePerTick;

    private Coroutine dotCoroutine;

    public void Apply(IDamageable t, int bulletDamage)
    {
        target = t;
        damagePerTick = Mathf.CeilToInt(bulletDamage / 5f);

        if (dotCoroutine != null)
        {
            StopCoroutine(dotCoroutine); // 기존 상태 초기화
            Debug.Log("Hollow DOT 초기화, 지속시간 재시작");
        }
        dotCoroutine = StartCoroutine(DOT());
    }

    private IEnumerator DOT()
    {
        float elapsed = 0f;
        while (elapsed < totalDuration)
        {
            if (target != null)
            {
                target.TakeDamage(damagePerTick);
                Debug.Log($"Hollow DOT 적용: {damagePerTick} 데미지, 경과 {elapsed+tickInterval}s");
            }
            yield return new WaitForSeconds(tickInterval);
            elapsed += tickInterval;
        }
        dotCoroutine = null;
        Destroy(this); // 상태 종료 후 스크립트 제거
        Debug.Log("Hollow 상태 종료");
    }
}
