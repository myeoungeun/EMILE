using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : Enemy, Monster.IAttackable, Monster.IDamageable
{
    // 총알이 발사되는 위치
    [SerializeField] private Transform bulletPos;

    // 일단 현재는 총알 프리팹을 인스펙터로 등록하여 사용
    public GameObject bullet;
    private Coroutine attackCoroutine;

    protected override void Awake()
    {
        base.Awake();
    }

    public void StartAttack()
    {
        if(attackCoroutine == null)
        {
            attackCoroutine = StartCoroutine(Attack());
        }
    }

    public void StopAttack()
    {
        if(attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }
        attackCoroutine = null;
    }

    public IEnumerator Attack()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f / EnemyData.AttackSpeed);
            Vector2 dir = (target.position - transform.position).normalized;

            // Todo: 오브젝트 풀에서 총알을 생성
            Instantiate(bullet, bulletPos.position, Quaternion.identity).GetComponent<TestBullet>().Init(5f, EnemyData.AttackPower, dir);
        }
    }

    public void TakeDamage(int damage)
    {
        curHp -= (EnemyData.Defence - damage);
        if(curHp <= 0)
        {
            Die();
        }
    }
}
