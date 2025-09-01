using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   private IAttackHandler attacker;
   public float speed = 10f;
   private int damage;
   private float angle;
   private Vector3 moveDirection; //�Ѿ� �̵� ����

   public void Initialize(IAttackHandler iattackHandler, bool lookDirectionRight) //bullet���� ������ ����, ���� ����
   {
      attacker = iattackHandler;
      attacker.OnShot(out damage, out angle);
      moveDirection = lookDirectionRight ? Vector3.right : Vector3.left;
   }
   
   void Update()
   {
         transform.Translate(moveDirection * speed * Time.deltaTime);
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
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
         Destroy(gameObject);
      }
      if (other.CompareTag("DeadZone"))
      {
         Destroy(gameObject);
      }
   }
}
