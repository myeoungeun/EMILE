using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   private IAttackHandler attacker;
   private int damage;
   private float speed;
   private int interval;
   private int count;
   private Vector3 moveDirection; //�Ѿ� �̵� ����

   public void Initialize(int sDamage, float sSpeed, int sInterval, int sCount, bool lookDirectionRight) //bullet���� ������ ����, ���� ����
   {
      damage = sDamage; //�Ѿ� ������
      speed = sSpeed;
      interval = sInterval;
      count = sCount;
      moveDirection = lookDirectionRight ? Vector3.right : Vector3.left; //������ ���� ������ �߻�, ���� ���������� ���� �߻�
   }
   
   void Update()
   {
         transform.Translate(moveDirection * speed * Time.deltaTime);
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      Debug.Log("�浹 �۵���");
      if (other.CompareTag("Monster"))
      {
         //���̶� �浹 ����
         Debug.Log("���Ͷ� �浹");
         //var monster = other.GetComponent<Monster>();
         //if(monster != null){ monster.TakeDamage(damage);}
         Destroy(gameObject);
      }
      if (other.CompareTag("Wall"))
      {
         Debug.Log("���̶� �浹");
         Destroy(gameObject);
      }
      if (other.CompareTag("DeadZone"))
      {
         Destroy(gameObject);
      }
   }
}
