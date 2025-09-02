using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoSingleton<Attack>, IAttackHandler
{
    [Header("�Ѿ� �⺻ ����")]
    public int baseDamage = 5;
    public float baseShotSpeed = 30f;
    public int baseShotInterval = 5;
    public int baseShotCount = 99999;
    
    [Header("�Ѿ� �̹���")]
    private Sprite currentSprite; //���� �̹���
    public Sprite normalBulletSprite;
    public Sprite pierceBulletSprite;
    public Sprite hollowBulletSprite;
    public Sprite enemyNormalBulletSprite; //�� �Ѿ� �̹���
    public Sprite homingBulletSprite; //���� �̻��� �̹���

    [Header("�Ѿ� ������")]
    [SerializeField] private int id;
    private string name;
    private int damage;
    private float shotSpeed; //�Ѿ� �ӵ�
    private int shotInterval; //�߻� �����̾��µ� �����̷� ������
    private int shotCount; //��� ������ �Ѿ� ����
    private bool isShooting = false;
    private float lastShotTime = 0f;
    public float shotCooldown => shotInterval * 0.05f; // shotInterval�� �� ������ ��ȯ
    
    [Header("�Ѿ� �߻� ��ġ ��")]
    public PlayerMovement playerMovement;
    public GameObject bullet;
    public Transform bulletStart; //�Ѿ��� �߻�Ǵ� ��ġ
    [SerializeField] private AttackType currentAttackType; //������
    [SerializeField] private BulletType currentBulletType; //���� ������ źâ ����
    
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) //��ư ������ �ִ� ���ȿ�
        {
            StartShooting(); //�Ѿ� �߻�
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            StopShooting();
        }
    }

    public void Shoot()
    {
        OnShot(id);
        Debug.Log("�Ѿ� �߻�");
        GameObject bulletObj = Instantiate(bullet, bulletStart.position, bulletStart.rotation); //�Ѿ� ����
        
        SpriteRenderer sr = bulletObj.GetComponent<SpriteRenderer>(); //�Ѿ� ���� ����
        if (sr != null && currentSprite != null)
        {
            sr.sprite = currentSprite;
        }
        
        bulletObj.GetComponent<Bullet>().Initialize(id, damage, shotSpeed, shotInterval, shotCount, currentAttackType, currentBulletType, playerMovement.lookDirectionRight); //������, �Ѿ� ũ��, ������ Bullet���� ����
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
            if (Time.time - lastShotTime >= shotCooldown) //��Ÿ�� üũ
            {
                Shoot();
                lastShotTime = Time.time;
            }

            yield return null; //�� ������ üũ
        }
    }
    
    public void SetBulletByID(int sID) //�ܺο��� źâ ������ �� ���� �Լ�
    {
        id = sID;
        OnShot(id);
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
                shotInterval = baseShotInterval;
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
