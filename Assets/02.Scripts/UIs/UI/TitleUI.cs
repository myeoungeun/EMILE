using UnityEngine;
using UnityEngine.UI;
public class TitleUI : UIBase
{
    [Header("시작화면 오브젝트")]
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject menuPanel;

    [Header("버튼")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button optionButton;
    [SerializeField] private Button exitButton;
    public override void Initialize()
    {
        // 버튼 이벤트 추가
        startButton.onClick.AddListener(OnStartButton);
        optionButton.onClick.AddListener(OnOptionButton);
        exitButton.onClick.AddListener(OnExitButton);
    }

    public void OnStartButton() // 게임 시작
    {
        UIManager.Instance.ShowUI(UIType.InGame); // InGameUI 전환

        //TODO 튜토리얼스테이지 비동기 로드
    }

    public void OnOptionButton() // 옵션창 열기
    {
        title.SetActive(false);
        menuPanel.SetActive(false);
        UIManager.Instance.ShowPopup(PopupType.Option);
    }

    public void OnExitButton() // 나가기
    {
        Application.Quit();
    }


}