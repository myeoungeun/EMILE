using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   private IAttackHandler attacker;
   public float speed = 20f; //����Ƽâ bullet prefabs���� ���� ����
   private int damage;
   private float scale;
   private Vector3 moveDirection; //�Ѿ� �̵� ����

   public void Initialize(int damage, float shotScale, bool lookDirectionRight) //bullet���� ������ ����, ���� ����
   {
      this.damage = damage; //�Ѿ� ������
      this.scale = shotScale; //�Ѿ� ũ��
      this.moveDirection = lookDirectionRight ? Vector3.right : Vector3.left; //������ ���� ������ �߻�, ���� ���������� ���� �߻�
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
