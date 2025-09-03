using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class BulletPoolManager : MonoSingleton<BulletPoolManager>
{
    [SerializeField] private List<BulletData> bulletDataList;

    private Dictionary<int, ObjectPool<BaseBullet>> bulletDic;

    private async void Start()
    {
        bulletDic = new Dictionary<int, ObjectPool<BaseBullet>>();
        if (bulletDataList != null )
        {
            foreach (BulletData bullet in bulletDataList)
            {
                GameObject bulletPrefab;
                var handle = Addressables.LoadAssetAsync<GameObject>(bullet.Id.ToString());
                await handle.Task;

                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    bulletPrefab = handle.Result;
                    ObjectPool<BaseBullet> pool = new ObjectPool<BaseBullet>(bulletPrefab.GetComponent<BaseBullet>(), 10, transform);

                    bulletDic[bullet.Id] = pool;

                    Debug.Log($"{bullet.Name} 프리팹 로드 완료");
                }
                else
                {
                    Debug.LogError($"{bullet.Name} 프리팹 로드 실패");
                }
            }
        }
    }

    public void GetBulletById(int id, Vector3 position, Transform target)
    {
        BaseBullet bullet = bulletDic[id].Get();
        bullet.transform.position = position;
        bullet.Init(target);
    }

    public void ReturnBullet(BaseBullet bullet)
    {
        bulletDic[bullet.BulletData.Id].Return(bullet);
    }
}
