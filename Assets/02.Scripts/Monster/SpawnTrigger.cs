using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    [SerializeField] private int monsterId;        // 소환할 몬스터 ID
    [SerializeField] private Transform spawnPoint; // 소환 위치

    private bool isTriggered = false; // 중복 소환 방지용

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[SpawnTrigger] 충돌 감지: {other.name}");

        if (!isTriggered && other.CompareTag("Player"))
        {
            Debug.Log("[SpawnTrigger] Player 진입 → 몬스터 소환 시작");
            isTriggered = true;

            if (EnemyPlaceManager.Instance != null)
            {
                EnemyPlaceManager.Instance.GetEnemyById(monsterId, spawnPoint.position);
                Debug.Log($"[SpawnTrigger] 몬스터(ID={monsterId}) 소환 완료");
            }
            else
            {
                Debug.LogError("[SpawnTrigger] EnemyPlaceManager 인스턴스가 없음!");
            }
        }
    }
}

