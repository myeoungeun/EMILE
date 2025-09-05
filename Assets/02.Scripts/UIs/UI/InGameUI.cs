using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : UIBase
{
    [Header("HUD")]
    [SerializeField] private PlayerHUD playerHUD; // 플레이어 HUD
    [SerializeField] private BossHUD bossHUD; // 보스에너미 HUD
    [SerializeField] private UITextHUD uiTextHUD; // 튜토리얼 텍스트 HUD

    public PlayerHUD PlayerHUD => playerHUD;
    public BossHUD BossHUD => bossHUD;
    public UITextHUD UITextHUD => uiTextHUD;


    public override void Initialize()
    {
        playerHUD.Initialize();
        bossHUD.Initialize();
        uiTextHUD.Initialize();

        playerHUD.Open(); // 항상 켜짐
        bossHUD.Close(); // 보스전 전까지 비활성
        uiTextHUD.Open(); // 켜두고 안에 내용만 비워둠
    }
}