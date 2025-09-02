using UnityEngine;
using UnityEngine.UI;
public class TitleUI : MonoBehaviour
{
    [Header("버튼")]
    [SerializeField] Button gameStart;
    [SerializeField] Button option;
    [SerializeField] Button exit;

    public void Initialize()
    {

    }

    public void OnGameStart() // 게임 시작
    {
        //ToDo InGameUI 활성화 + 튜토리얼 씬 로드(Addressable)
    }

    public void OnOption() // 옵션창 열기
    {

    }

    public void OnExit() // 나가기
    {
        Application.Quit();
    }
}