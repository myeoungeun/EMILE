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

    public void InitBullet(BulletData bullet,int index) //총알 개수 초기화
    {
        currentBullet = bullet;
        shotRemainCount = bullet.ShotMaxCount;
        
        if (bullet.BulletPrefab.TryGetComponent<Bullet>(out Bullet prefabBullet))
        {
            bulletPool[index] = new ObjectPool<Bullet>(prefabBullet, 15);
        }
        else
        {
            prefabBullet = bullet.BulletPrefab.AddComponent<Bullet>();
            prefabBullet.Initialize(bullet, (idx,bullet) => {  },index);
            bulletPool[index] = new ObjectPool<Bullet>(prefabBullet, 15);
            Debug.LogError("BulletPrefab에 Bullet 컴포넌트가 없음!");
        }
    }

    public void Init()
    {
        string[] bulletNames = { "NormalBullet501", "PierceBullet502", "HollowBullet503" };
        bulletSo = new BulletData[bulletNames.Length];
        bulletPool = new ObjectPool<Bullet>[bulletNames.Length];
        for (int i = 0; i < bulletNames.Length; i++)
        {
            int tempI = i; //클로저 이슈 대비를 위한 변수 복사
            if (ResourceManager.GetInstance.GetPreLoad.TryGetValue(bulletNames[tempI], out object loadedObject))
            {
                bulletSo[tempI] = (BulletData)loadedObject;
                InitBullet(bulletSo[tempI],tempI);
            }
            else
            {
                Debug.LogWarning("데이터 로딩 실패, 직접로드 실행");
                ResourceManager.GetInstance.LoadAsync<BulletData>(bulletNames[tempI], (result) =>
                {
                    bulletSo[tempI] = result;
                    InitBullet(bulletSo[tempI], tempI);
                }, true);
            }
        }

        
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
            b.Initialize(currentBullet,BulletEnqueue, currentBulletIndex);
        }
        else
        {
            bullet.gameObject.AddComponent<Bullet>().Initialize(currentBullet,BulletEnqueue, currentBulletIndex);
        }
        
        if (shotRemainCount > 0) shotRemainCount--; //총알 감소
        Debug.Log($"남은 총알 {shotRemainCount}");
        bulletRemain[currentBullet.Id] = shotRemainCount; //남은 개수 기록
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
}
