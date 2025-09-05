using System.Collections.Generic;
using UnityEngine;

public enum UIType
{
    GameStart,
    InGame,
    GameOver,
}

public enum PopupType
{
    Option,
    KeyMapping,
}

public class UIManager : MonoSingleton<UIManager>
{
    [Header("UI")]
    [SerializeField] private GameStartUI gameStartUI;
    [SerializeField] private InGameUI inGameUI;
    [SerializeField] private GameOverUI gameOverUI;

    public GameStartUI GameStartUI => gameStartUI;
    public InGameUI InGameUI => inGameUI;
    public GameOverUI GameOverUI => gameOverUI;

    [Header("Popup")]
    [SerializeField] private OptionPopup optionPopup;
    [SerializeField] private KeyMappingPopup keyMappingPopup;

    private Dictionary<UIType, UIBase> uiDictionary; // 일반UI 관리
    private Dictionary<PopupType, PopupBase> popupDictionary; // 팝업UI 관리

    protected override void Awake()
    {
        base.Awake();
        InitUIManager();

        ShowUI(UIType.GameStart); // 시작화면UI만 활성화

        foreach (var popup in popupDictionary.Values)
        {
            popup.Close(); // 팝업UI 비활성화
        }
    }

    #region 초기화
    private void InitUIManager()
    {
        //일반 UI 딕셔너리 초기화
        uiDictionary = new Dictionary<UIType, UIBase>
        {
            { UIType.GameStart, gameStartUI },
            { UIType.InGame, inGameUI },
            { UIType.GameOver, gameOverUI }
        };

        //팝업 UI 딕셔너리 초기화
        popupDictionary = new Dictionary<PopupType, PopupBase>
        {
            {PopupType.Option, optionPopup},
            {PopupType.KeyMapping, keyMappingPopup}
        };

        //모든 일반 UI 초기화
        foreach (var ui in uiDictionary.Values)
        {
            ui.Initialize();
        }

        //모든 팝업 UI 초기화
        foreach (var popup in popupDictionary.Values)
        {
            popup.Initialize();
        }
    }
    #endregion

    #region UI 제어

    public void ShowUI(UIType type)
    {
        foreach (var ui in uiDictionary.Values)
        {
            ui.Close(); // 모든 일반UI 닫기
        }
        uiDictionary[type].Open(); // 요청한 일반UI 열기
    }

    #endregion 

    #region Popup 제어

    public void ShowPopup(PopupType type) => popupDictionary[type].Open(); // 요청한 팝업UI 열기
    public void ClosePopup(PopupType type) => popupDictionary[type].Close(); // 요청한 팝업UI 닫기

    #endregion
}