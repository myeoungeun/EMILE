using System;
using System.Collections;
using System.Collections.Generic;
using Monster;
using UnityEngine;

public class Bullet : MonoBehaviour
{ 
    protected BulletData bulletData;
    private Action<int,Bullet> poolEnqueue;
    private int bulletIndex;

    public void Initialize(BulletData BulletData, Action<int,Bullet> poolEnqueue,int index)
    {
        bulletData = BulletData;
        bulletIndex = index;
        this.poolEnqueue = poolEnqueue;
    }
   
   protected virtual void Update() //shot move
   {
       transform.Translate( transform.right * bulletData.Speed * Time.deltaTime, Space.World);
   }
   
   private void OnTriggerEnter2D(Collider2D other)
   {
      HandleCollision(other);
   }

   protected virtual void HandleCollision(Collider2D other) //충돌 처리
   {
      var iDamageable = other.GetComponent<IDamageable>();
      
      if (bulletData.AttackType == AttackType.Player && other.CompareTag("Monster"))
      {
         Debug.Log("player -> monster attack");
         DealDamage(iDamageable);
      }
      
      if (bulletData.AttackType == AttackType.Enemy && other.CompareTag("Player"))
      {
         Debug.Log("monster -> player attack");
         DealDamage(iDamageable);
      }
      
      if (other.CompareTag("Wall") || other.CompareTag("DeadZone") || other.CompareTag("BlockZone"))
      {
          ReturnToPool();
      }
   }
   
   protected void DealDamage(IDamageable target)
   {
       Debug.Log(target);
       if (target != null)
       {
           target.TakeDamage(bulletData.Damage);
           Debug.Log($"[DealDamage] ID={bulletData.Id}, Damage={bulletData.Damage}, Type={bulletData.BulletType}, Obj={gameObject.GetInstanceID()}");
       }
       if (bulletData.BulletType != BulletType.Pierce) //pierce일 경우 통과
       {
           ReturnToPool();
       }
   }
   
   private void ReturnToPool()
   {
       poolEnqueue.Invoke(bulletIndex,this);
   }
}
