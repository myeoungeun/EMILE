using UnityEngine;
using UnityEngine.UI;

public class OptionPopup : PopupBase
{
    [Header("사운드")]
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    [Header("버튼")]
    [SerializeField] private Button keyMappingButton;
    [SerializeField] private Button exitButton;
    public override void Initialize()
    {
        SetSlider();
        keyMappingButton.onClick.AddListener(OnKeyMappingButton);
        exitButton.onClick.AddListener(OnExitButton);
    }

    protected override void OnDisable()
    {
        UIManager.Instance.InGameUI.PauseButton.TooglePause();
    }

    public void SetSlider()
    {
        //TODO 슬라이더 초기값 설정
    }

    public void OnKeyMappingButton() // 키매핑 팝업 열기
    {

        UIManager.Instance.ClosePopup(PopupType.Option); // 옵션 팝업UI 닫고
        UIManager.Instance.ShowPopup(PopupType.KeyMapping); // 키설정 팝업UI 열기
    }

    public void OnExitButton() // 돌아가기
    {
        UIManager.Instance.ClosePopup(PopupType.Option);
        UIManager.Instance.GameStartUI.ShowTitleMenu(true);
    }
}
