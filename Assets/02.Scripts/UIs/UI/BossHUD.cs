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

    private BossEnemy bossEnemy;

    public override void Initialize()
    {
        bossPortrait.sprite = null;
        bossName.text = "???";
        bossHpBar.fillAmount = 1f;
    }

    public void SetBossData(BossEnemy boss) // 보스 정보 연결 
    {
        bossEnemy = boss;
        bossName.text = boss.EnemyData.Name;
        //bossPortrait.sprite = null; // 보스 이미지 추가안되어있음
        bossHpBar.fillAmount = 1f; // 시작때는 체력 100%

        boss.onHpChanged -= UpdateBossHp;
        boss.onHpChanged += UpdateBossHp;
    }

    public void UpdateBossHp()
    {
        bossHpBar.fillAmount = (float)bossEnemy.CurHp / bossEnemy.EnemyData.MaxHp;
    }
}
