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
   private Vector3 moveDirection; //총알 이동 방향

   public void Initialize(int sDamage, float sSpeed, int sInterval, int sCount, bool lookDirectionRight) //bullet에게 공격자 정보, 방향 전달
   {
      damage = sDamage; //총알 데미지
      speed = sSpeed;
      interval = sInterval;
      count = sCount;
      moveDirection = lookDirectionRight ? Vector3.right : Vector3.left; //오른쪽 보면 오른쪽 발사, 왼쪽 보고있으면 왼쪽 발사
   }
   
   void Update()
   {
         transform.Translate(moveDirection * speed * Time.deltaTime);
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      Debug.Log("충돌 작동중");
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
         Debug.Log("벽이랑 충돌");
         Destroy(gameObject);
      }
      if (other.CompareTag("DeadZone"))
      {
         Destroy(gameObject);
      }
   }
}
