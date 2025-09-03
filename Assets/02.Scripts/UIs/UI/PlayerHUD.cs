using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [Header("플레이어 정보")]
    [SerializeField] private Image portrait; // 플레이어 초상화
    [SerializeField] private TextMeshProUGUI playerStatus; // 플레이어 이름
    [SerializeField] private Image hpBar; // 플레이어 체력바

    [Header("탄약 슬롯")]
    [SerializeField] private BulletSlotUI[] bulletSlots; // 탄약 슬롯

    private Dictionary<BulletType, BulletSlotUI> slots = new Dictionary<BulletType, BulletSlotUI>();

    private int selectedBulletIndex = 0; // 현재 선택된 탄약 슬롯 인덱스


    private BulletType[] playerBulletType = new BulletType[]
    {
        BulletType.Normal,
        BulletType.Pierce,
        BulletType.Hollow,
    };

    public void InitSlots()
    {
        for(int i = 0; i < playerBulletType.Length; i++)
        {
            bulletSlots[i].Initialize();
            slots[playerBulletType[i]] = bulletSlots[i];
        }
    }

    // TODO : 플레이어 정보를 받아와야함
    public void SetPlayerInfo(Sprite portraitSprite, string name, int life, float hp, float maxHp)
    {
        portrait.sprite = portraitSprite;
        playerStatus.text = $"{name} X {life}";
        hpBar.fillAmount = hp / maxHp; // 플레이어 현재 hp / 최대 hp
    }

    // TODO : 선택된 탄약 슬롯 강조 메서드
    public void HighlightEquipBullet(BulletType type)
    {
        foreach(var bullet in slots)
        {
            bullet.Value.SetSelected(bullet.Key == type);
        }

        selectedBulletIndex = Array.IndexOf(playerBulletType, type);
    }

    // TODO : 선택된 탄약 슬롯의 탄약 개수 갱신 메서드
    public void UpdateEquipBullet(BulletType type, int bulletCount)
    {
        if(slots.ContainsKey(type))
        {
            slots[type].SetBulletCount(bulletCount);
        }
    }

    // TODO : 선택된 탄약 사용(소비) 표시 메서드
    public void UseEquipBullet()
    {
        bulletSlots[selectedBulletIndex].UseBullet();
    }

    // 선택한 탄약 슬롯 인덱스 반환
    public int GetEquipBulletIndex() => selectedBulletIndex;
}
