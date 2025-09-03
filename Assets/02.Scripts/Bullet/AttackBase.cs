using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class AttackBase : MonoBehaviour
{
    [Header("총알 기본 정보")]
    public int baseDamage = 10;
    public float baseShotSpeed = 20f;
    public int baseShotInterval = 5;
    public int baseShotCount = -1; //무한
    
    [Header("총알 이미지")]
    protected Sprite currentSprite; //현재 이미지
    public Sprite normalBulletSprite;
    public Sprite pierceBulletSprite;
    public Sprite hollowBulletSprite;
    public Sprite enemyNormalBulletSprite; //적 총알 이미지
    public Sprite homingBulletSprite; //보스 미사일 이미지

    [Header("총알 변수들")]
    [SerializeField] protected int id;
    protected string name;
    protected int damage;
    protected float shotSpeed; //총알 속도
    protected int shotInterval; //발사 간격이었는데 딜레이로 수정함
    protected int shotCount; //사용 가능한 총알 개수
    protected bool isShooting = false;
    protected float lastShotTime = 0f;
    public float shotCooldown => shotInterval * 0.05f; // shotInterval을 초 단위로 변환
    [SerializeField] protected AttackType currentAttackType; //공격자
    [SerializeField] protected BulletType currentBulletType; //현재 장착한 탄창 종류
    
    protected Dictionary<int, int> bulletRemain = new Dictionary<int, int>();
    
    public void SetBulletByID(int sID)
    {
        if (shotCount > 0) bulletRemain[id] = shotCount; //탄환 저장. 무한 탄환은 저장x
        id = sID; //새로운 탄창ID 설정
        OnShot(sID);
        if (shotCount != -1 && bulletRemain.ContainsKey(sID)) //남은 탄환이 있으면 그거 사용하고, 없으면 기본값 사용
        {
            shotCount = bulletRemain[sID];
        }
    }
    
    public void ResetBullets() //씬이나 스테이지 바꿀 때 호출하기
    {
        bulletRemain.Clear(); // 남은 탄환 기록 초기화
        OnShot(id);           // 탄창 다시 세팅
    }
    
    public void Shoot(GameObject bulletPrefab, Transform bulletStart, Vector3 direction)
    {
        if (shotCount == 0)
        {
            return; //총알x
        }
        
        Debug.Log($"Shoot 호출 전 -> damage: {damage}");
        
        GameObject bulletObj = Instantiate(bulletPrefab, bulletStart.position, bulletStart.rotation); //총알 생성
        SpriteRenderer sr = bulletObj.GetComponent<SpriteRenderer>();
        if (sr != null && currentSprite != null) //총알 외형 변경
        {
            sr.sprite = currentSprite;
        }
        
        bulletObj.GetComponent<Bullet>().Initialize(id, damage, shotSpeed, shotInterval, shotCount, 
            currentAttackType, currentBulletType, direction.x >= 0); //데미지, 총알 크기, 방향을 Bullet에게 전달
        
        if (shotCount > 0) shotCount--; //탄환 감수
        Debug.Log($"남은 탄환 {shotCount}");
        bulletRemain[id] = shotCount; //남은 탄환 기록
        lastShotTime = Time.time;
    }
    
    protected abstract Vector3 GetShootDirection();
    protected abstract Transform GetBulletStart();
    protected abstract GameObject GetBulletPrefab();
    
    protected IEnumerator ShootingRoutine()
    {
        while (isShooting)
        {
            if (Time.time - lastShotTime >= shotCooldown) //쿨타임 체크
            {
                Shoot(GetBulletPrefab(), GetBulletStart(), GetShootDirection());
            }
            yield return null;
        }
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
