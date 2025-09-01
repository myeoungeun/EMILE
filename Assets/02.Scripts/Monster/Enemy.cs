using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected EnemyData _enemyData;
    public EnemyData EnemyData {  get { return _enemyData; } private set { _enemyData = value; } }

    [SerializeField] private EnemyStateMachine stateMachine;
    public EnemyStateMachine StateMachine { get { return stateMachine; } private set { stateMachine = value; } }

    protected int curHp;
    private Vector3 originPos;

    public Transform target;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D col;

    private void Awake()
    {
        stateMachine = new EnemyStateMachine(this);
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    private void Start()
    {
        originPos = transform.position;
    }

    private void Update()
    {
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    public void TakeDamage(int damage)
    {
        curHp -= damage;
        if(curHp <= 0)
            Die();
    }
    
    private void Die()
    {
        // Todo: 오브젝트 풀로 리턴
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void ResetTarget()
    {
        this.target = null;
    }

    /// <summary>
    /// 타겟이 존재한다면 타겟과의 거리를 가져오는 함수
    /// </summary>
    /// <returns>타겟과의 거리, 타겟이 null이면 최대값 반환</returns>
    public float GetDistanceToTarget()
    {
        if (target == null)
            return float.MaxValue;
        float dist = Vector3.Distance(transform.position, target.position);

        return dist;
    }

    private void OnDrawGizmos()
    {
        if(_enemyData == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _enemyData.DetectRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _enemyData.AttackRange);
    }
}
