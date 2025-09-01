using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   private IAttackHandler attacker;
   public float speed = 20f; //유니티창 bullet prefabs에서 수정 가능
   private int damage;
   private float scale;
   private Vector3 moveDirection; //총알 이동 방향

   public void Initialize(int damage, float shotScale, bool lookDirectionRight) //bullet에게 공격자 정보, 방향 전달
   {
      this.damage = damage; //총알 데미지
      this.scale = shotScale; //총알 크기
      this.moveDirection = lookDirectionRight ? Vector3.right : Vector3.left; //오른쪽 보면 오른쪽 발사, 왼쪽 보고있으면 왼쪽 발사
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
