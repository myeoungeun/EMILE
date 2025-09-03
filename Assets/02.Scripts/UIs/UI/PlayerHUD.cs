using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [Header("플레이어 정보")]
    [SerializeField] private Image portrait; // 플레이어 초상화
    [SerializeField] private TextMeshProUGUI playerName; // 플레이어 이름
    [SerializeField] private Image hpBar; // 플레이어 체력바
    // 총알을 슬롯형태로 만들어야하나?

    public void SetPlayerInfo(Sprite portraitSprite, string name, int life, float Hp, float maxHp)
    {
        // TODO : 캐릭터 초상화를 Player 관련 스크립트에서 전달 필요
        portrait.sprite = portraitSprite;
        playerName.text = name;
        hpBar.fillAmount = Hp / maxHp;
    }
}
