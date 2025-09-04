using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BulletSlotUI : MonoBehaviour
{
    [SerializeField] private Image bulletIcon; // 탄약 이미지
    [SerializeField] private TextMeshProUGUI bulletCount; // 탄약 개수
    [SerializeField] private GameObject highlightBorder; // 선택 탄약 강조
    [SerializeField] private Image cooldown; // 탄약 쿨다운(재장전)

    private int curBulletCount; // 현재 탄약 개수
    // 슬롯 초기화
    public void Initialize()
    {
        curBulletCount = 0;
        bulletCount.text = "0";

        SetSelected(false); // 기본 선택 해제
    }

    public void SetBulletData(int startBullet)
    {
        SetBulletCount(startBullet); // 최초 시작 탄약 개수
    }

    // 선택 탄약 강조 표시
    public void SetSelected(bool isSelected)
    {
        highlightBorder.SetActive(isSelected);
    }

    // 탄약 개수 갱신
    public void SetBulletCount(int count)
    {
        curBulletCount = count;
        bulletCount.text = curBulletCount.ToString();
    }

    // 탄약 소모시 1씩 감소 - UI갱신
    public void UseBullet()
    {
        if (curBulletCount < 0) return;
        curBulletCount--;
        SetBulletCount(curBulletCount);
    }
}
