using System.Collections;
using System.Collections.Generic;
using Monster;
using UnityEngine;

using UnityEngine;

public class HollowBullet : Bullet
{
    //��Ʈ ������ �ɸ� ���
    private Dictionary<IDamageable, Coroutine> activeHollows = new Dictionary<IDamageable, Coroutine>();

    protected override void HandleCollision(Collider2D other)
    {
        var iDamageable = other.GetComponent<IDamageable>();

        if (attacker == AttackType.Player && other.CompareTag("Monster"))
        {
            Debug.Log($"HollowBullet Hit -> Damage: {Damage}");
            DealDamage(iDamageable); //�⺻ ������
            
            if (iDamageable != null && bulletType == BulletType.Hollow) //��Ʈ ������ ����
            {
                if (activeHollows.ContainsKey(iDamageable)) // ���� Coroutine ������ �ߴ� (�߰��� ������ ���ӽð� �ʱ�ȭ)
                {
                    StopCoroutine(activeHollows[iDamageable]);
                    activeHollows.Remove(iDamageable);
                }

                Coroutine dot = StartCoroutine(HollowDOT(iDamageable, Damage / 5, 10f)); //������ 1/5�� 10�ʰ� ����
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

        // ���ӽð� ������ ��� ����
        if (activeHollows.ContainsKey(target))
            activeHollows.Remove(target);
    }
}
