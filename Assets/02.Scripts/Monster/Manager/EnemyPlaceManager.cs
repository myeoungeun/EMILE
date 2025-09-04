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

                    Debug.Log($"{enemy.Name} 프리팹 로드 완료");
                }
                else
                {
                    Debug.LogError($"{enemy.Name} 프리팹 로드 실패");
                }
            }
        }
    }

    /// <summary>
    /// 해당 인스턴스에 몬스터 아이디와 몬스터를 배치할 위치를 인자로 넘기면 해당 위치에 몬스터가 소환됨
    /// </summary>
    /// <param name="id">소환하고자 하는 몬스터 아이디</param>
    /// <param name="position">몬스터 생성 위치</param>
    public void GetEnemyById(int id, Vector3 position)
    {
        Enemy enemy =  enemyDic[id]?.Get();
        enemy.transform.position = position;
    }

    /// <summary>
    /// 에너미를 풀로 회수시킴
    /// </summary>
    /// <param name="enemy"></param>
    public void Return(Enemy enemy)
    {
        enemyDic[enemy.EnemyData.Id].Return(enemy);
    }
}
