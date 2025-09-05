using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class AttackBase
{
    [Header("총알")]
    public BulletData[] bulletSo;
    public BulletData currentBullet; //현재 장전
    protected int shotRemainCount; //남은 총알
    protected int currentBulletIndex = 0;

    protected ObjectPool<Bullet>[] bulletPool;

    protected Dictionary<int, int> bulletRemain = new Dictionary<int, int>(); //남은 총알 개수 기록용

    public event Action<int> OnBulletSlotChanged;       // 현재 탄약 슬롯 인덱스
    public event Action<int, int> OnBulletCountChanged; // 탄약 슬롯 인덱스, 남은 탄 수

    public int CurrentBulletIndex => currentBulletIndex; // PlayerHUD 접근용 프로퍼티, 현재 탄약 슬롯 인덱스 확인용


    public void InitBullet(BulletData bullet, int index) //총알 개수 초기화
    {
        currentBullet = bullet;
        shotRemainCount = bullet.ShotMaxCount;

        if (bullet.BulletPrefab.TryGetComponent<Bullet>(out Bullet prefabBullet))
        {
            bulletPool[index] = new ObjectPool<Bullet>(prefabBullet, 15, BulletPoolManager.Instance.transform);
        }
        else
        {
            prefabBullet = bullet.BulletPrefab.AddComponent<Bullet>();
            prefabBullet.Initialize(bullet, (idx, bullet) => { }, index);
            bulletPool[index] = new ObjectPool<Bullet>(prefabBullet, 15);
            Debug.LogError("BulletPrefab에 Bullet 컴포넌트가 없음!");
        }
    }

    public void Init()
    {
        string[] bulletNames = { "NormalBullet501", "PierceBullet502", "HollowBullet503" };
        bulletSo = new BulletData[bulletNames.Length];
        bulletPool = new ObjectPool<Bullet>[bulletNames.Length];
        int loadedCount = 0;

        for (int i = 0; i < bulletNames.Length; i++)
        {
            int tempI = i; //클로저 이슈 대비를 위한 변수 복사
            if (ResourceManager.GetInstance.GetPreLoad.TryGetValue(bulletNames[tempI], out object loadedObject))
            {
                bulletSo[tempI] = (BulletData)loadedObject;
                InitBullet(bulletSo[tempI], tempI);
                loadedCount++;

                if (loadedCount == bulletNames.Length) // 모든 총알 로딩 완료 시 초기 총알 설정
                    SetInitialBullet();
            }
            else
            {
                Debug.LogWarning("데이터 로딩 실패, 직접로드 실행");
                ResourceManager.GetInstance.LoadAsync<BulletData>(bulletNames[tempI], (result) =>
                {
                    bulletSo[tempI] = result;
                    InitBullet(bulletSo[tempI], tempI);
                    loadedCount++;

                    if (loadedCount == bulletNames.Length){
                        SetInitialBullet();
                    }
                }, true);
            }
        }

        
    }

    private void SetInitialBullet()
    {
        currentBulletIndex = 0; // 501번
        currentBullet = bulletSo[currentBulletIndex];
        
        // 현재 슬롯의 탄 수 초기화
        shotRemainCount = currentBullet.ShotMaxCount;
        bulletRemain[currentBulletIndex] = shotRemainCount;

        // 현재 슬롯 변경 이벤트 호출
        OnBulletSlotChanged?.Invoke(currentBulletIndex);

        // 모든 슬롯 탄 수 이벤트 호출
        for (int i = 0; i < bulletSo.Length; i++)
        {
            int count;
            if (!bulletRemain.TryGetValue(i, out count))
            {
                count = bulletSo[i].ShotMaxCount;
                bulletRemain[i] = count;
            }

            OnBulletCountChanged?.Invoke(i, count);
        }
    }

    public void SetBulletByID(int sID)
    {
        if (shotRemainCount > 0 && currentBullet != null) //총알 남은 탄 저장
        {
            bulletRemain[currentBullet.Id] = shotRemainCount; //총알을 바꾸기 전, 남은 발사 횟수 기록함
        }

        BulletData newBullet = GetBulletDataID(sID); //총알 교체
        if (newBullet == null)
        {
            Debug.LogWarning($"총알 ID {sID}를 찾을 수 없습니다!");
            return;
        }

        currentBullet = newBullet;

        currentBulletIndex = Array.IndexOf(bulletSo, currentBullet); // 교체된 총알 인덱스 갱신

        if (!bulletRemain.TryGetValue(sID, out shotRemainCount)) //교체한 총알 남은 탄 수 가져오기
        {
            shotRemainCount = newBullet.ShotMaxCount;
            bulletRemain[sID] = shotRemainCount; // 총알 없으면 바로 기록
        }

        Debug.Log($"총알 교체됨 → ID={newBullet.Id}, 남은 탄={shotRemainCount}");

        // 탄약 교체, 갯수 이벤트 실행
        bulletRemain[currentBullet.Id] = shotRemainCount;
        OnBulletSlotChanged?.Invoke(currentBulletIndex);
        OnBulletCountChanged?.Invoke(currentBulletIndex, shotRemainCount);
    }

    public void Shoot(Vector3 position, Quaternion rotation)
    {
        if (shotRemainCount == 0)
        {
            Debug.Log("총알이 없습니다!");
            return;
        }

        Bullet bullet = bulletPool[currentBulletIndex].Get();
        bullet.transform.position = position;
        bullet.transform.rotation = rotation;
        if (bullet.TryGetComponent<Bullet>(out Bullet b))
        {
            b.Initialize(currentBullet, BulletEnqueue, currentBulletIndex);
        }
        else
        {
            bullet.gameObject.AddComponent<Bullet>().Initialize(currentBullet, BulletEnqueue, currentBulletIndex);
        }

        if (shotRemainCount != -1 && shotRemainCount > 0) shotRemainCount--; //총알 감소
        Debug.Log($"남은 총알 {shotRemainCount}");
        bulletRemain[currentBullet.Id] = shotRemainCount; //남은 개수 기록

        //탄수 변경 이벤트 호출
        OnBulletCountChanged?.Invoke(currentBulletIndex, shotRemainCount);
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
    private void BulletEnqueue(int index, Bullet obj)
    {
        bulletPool[index].Return(obj);
    }

    // 현재 슬롯별 남은 탄 수를 반환하는 메서드
    public int GetRemainCount(int slotIndex)
    {
        if (bulletSo == null || slotIndex < 0 || slotIndex >= bulletSo.Length)
            return 0;

        if (bulletSo[slotIndex] == null)
            return 0;

        return bulletRemain.TryGetValue(bulletSo[slotIndex].Id, out int count) ? count : 0;
    }

}
