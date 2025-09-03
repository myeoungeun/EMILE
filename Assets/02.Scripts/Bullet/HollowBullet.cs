using System.Collections;
using System.Collections.Generic;
using Monster;
using UnityEngine;

using UnityEngine;

public class HollowBullet : Bullet
{
    //도트 데미지 걸린 대상
    private Dictionary<IDamageable, Coroutine> activeHollows = new Dictionary<IDamageable, Coroutine>();

    protected override void HandleCollision(Collider2D other)
    {
        var iDamageable = other.GetComponent<IDamageable>();

        if (attacker == AttackType.Player && other.CompareTag("Monster"))
        {
            Debug.Log($"HollowBullet Hit -> Damage: {Damage}");
            DealDamage(iDamageable); //기본 데미지
            
            if (iDamageable != null && bulletType == BulletType.Hollow) //도트 데미지 적용
            {
                if (activeHollows.ContainsKey(iDamageable)) // 기존 Coroutine 있으면 중단 (중간에 맞으면 지속시간 초기화)
                {
                    StopCoroutine(activeHollows[iDamageable]);
                    activeHollows.Remove(iDamageable);
                }

                Coroutine dot = StartCoroutine(HollowDOT(iDamageable, Damage / 5, 10f)); //데미지 1/5를 10초간 적용
                activeHollows.Add(iDamageable, dot);
            }
        }

        if (other.CompareTag("Wall") || other.CompareTag("DeadZone"))
            Destroy(gameObject);
    }

    private IEnumerator HollowDOT(IDamageable target, int dotDamage, float duration)
    {
        float timer = 0f;
        
        while (timer < duration)
        {
            target.TakeDamage(dotDamage);
            Debug.Log($"Hollow DOT: {dotDamage} -> Target: {target}");
            timer += 1f;
            yield return new WaitForSeconds(1f);
        }

        // 지속시간 끝나면 기록 삭제
        if (activeHollows.ContainsKey(target))
            activeHollows.Remove(target);
    }
}
