using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHUD : UIBase
{
    [Header("보스 정보")]
    [SerializeField] private Image bossPortrait; // 보스 초상화
    [SerializeField] private TextMeshProUGUI bossName; // 보스몬스터 이름
    [SerializeField] private Image bossHpBar; //보스몬스터 체력바

    [SerializeField] private BossEnemy boss; // Inspector에서 드래그로 할당

    public override void Initialize()
    {
        InitBossData(boss); // Scene에 있는 동일 객체
    }

    public void InitBossData(BossEnemy boss) // 보스 정보 할당
    {
        this.boss = boss;
        bossName.text = boss.EnemyData.Name;
        bossPortrait.sprite = null; 
        bossHpBar.fillAmount = 1f; // 시작때는 체력 100%

        boss.onHpChanged -= UpdateBossHp;
        boss.onHpChanged += UpdateBossHp;
    }

    public void UpdateBossHp()
    {
        bossHpBar.fillAmount = (float)boss.CurHp / boss.EnemyData.MaxHp;
    }
}
