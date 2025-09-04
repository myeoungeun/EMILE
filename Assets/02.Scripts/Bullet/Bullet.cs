using System;
using System.Collections;
using System.Collections.Generic;
using Monster;
using UnityEngine;

public class Bullet : MonoBehaviour
{ 
    protected AttackType attacker;
   protected BulletType bulletType;
   protected int Id;
   protected int Damage;
   protected float Speed;
   protected int Interval;
   protected int Count;

   public void Initialize(int sid, int sDamage, float sSpeed, int sInterval, int sCount, AttackType currentAttackType, BulletType currentBulletType) //bullet���� ������ ����, ���� ����
   {
      attacker = currentAttackType;
      bulletType = currentBulletType;
      Id = sid;
      Damage = sDamage;
      Speed = sSpeed;
      Interval = sInterval; //�����̷� ���� �ʿ�
      Count = sCount; //�Ѿ� ����
   }
   
   protected virtual void Update()
   {
       transform.Translate( transform.right * Speed * Time.deltaTime, Space.World);
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
   
   protected void DealDamage(IDamageable target)
   {
       Debug.Log(target);
       if (target != null)
       {
           target.TakeDamage(Damage);
           Debug.Log($"[DealDamage] ID={Id}, Damage={Damage}, Type={bulletType}, Obj={gameObject.GetInstanceID()}");
       }
       if (bulletType != BulletType.Pierce) //����ź�϶��� �ı�x, �����
       {
           Destroy(gameObject);
       }
   }
}
