using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : UIBase
{
    [Header("HUD")]
    [SerializeField] private PlayerHUD playerHUD;
    [SerializeField] private BossHUD bossHUD;

    public PlayerHUD PlayerHUD => playerHUD;
    public BossHUD BossHUD => bossHUD;

    public override void Initialize()
    {
        playerHUD.Initialize();
        bossHUD.Initialize();

        playerHUD.Open(); // 항상 켜짐
        bossHUD.Close(); // 보스전 전까지 비활성
    }
}