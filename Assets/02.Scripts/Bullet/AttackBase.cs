using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class AttackBase : MonoBehaviour
{
    [Header("�Ѿ� �⺻ ����")]
    public int baseDamage = 10;
    public float baseShotSpeed = 20f;
    public int baseShotInterval = 5;
    public int baseShotCount = -1; //����
    
    [Header("�Ѿ� �̹���")]
    protected Sprite currentSprite; //���� �̹���
    public Sprite normalBulletSprite;
    public Sprite pierceBulletSprite;
    public Sprite hollowBulletSprite;
    public Sprite enemyNormalBulletSprite; //�� �Ѿ� �̹���
    public Sprite homingBulletSprite; //���� �̻��� �̹���

    [Header("�Ѿ� ������")]
    [SerializeField] protected int id;
    protected string name;
    protected int damage;
    protected float shotSpeed; //�Ѿ� �ӵ�
    protected int shotInterval; //�߻� �����̾��µ� �����̷� ������
    protected int shotCount; //��� ������ �Ѿ� ����
    protected bool isShooting = false;
    protected float lastShotTime = 0f;
    public float shotCooldown => shotInterval * 0.05f; // shotInterval�� �� ������ ��ȯ
    [SerializeField] protected AttackType currentAttackType; //������
    [SerializeField] protected BulletType currentBulletType; //���� ������ źâ ����
    
    protected Dictionary<int, int> bulletRemain = new Dictionary<int, int>();
    
    public void SetBulletByID(int sID)
    {
        if (shotCount > 0) bulletRemain[id] = shotCount; //źȯ ����. ���� źȯ�� ����x
        id = sID; //���ο� źâID ����
        OnShot(sID);
        if (shotCount != -1 && bulletRemain.ContainsKey(sID)) //���� źȯ�� ������ �װ� ����ϰ�, ������ �⺻�� ���
        {
            shotCount = bulletRemain[sID];
        }
    }
    
    public void ResetBullets() //���̳� �������� �ٲ� �� ȣ���ϱ�
    {
        bulletRemain.Clear(); // ���� źȯ ��� �ʱ�ȭ
        OnShot(id);           // źâ �ٽ� ����
    }
    
    public void Shoot(GameObject bulletPrefab, Transform bulletStart, Vector3 direction)
    {
        if (shotCount == 0)
        {
            return; //�Ѿ�x
        }
        
        Debug.Log($"Shoot ȣ�� �� -> damage: {damage}");
        
        GameObject bulletObj = Instantiate(bulletPrefab, bulletStart.position, bulletStart.rotation); //�Ѿ� ����
        SpriteRenderer sr = bulletObj.GetComponent<SpriteRenderer>();
        if (sr != null && currentSprite != null) //�Ѿ� ���� ����
        {
            sr.sprite = currentSprite;
        }
        
        bulletObj.GetComponent<Bullet>().Initialize(id, damage, shotSpeed, shotInterval, shotCount, 
            currentAttackType, currentBulletType, direction.x >= 0); //������, �Ѿ� ũ��, ������ Bullet���� ����
        
        if (shotCount > 0) shotCount--; //źȯ ����
        Debug.Log($"���� źȯ {shotCount}");
        bulletRemain[id] = shotCount; //���� źȯ ���
        lastShotTime = Time.time;
    }
    
    protected abstract Vector3 GetShootDirection();
    protected abstract Transform GetBulletStart();
    protected abstract GameObject GetBulletPrefab();
    
    protected IEnumerator ShootingRoutine()
    {
        while (isShooting)
        {
            if (Time.time - lastShotTime >= shotCooldown) //��Ÿ�� üũ
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
    
    public void OnShot(int sid) //�Ѿ�(źâ) ����
    {
        switch(sid)
        {
            case 501 :
                currentBulletType = BulletType.Normal;
                currentAttackType = AttackType.Player;
                name = "���� ����ü";
                currentSprite = normalBulletSprite; //�̹��� �ٲٴ� �κ�. �ִϸ��̼� ���ʿ��� �����ø� �˴ϴ�.
                damage = baseDamage;
                shotSpeed = baseShotSpeed;
                shotInterval = baseShotInterval * 2;
                shotCount = baseShotCount;
                break;
            case 502 :
                currentBulletType = BulletType.Pierce;
                currentAttackType = AttackType.Player;
                name = "���� ����ü";
                currentSprite = pierceBulletSprite;
                damage = baseDamage * 2;
                shotSpeed = baseShotSpeed + 10f;
                shotInterval = baseShotInterval;
                shotCount = 10;
                break;
            case 503 :
                currentBulletType = BulletType.Hollow;
                currentAttackType = AttackType.Player;
                name = "�Ŀ� ����ü";
                currentSprite = hollowBulletSprite;
                damage = baseDamage * 2;
                shotSpeed = baseShotSpeed;
                shotInterval = baseShotInterval * 2;
                shotCount = 5;
                break;
            case 504 :
                currentBulletType = BulletType.Hollow;
                currentAttackType = AttackType.Enemy;
                name = "�� ���� ����ü";
                currentSprite = enemyNormalBulletSprite;
                damage = baseDamage * 2;
                shotSpeed = baseShotSpeed;
                shotInterval = baseShotInterval;
                shotCount = baseShotCount;
                break;
            case 505 :
                currentBulletType = BulletType.Homing;
                currentAttackType = AttackType.Enemy;
                name = "���� �̻��� ����ü";
                currentSprite = homingBulletSprite;
                damage = baseDamage * 5;
                shotSpeed = baseShotSpeed;
                shotInterval = baseShotInterval * 2;
                shotCount = baseShotCount;
                break;
            default:
                currentBulletType = BulletType.Normal;
                currentAttackType = AttackType.Player;
                name = "���� ����ü";
                currentSprite = normalBulletSprite; //�̹��� �ٲٴ� �κ�. �ִϸ��̼� ���ʿ��� �����ø� �˴ϴ�.
                damage = baseDamage;
                shotSpeed = baseShotSpeed;
                shotInterval = baseShotInterval * 2;
                shotCount = baseShotCount;
                break;
        }
    }
}
