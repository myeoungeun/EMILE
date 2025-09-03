using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlaceManager : MonoSingleton<EnemyPlaceManager>
{
    [SerializeField] private List<EnemyData> enemyDataList;
    // 이 부분 어드레서블로 바꾸면 될 듯
    [SerializeField] private List<GameObject> enemyPrefabs;
    private Dictionary<int, ObjectPool<Enemy>> enemyDic;

    private void Start()
    {
        if(enemyDataList != null && enemyPrefabs != null)
        {
            
        }
    }

    public void GetEnemyById(int id, Vector3 position)
    {
        Enemy enemy =  enemyDic[id].Get();
        enemy.transform.position = position;
    }
}
