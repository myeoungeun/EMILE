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
   protected Vector3 MoveDirection; //총알 이동 방향

   public void Initialize(int sid, int sDamage, float sSpeed, int sInterval, int sCount, AttackType currentAttackType, BulletType currentBulletType, bool lookDirectionRight) //bullet에게 공격자 정보, 방향 전달
   {
      attacker = currentAttackType;
      bulletType = currentBulletType;
      Id = sid;
      Damage = sDamage;
      Speed = sSpeed;
      Interval = sInterval; //딜레이로 수정 필요
      Count = sCount; //총알 개수
      MoveDirection = lookDirectionRight ? Vector3.right : Vector3.left; //오른쪽 보면 오른쪽 발사, 왼쪽 보고있으면 왼쪽 발사
      
      SpriteRenderer sr = GetComponent<SpriteRenderer>();
      if (sr != null)
      {
          sr.flipX = !lookDirectionRight; //플레이어 왼쪽이면 이미지도 뒤집음
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

   protected virtual void HandleCollision(Collider2D other) //자식에서 재정의하기 위한 가상 메서드
   {
      var iDamageable = other.GetComponent<IDamageable>(); //충돌한 오브젝트에 IDamageable 인터페이스가 있으면 그걸 가져옴
      
      if (attacker == AttackType.Player && other.CompareTag("Monster")) //공격자가 정해져있어서 본인이 쏜 총에 안 맞음
      {
         Debug.Log("플레이어->몬스터 공격");
         DealDamage(iDamageable);
      }
      
      if (attacker == AttackType.Enemy && other.CompareTag("Player"))
      {
         Debug.Log("몬스터->플레이어 공격");
         DealDamage(iDamageable);
      }
      
      if (other.CompareTag("Wall")) //모든 탄환은 벽에 닿으면 파괴
      {
         Destroy(gameObject);
      }
      
      if (other.CompareTag("DeadZone")) //데드존에서 모든 탄환 파괴
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
       if (bulletType != BulletType.Pierce) //관통탄일때는 파괴x, 통과함
       {
           Destroy(gameObject);
       }
   }
}
