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

    public void InitBossData(EnemyData bossData, BossEnemy bossEnemy) // 보스 정보 할당
    {
        bossName.text = bossData.Name;
        bossPortrait.sprite = null; // TODO: SO에 스프라이트 정보 추가 요청
        //bossPortrait.sprite = bossData.Sprite;
        bossHpBar.fillAmount = 1f; // 시작때는 체력 100%

        bossEnemy.OnHpChanged += UpdateBossHp; // 이벤트 구독
    }

    public void UpdateBossHp(float bossHp, float bossMaxHp)
    {
        bossHpBar.fillAmount = Mathf.Clamp01(bossHp / bossMaxHp);
    }

}
