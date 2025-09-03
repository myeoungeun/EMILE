using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoSingleton<Attack>, IAttackHandler
{
    [Header("총알 기본 정보")]
    public int baseDamage = 5;
    public float baseShotSpeed = 30f;
    public int baseShotInterval = 5;
    public int baseShotCount = -1; //무한
    
    [Header("총알 이미지")]
    private Sprite currentSprite; //현재 이미지
    public Sprite normalBulletSprite;
    public Sprite pierceBulletSprite;
    public Sprite hollowBulletSprite;
    public Sprite enemyNormalBulletSprite; //적 총알 이미지
    public Sprite homingBulletSprite; //보스 미사일 이미지

    [Header("총알 변수들")]
    [SerializeField] private int id; //기본값
    private string name;
    private int damage;
    private float shotSpeed; //총알 속도
    private int shotInterval; //발사 간격이었는데 딜레이로 수정함
    private int shotCount; //사용 가능한 총알 개수
    private bool isShooting = false;
    private float lastShotTime = 0f;
    public float shotCooldown => shotInterval * 0.05f; // shotInterval을 초 단위로 변환
    
    [Header("총알 발사 위치 등")]
    public PlayerMovement playerMovement;
    public GameObject bullet;
    public Transform bulletStart; //총알이 발사되는 위치
    [SerializeField] private AttackType currentAttackType; //공격자
    [SerializeField] private BulletType currentBulletType; //현재 장착한 탄창 종류
    
    private Dictionary<int, int> bulletRemain = new Dictionary<int, int>();
    
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) //버튼 누르고 있는 동안에
        {
            StartShooting(); //총알 발사
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            StopShooting();
        }
    }

    public void Shoot()
    {
        GameObject bulletObj = Instantiate(bullet, bulletStart.position, bulletStart.rotation); //총알 생성
        
        SpriteRenderer sr = bulletObj.GetComponent<SpriteRenderer>(); //총알 외형 변경
        if (sr != null && currentSprite != null)
        {
            sr.sprite = currentSprite;
        }
        
        bulletObj.GetComponent<Bullet>().Initialize(id, damage, shotSpeed, shotInterval, shotCount, currentAttackType, currentBulletType, playerMovement.lookDirectionRight); //데미지, 총알 크기, 방향을 Bullet에게 전달
    }
    
    public void StartShooting()
    {
        if (!isShooting)
        {
            isShooting = true;
            StartCoroutine(ShootingRoutine());
        }
    }

    public void StopShooting()
    {
        isShooting = false;
    }

    private IEnumerator ShootingRoutine()
    {
        while (isShooting)
        {
            if (Time.time - lastShotTime >= shotCooldown) //쿨타임 체크
            {
                if (shotCount != 0) //총알이 있을 때만
                {
                    Shoot();            // 총알 발사
                    if (shotCount > 0) //탄환 감소
                    {
                        shotCount--;
                    }
                    bulletRemain[id] = shotCount; //남은 탄환 기록
                    Debug.Log($"남은 탄환 : {shotCount}");
                    
                    lastShotTime = Time.time;
                }
                else Debug.Log("총알이 없습니다!");
            }
            yield return null; //매 프레임 체크
        }
    }
    
    public void SetBulletByID(int sID)
    {
        bulletRemain[id] = shotCount; //남은 탄환 저장
        id = sID; //새로운 탄창ID 설정
        OnShot(sID);
        shotCount = bulletRemain.ContainsKey(sID) ? bulletRemain[sID] : shotCount; //남은 탄환이 있으면 그거 사용하고, 없으면 기본값 사용
    }
    
    public void ResetBullets() //씬이나 스테이지 바꿀 때 호출하기
    {
        bulletRemain.Clear(); // 남은 탄환 기록 초기화
        OnShot(id);           // 현재 탄창 기본 속성 다시 세팅
    }

    public void OnShot(int sid) //총알(탄창) 정보
    {
        switch(sid)
        {
            case 501 :
                currentBulletType = BulletType.Normal;
                currentAttackType = AttackType.Player;
                name = "보통 투사체";
                currentSprite = normalBulletSprite; //이미지 바꾸는 부분. 애니메이션 이쪽에다 넣으시면 됩니다.
                damage = baseDamage;
                shotSpeed = baseShotSpeed;
                shotInterval = baseShotInterval * 2;
                shotCount = baseShotCount;
                break;
            case 502 :
                currentBulletType = BulletType.Pierce;
                currentAttackType = AttackType.Player;
                name = "관통 투사체";
                currentSprite = pierceBulletSprite;
                damage = baseDamage * 2;
                shotSpeed = baseShotSpeed + 10f;
                shotInterval = baseShotInterval;
                shotCount = 10;
                break;
            case 503 :
                currentBulletType = BulletType.Hollow;
                currentAttackType = AttackType.Player;
                name = "파열 투사체";
                currentSprite = hollowBulletSprite;
                damage = baseDamage * 2;
                shotSpeed = baseShotSpeed;
                shotInterval = baseShotInterval * 2;
                shotCount = 5;
                break;
            case 504 :
                currentBulletType = BulletType.Hollow;
                currentAttackType = AttackType.Enemy;
                name = "적 보통 투사체";
                currentSprite = enemyNormalBulletSprite;
                damage = baseDamage * 2;
                shotSpeed = baseShotSpeed;
                shotInterval = baseShotInterval;
                shotCount = baseShotCount;
                break;
            case 505 :
                currentBulletType = BulletType.Homing;
                currentAttackType = AttackType.Enemy;
                name = "보스 미사일 투사체";
                currentSprite = homingBulletSprite;
                damage = baseDamage * 5;
                shotSpeed = baseShotSpeed;
                shotInterval = baseShotInterval * 2;
                shotCount = baseShotCount;
                break;
            default:
                currentBulletType = BulletType.Normal;
                currentAttackType = AttackType.Player;
                name = "보통 투사체";
                currentSprite = normalBulletSprite; //이미지 바꾸는 부분. 애니메이션 이쪽에다 넣으시면 됩니다.
                damage = baseDamage;
                shotSpeed = baseShotSpeed;
                shotInterval = baseShotInterval * 2;
                shotCount = baseShotCount;
                break;
        }
    }
}
