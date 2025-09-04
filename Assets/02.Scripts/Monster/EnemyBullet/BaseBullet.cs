using Monster;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    [SerializeField] private BulletData bulletData;
    public BulletData BulletData { get { return bulletData; } }

    protected Rigidbody2D rb;

    protected Vector2 dir;

    // 나중에 공통 총알이면 데이터 변경이 가능하도록
    public void InitData(BulletData newData)
    {
        bulletData = newData;
    }

    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void Init(Transform target)
    {
        dir = (target.position - transform.position).normalized;
    }

    protected virtual void FixedUpdate()
    {
        Move();
    }

    protected virtual void Move()
    {
        if (dir != Vector2.zero)
            rb.velocity = dir * bulletData.Speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HandleCollision(other);
    }

    protected virtual void HandleCollision(Collider2D other) //자식에서 재정의하기 위한 가상 메서드
    {
        var iDamageable = other.GetComponent<IDamageable>(); //충돌한 오브젝트에 IDamageable 인터페이스가 있으면 그걸 가져옴

        if (bulletData.AttackType == global::AttackType.Player && other.CompareTag("Monster")) //공격자가 정해져있어서 본인이 쏜 총에 안 맞음
        {
            DealDamage(iDamageable);
        }

        if (bulletData.AttackType == global::AttackType.Enemy && other.CompareTag("Player"))
        {
            DealDamage(iDamageable);
        }

        if (other.CompareTag("Wall")) //모든 탄환은 벽에 닿으면 파괴
        {
            BulletPoolManager.Instance.ReturnBullet(this);
        }

        if (other.CompareTag("DeadZone")) //데드존에서 모든 탄환 파괴
        {
            BulletPoolManager.Instance.ReturnBullet(this);
        }
    }

    private void DealDamage(IDamageable target)
    {
        if (target != null)
        {
            target.TakeDamage(bulletData.Damage);
        }
        BulletPoolManager.Instance.ReturnBullet(this);
    }
}