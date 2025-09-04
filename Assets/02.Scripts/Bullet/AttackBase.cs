using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class AttackBase : MonoBehaviour
{
    [Header("총알")] public BulletData[] bulletSo;
    public BulletData currentBullet; //현재 장전
    protected int shotRemainCount; //남은 총알
    
    
    protected bool isShooting = false;
    protected float lastShotTime = 0f;
    public float shotCooldown => currentBullet.ShotInterval * 0.05f;
    
    protected Dictionary<int, int> bulletRemain = new Dictionary<int, int>(); //남은 총알 개수 기록용

    public void InitBullet(BulletData bullet) //총알 개수 초기화
    {
        currentBullet = bullet;
        shotRemainCount = bullet.ShotMaxCount;
    }

    void Start()
    {
        InitBullet(bulletSo[0]);
    }

    public void SetBulletByID(int sID)
    {
        if (shotRemainCount > 0 && currentBullet != null) //총알 남은 탄 저장
        {
            bulletRemain[currentBullet.Id] = shotRemainCount; //총알을 바꾸기 전, 남은 발사 횟수 기록함
        }
        
        if (shotRemainCount != -1 && bulletRemain.ContainsKey(sID)) //무한 총알 아닌 경우, 선택하려는 총알 ID가 딕셔너리에 기록되어 있는 경우
        {
            shotRemainCount = bulletRemain[sID]; //이전에 사용했던 총알이면 남은 발사 횟수 이어서 사용
        }
    }
    
    public void Shoot(Transform bulletStart)
    {
        if (shotRemainCount == 0)
        {
            Debug.Log("총알이 없습니다!");
            return;
        }
        
        GameObject bulletObj = Instantiate(currentBullet.BulletPrefab, bulletStart.position, bulletStart.rotation); //총알 생성
        bulletObj.GetComponent<Bullet>().Initialize(currentBullet);
        
        if (shotRemainCount > 0) shotRemainCount--; //총알 감소
        Debug.Log($"남은 총알 {shotRemainCount}");
        bulletRemain[currentBullet.Id] = shotRemainCount; //남은 개수 기록
        lastShotTime = Time.time; //발사 시간 갱신(연속 발사 간격 계산용)
    }
    
    public IEnumerator ShootingRoutine(Transform bulletStart)
    {
        while (isShooting)
        {
            if (Time.time - lastShotTime >= shotCooldown)
            {
                Shoot(bulletStart);
            }
            yield return null;
        }
    }
    
    public void StartShooting(Transform bulletStart)
    {
        if (!isShooting)
        {
            isShooting = true;
            StartCoroutine(ShootingRoutine(bulletStart));
        }
    }
    
    public void StopShooting()
    {
        isShooting = false;
    }
    
    public BulletData GetBulletDataID(int id)
    {
        foreach (var bullet in bulletSo) //배열에서 id찾기
        {
            BulletData bData = bullet as BulletData;
            if (bData != null && bData.Id == id)
                return bData;
        }
        Debug.LogWarning($"BulletData ID {id}를 찾을 수 없음!");
        return null;
    }
}
