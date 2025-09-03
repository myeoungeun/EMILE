using System;
using System.Collections;
using System.Collections.Generic;
using Monster;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   private AttackType attacker;
   private BulletType bulletType;
   protected int Id;
   protected int Damage;
   protected float Speed;
   protected int Interval;
   protected int Count;
   protected Vector3 MoveDirection; //�Ѿ� �̵� ����

   public void Initialize(int sid, int sDamage, float sSpeed, int sInterval, int sCount, AttackType currentAttackType, BulletType currentBulletType, bool lookDirectionRight) //bullet���� ������ ����, ���� ����
   {
      attacker = currentAttackType;
      bulletType = currentBulletType;
      Id = sid;
      Damage = sDamage;
      Speed = sSpeed;
      Interval = sInterval; //�����̷� ���� �ʿ�
      Count = sCount; //�Ѿ� ����
      MoveDirection = lookDirectionRight ? Vector3.right : Vector3.left; //������ ���� ������ �߻�, ���� ���������� ���� �߻�
      
      SpriteRenderer sr = GetComponent<SpriteRenderer>();
      if (sr != null)
      {
          sr.flipX = !lookDirectionRight; //�÷��̾� �����̸� �̹����� ������
      }
   }
   
   void Update()
   {
      transform.Translate(MoveDirection * Speed * Time.deltaTime, Space.World);
   }
   
   private void OnTriggerEnter2D(Collider2D other)
   {
      HandleCollision(other);
   }

   protected virtual void HandleCollision(Collider2D other) //�ڽĿ��� �������ϱ� ���� ���� �޼���
   {
      var iDamageable = other.GetComponent<IDamageable>(); //�浹�� ������Ʈ�� IDamageable �������̽��� ������ �װ� ������
      
      if (attacker == AttackType.Player && other.CompareTag("Monster")) //�����ڰ� �������־ ������ �� �ѿ� �� ����
      {
         Debug.Log("�÷��̾�->���� ����");
         DealDamage(iDamageable);
      }
      
      if (attacker == AttackType.Enemy && other.CompareTag("Player"))
      {
         Debug.Log("����->�÷��̾� ����");
         DealDamage(iDamageable);
      }
      
      if (other.CompareTag("Wall")) //��� źȯ�� ���� ������ �ı�
      {
         Destroy(gameObject);
      }
      
      if (other.CompareTag("DeadZone")) //���������� ��� źȯ �ı�
      {
         Destroy(gameObject);
      }
   }
   
   private void DealDamage(IDamageable target)
   {
       if (target != null)
       {
           target.TakeDamage(Damage);
           Debug.Log(Damage);
       }
       if (bulletType != BulletType.Pierce) //����ź�϶��� �ı�x, �����
       {
           Destroy(gameObject);
       }
   }
}
