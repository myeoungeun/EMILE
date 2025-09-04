using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected EnemyData _enemyData;
    public EnemyData EnemyData {  get { return _enemyData; } }

    [SerializeField] private EnemyStateMachine stateMachine;
    public EnemyStateMachine StateMachine { get { return stateMachine; } }

    [SerializeField] protected int curHp;
    public int CurHp { get { return curHp; } }
    protected Vector3 originPos;

    public Transform target;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D col;
    [SerializeField] private Animator anim;
    public Animator Anim { get { return anim; } }

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponentInChildren<Collider2D>();
        anim = GetComponentInChildren<Animator>();

        stateMachine = new EnemyStateMachine(this);

        curHp = EnemyData.MaxHp;
        originPos = transform.position;
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

    protected virtual void Die()
    {
        // Todo: 오브젝트 풀로 리턴
        Destroy(gameObject);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void ResetTarget()
    {
        this.target = null;
    }

    public virtual void LookTarget()
    {
        float d = target.position.x < transform.position.x ? Mathf.Abs(transform.localScale.x) : -Mathf.Abs(transform.localScale.x);
        transform.localScale = new Vector3(d, transform.localScale.y, transform.localScale.z);
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

    // 플레이어가 몬스터에 부딪혔을 때, 몬스터의 공격력 만큼의 피해를 주는 메서드, 연결 필요함
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer) == LayerMask.GetMask(Monster.Layers.Player))
        {
            // Todo: 플레이어에게 피해를 줌
            Debug.Log($"플레이어 충돌 피해: {EnemyData.AttackPower}");
        }
    }

    // 에디터에서 탐지 범위와 공격 가능 범위를 표시하는 메서드
    private void OnDrawGizmos()
    {
        if(_enemyData == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _enemyData.DetectRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _enemyData.AttackRange);
    }
}
