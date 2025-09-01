using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class MapManager : MonoBehaviour
{
    [Header("�÷��̾�� ���� ����")]
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float sectorWidth = 26f;
    [SerializeField]
    private int preloadSectors = 1; // ���� ���� �� �ķ� �� �� ���͸� �̸� �ε��� �� ����

    // �ε��ؾ��� ���� ����
    private Dictionary<int, AsyncOperationHandle<GameObject>> loadedSectors = new Dictionary<int, AsyncOperationHandle<GameObject>>();

    private int currentSector = -1;

    private void Update()
    {
        int _newSector = Mathf.FloorToInt(player.position.x / sectorWidth);

        if(_newSector != currentSector)
        {
            currentSector = _newSector;
            UpdateSectors();
        }    
    }

    private void UpdateSectors()
    {
        // 1. �ε��ؾ��� ���� ����Ʈ �߰�
        List<int> sectorsToLoad = new List<int>();
        for (int i = currentSector - preloadSectors; i <= currentSector + preloadSectors; i++)
        {
            if (i < 0) continue;
            sectorsToLoad.Add(i);
        }

        // 2. �ε�� ���� �� �ʿ���� ���� Release
        List<int> keys = new List<int>(loadedSectors.Keys);
        foreach(int key in keys)
        {
            if(!sectorsToLoad.Contains(key))
            {
                Addressables.Release(loadedSectors[key]);
                loadedSectors.Remove(key);
            }
        }

        // 3. �ʿ��� ���� �ε�
        foreach (int sectorIndex in sectorsToLoad)
        {
            if (!loadedSectors.ContainsKey(sectorIndex))
            {
                string key = $"Sector_{sectorIndex}";
                AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(key, new Vector3(sectorIndex * sectorWidth,0,0),Quaternion.identity);
                loadedSectors.Add(sectorIndex, handle);
            }
        }
    }
}
