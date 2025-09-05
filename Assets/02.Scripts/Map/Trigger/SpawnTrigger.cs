using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    [SerializeField] private int monsterId;        // 소환할 몬스터 ID
    [SerializeField] private Transform spawnPoint; // 소환 위치

    private bool isTriggered = false; // 중복 소환 방지용


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTriggered && collision.CompareTag("Player"))
        {
            isTriggered = true;

            EnemyPlaceManager.Instance.GetEnemyById(monsterId, spawnPoint.position);
        }
    }
}


