using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : UIBase
{
    [Header("플레이어 정보")]
    [SerializeField] private Image portrait; // 플레이어 초상화
    [SerializeField] private TextMeshProUGUI playerStatus; // 플레이어 이름 + 목숨 갯수
    [SerializeField] private Image hpBar; // 플레이어 체력바

    [Header("탄약 슬롯")]
    [SerializeField] private BulletSlotUI[] bulletSlots; // 탄약 슬롯(3개)

    private int curBulletIndex = 0; // 현재 선택된 탄약 슬롯 인덱스

    private PlayerStat playerStat;
    public override void Initialize()
    {
        hpBar.fillAmount = 1f;
        playerStatus.text = "Player X 2";
        InitSlots();
    }

    public void SetPlayer(Player player)
    {
        //TODO: PlayerStat과 연결
        //playerStat = player.Stat;

        // TODO: PlayerStat에서 이벤트 구독
        //playerStat.OnHPChanged += UpdateHP;
        //playerStat.OnLifeChanged += UpdateLife;
        //playerStat.OnBulletCountChanged += UpdateBulletCount;
        //playerStat.OnBulletSlotChanged += ChangeBulletSlot;

        // 초기값 반영
        //UpdateHP(playerStat.CurHP, playerStat.MaxHP);
        //UpdateLife(playerStat.Life);

        //for (int i = 0; i < bulletSlots.Length; i++)
        //{
        //    UpdateBulletCount(i, playerStat.GetBulletCount(i));
        //}

        //ChangeBulletSlot(playerStat.CurrentBulletIndex);
    }
    public void InitSlots() // 슬롯 초기화
    {
        for (int i = 0; i < bulletSlots.Length; i++)
        {
            bulletSlots[i].Initialize();
        }

        // 처음 기본탄 강조
        curBulletIndex = 0;
        bulletSlots[curBulletIndex].SetSelected(true);
    }

    public void ChangeBulletSlot(int newIndex) // 탄약 전환 기능 이벤트 연결해서 구독
    {
        if (newIndex < 0 || newIndex >= bulletSlots.Length) return;

        bulletSlots[curBulletIndex].SetSelected(false);
        curBulletIndex = newIndex;
        bulletSlots[curBulletIndex].SetSelected(true);
    }

    //  슬롯 탄약 개수 업데이트
    public void UpdateBulletCount(int slotIndex, int count)
    {
        if (slotIndex < 0 || slotIndex >= bulletSlots.Length) return;

        bulletSlots[slotIndex].SetBulletCount(count);
    }

    // 현재 슬롯 탄약 사용 업데이트
    public void UseCurBullet()
    {
        bulletSlots[curBulletIndex].UseBullet();
    }

    // 체력 업데이트
    public void UpdateHP(int curHp, int maxHp)
    {
        hpBar.fillAmount = Mathf.Clamp01((float)curHp / maxHp);
    }

    // 플레이어 목숨 업데이트
    public void UpdateLife(int life)
    {
        playerStatus.text = $"Player X {life}";
    }
}
