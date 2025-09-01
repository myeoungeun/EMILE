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
   private Vector3 moveDirection; //총알 이동 방향

   public void Initialize(IAttackHandler iattackHandler, bool lookDirectionRight) //bullet에게 공격자 정보, 방향 전달
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
         //적이랑 충돌 판정
         Debug.Log("몬스터랑 충돌");
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
