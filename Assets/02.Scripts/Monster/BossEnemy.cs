using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy, IDamageable
{
    public event System.Action onHpChanged;

    [Header("Booster Effect")]
    [SerializeField] GameObject boost1;
    [SerializeField] GameObject boost2;

    [Header("Projectiles")]
    [SerializeField] Transform bulletPos;
    [SerializeField] Transform missilePos;

    private Coroutine pattern1;
    private Coroutine pattern2;
    private Coroutine pattern3;

    [Header("Rush")]
    [SerializeField] SpriteRenderer warningSign;
    [SerializeField] Color warningColor;
    Color originColor;

    [SerializeField] private float dashSpeed;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float respawnDelay;

    private bool isAttacking;
    public bool IsAttacking { get { return isAttacking; } }

    protected override void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponentInChildren<Collider2D>();
        anim = GetComponentInChildren<Animator>();

        stateMachine = new BossEnemyStateMachine(this);

        curHp = EnemyData.MaxHp;
        originPos = transform.position;

        isAttacking = false;

        warningSign.gameObject.SetActive(false);
        originColor = warningSign.color;
    }

    private void PauseAttack()
    {
        if (pattern1 != null)
        {
            StopCoroutine(pattern1);
            pattern1 = null;
        }
        if (pattern2 != null)
        {
            StopCoroutine(pattern2);
            pattern2 = null;
        }
        if (pattern3 != null)
        {
            StopCoroutine(pattern3);
            pattern3 = null;
        }
    }

    public void TakeDamage(int damage)
    {
        if (curHp <= 0)
            return;

        curHp -= Mathf.Abs(EnemyData.Defence - damage);
        curHp = Mathf.Max(curHp, 0);

        onHpChanged?.Invoke();
        if (curHp <= 0)
        {
            Die();
        }
    }

    public void Phase1()
    {
        pattern1 = StartCoroutine(AttackPattern1());
    }

    public void Phase2()
    {
        pattern1 = StartCoroutine(AttackPattern1());
        pattern2 = StartCoroutine(AttackPattern2());
    }

    public void StartRushPattern()
    {
        pattern3 = StartCoroutine(AttackPattern3());
    }

    public void StopAllPattern()
    {
        PauseAttack();
    }

    private IEnumerator AttackPattern1()
    {
        while(true)
        {
            yield return new WaitForSeconds(1 / EnemyData.AttackSpeed);
            // 총알 생성
            Vector2 dir = (target.position - transform.position).normalized;

            // Todo: 오브젝트 풀에서 총알을 생성
            BulletPoolManager.Instance.GetBulletById(EnemyData.EnemyBulletId[0], bulletPos.position, target);
        }
    }

    private IEnumerator AttackPattern2()
    {
        while (true)
        {
            yield return new WaitForSeconds(1 / (EnemyData.AttackSpeed / 2));
            // 미사일 생성
            Vector2 dir = (target.position - transform.position).normalized;

            // Todo: 오브젝트 풀에서 미사일을 생성
            BulletPoolManager.Instance.GetBulletById(EnemyData.EnemyBulletId[1], missilePos.position, target);
        }
    }

    public IEnumerator AttackPattern3()
    {
        float curChaseTime = 0f;
        float chaseTime = 2f;

        isAttacking = true;
        warningSign.gameObject.SetActive(true);

        while(curChaseTime < chaseTime)
        {
            // 여기서 캐릭터 추적
            float y = Mathf.Lerp(target.position.y, transform.position.y, chaseSpeed);
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
            // 경고 표시도 변경
            warningSign.color = Color.Lerp(originColor, warningColor, curChaseTime / chaseTime);

            curChaseTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(1.0f);
        warningSign.gameObject.SetActive(false);
        ToggelBosstEffect();

        // 돌진
        while (Mathf.Abs(originPos.x - transform.position.x) < 20) // 화면 밖 기준값은 필요에 따라 수정
        {
            transform.position += Vector3.left * dashSpeed * Time.deltaTime;
            yield return null;
        }

        // 4. 화면 밖 → 1초 대기 후 복귀
        yield return new WaitForSeconds(respawnDelay);

        // 오른쪽 끝에서 원래 자리로 이동
        Vector3 respawnPos = new Vector3(originPos.x + 20, originPos.y, originPos.z); // 오른쪽 끝 좌표는 수정 필요
        transform.position = respawnPos;

        // 복귀
        while (Vector3.Distance(transform.position, originPos) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, originPos, dashSpeed * Time.deltaTime);
            yield return null;
        }

        ToggelBosstEffect();
        pattern3 = null;
        isAttacking = false;
        yield return null;
    }

    private void ToggelBosstEffect()
    {
        if (boost1 != null)
            boost1.SetActive(!boost1.activeSelf);
        if (boost2 != null)
            boost2.SetActive(!boost2.activeSelf);
    }

    public override void LookTarget()
    {
        if(!isAttacking)
            base.LookTarget();
    }

}
