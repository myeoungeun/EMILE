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
   private Vector3 moveDirection; //총알 이동 방향

   public void Initialize(int sid, int sDamage, float sSpeed, int sInterval, int sCount, AttackType currentAttackType, BulletType currentBulletType, bool lookDirectionRight) //bullet에게 공격자 정보, 방향 전달
   {
      attacker = currentAttackType;
      bulletType = currentBulletType;
      id = sid;
      damage = sDamage;
      speed = sSpeed;
      interval = sInterval; //딜레이로 수정 필요
      count = sCount; //총알 개수
      moveDirection = lookDirectionRight ? Vector3.right : Vector3.left; //오른쪽 보면 오른쪽 발사, 왼쪽 보고있으면 왼쪽 발사
   }
   
   void Update()
   {
      transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      Debug.Log("충돌 작동중");
      if (attacker == AttackType.Player && other.CompareTag("Monster"))
      {
         //적이랑 충돌 판정
         Debug.Log("몬스터랑 충돌");
         //var monster = other.GetComponent<Monster>();
         //if(monster != null){ monster.TakeDamage(damage);}
         if (bulletType != BulletType.Pierce)
         {
            Destroy(gameObject);
         }
      }

      if (attacker == AttackType.Player && other.CompareTag("Player"))
      {
         Debug.Log("플레이어랑 충돌");
         //플레이어 피 깎기
         //필요하다면 탄환 파괴되는 애니메이션 추가
         Destroy(gameObject);
      }
      if ((bulletType != BulletType.Pierce) && other.CompareTag("Wall")) //관통탄이 아닌 모든 탄은 벽에 닿으면 파괴
      {
         Debug.Log("벽이랑 충돌");
         Destroy(gameObject);
      }
      if (other.CompareTag("DeadZone")) //데드존에서 모든 탄환 파괴
      {
         Destroy(gameObject);
      }
   }
}
