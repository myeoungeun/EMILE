using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   private AttackType attacker;
   private BulletType bulletType;
   private int id;
   private int damage;
   private float speed;
   private int interval;
   private int count;
   private Vector3 moveDirection; //�Ѿ� �̵� ����

   public void Initialize(int sid, int sDamage, float sSpeed, int sInterval, int sCount, AttackType currentAttackType, BulletType currentBulletType, bool lookDirectionRight) //bullet���� ������ ����, ���� ����
   {
      attacker = currentAttackType;
      bulletType = currentBulletType;
      id = sid;
      damage = sDamage;
      speed = sSpeed;
      interval = sInterval; //�����̷� ���� �ʿ�
      count = sCount; //�Ѿ� ����
      moveDirection = lookDirectionRight ? Vector3.right : Vector3.left; //������ ���� ������ �߻�, ���� ���������� ���� �߻�
   }
   
   void Update()
   {
      transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      Debug.Log("�浹 �۵���");
      if (attacker == AttackType.Player && other.CompareTag("Monster"))
      {
         //���̶� �浹 ����
         Debug.Log("���Ͷ� �浹");
         //var monster = other.GetComponent<Monster>();
         //if(monster != null){ monster.TakeDamage(damage);}
         if (bulletType != BulletType.Pierce)
         {
            Destroy(gameObject);
         }
      }

      if (attacker == AttackType.Player && other.CompareTag("Player"))
      {
         Debug.Log("�÷��̾�� �浹");
         //�÷��̾� �� ���
         //�ʿ��ϴٸ� źȯ �ı��Ǵ� �ִϸ��̼� �߰�
         Destroy(gameObject);
      }
      if ((bulletType != BulletType.Pierce) && other.CompareTag("Wall")) //����ź�� �ƴ� ��� ź�� ���� ������ �ı�
      {
         Debug.Log("���̶� �浹");
         Destroy(gameObject);
      }
      if (other.CompareTag("DeadZone")) //���������� ��� źȯ �ı�
      {
         Destroy(gameObject);
      }
   }
}
