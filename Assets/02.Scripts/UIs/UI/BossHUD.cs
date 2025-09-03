using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHUD : MonoBehaviour
{
    [Header("보스 정보")]
    [SerializeField] private Image bossPortrait; // 보스 초상화
    [SerializeField] private TextMeshProUGUI bossName; // 보스몬스터 이름
    [SerializeField] private Image bossHpBar; //보스몬스터 체력바

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
