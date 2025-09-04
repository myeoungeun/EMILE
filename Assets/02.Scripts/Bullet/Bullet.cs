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

   public void Initialize(int sid, int sDamage, float sSpeed, int sInterval, int sCount, AttackType currentAttackType, BulletType currentBulletType) //bullet에게 공격자 정보, 방향 전달
   {
      attacker = currentAttackType;
      bulletType = currentBulletType;
      Id = sid;
      Damage = sDamage;
      Speed = sSpeed;
      Interval = sInterval; //딜레이로 수정 필요
      Count = sCount; //총알 개수
   }
   
   protected virtual void Update()
   {
       transform.Translate( transform.right * Speed * Time.deltaTime, Space.World);
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
   
   protected void DealDamage(IDamageable target)
   {
       Debug.Log(target);
       if (target != null)
       {
           target.TakeDamage(Damage);
           Debug.Log($"[DealDamage] ID={Id}, Damage={Damage}, Type={bulletType}, Obj={gameObject.GetInstanceID()}");
       }
       if (bulletType != BulletType.Pierce) //관통탄일때는 파괴x, 통과함
       {
           Destroy(gameObject);
       }
   }
}
