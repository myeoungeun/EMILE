using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;
using UnityEngine.ResourceManagement.AsyncOperations;

public class EnemyPlaceManager : MonoSingleton<EnemyPlaceManager>
{
    [SerializeField] private List<EnemyData> enemyDataList;

    private Dictionary<int, ObjectPool<Enemy>> enemyDic;

    private async void Start()
    {
        enemyDic = new Dictionary<int, ObjectPool<Enemy>>();
        if(enemyDataList != null)
        {
            foreach(EnemyData enemy in enemyDataList)
            {
                GameObject enemyObj;
                var handle = Addressables.LoadAssetAsync<GameObject>(enemy.Id.ToString());
                await handle.Task;

                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    enemyObj = handle.Result;
                    ObjectPool<Enemy> pool = new ObjectPool<Enemy>(enemyObj.GetComponent<Enemy>(), 5, transform);

                    enemyDic[enemy.Id] = pool;

                    Debug.Log($"{enemy.name} 프리팹 로드 완료");
                }
                else
                {
                    Debug.LogError($"{enemy.Name} 프리팹 로드 실패");
                }
            }
        }
    }

    public void GetEnemyById(int id, Vector3 position)
    {
        Enemy enemy =  enemyDic[id]?.Get();
        enemy.transform.position = position;
    }
}
