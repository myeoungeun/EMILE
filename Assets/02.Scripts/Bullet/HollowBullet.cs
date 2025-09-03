using System.Collections;
using System.Collections.Generic;
using Monster;
using UnityEngine;

using UnityEngine;

public class HollowBullet : Bullet
{
    protected override void HandleCollision(Collider2D other)
    {
        if (attacker == AttackType.Player && other.CompareTag("Monster"))
        {
            IDamageable dmg = other.GetComponent<IDamageable>();
            if (dmg != null)
            {
                dmg.TakeDamage(Damage); // �⺻ ������
                Debug.Log($"hollow bullet ������ : {Damage}");

                // Hollow ���� ����
                HollowEffect effect = other.GetComponent<HollowEffect>();
                if (effect == null)
                {
                    effect = other.gameObject.AddComponent<HollowEffect>();
                }
                effect.Apply(dmg, Damage);
            }

            Destroy(gameObject); // �浹 �� �Ҹ�
        }
        else if (other.CompareTag("Wall") || other.CompareTag("DeadZone"))
        {
            Destroy(gameObject);
        }
    }
}

