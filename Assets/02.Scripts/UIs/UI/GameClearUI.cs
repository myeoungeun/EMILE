using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClaerUI : UIBase
{
    [SerializeField] private Button nextButton;
    [SerializeField] private Button exitButton;
    public override void Initialize()
    {
        nextButton.onClick.AddListener(OnNextButton);
        exitButton.onClick.AddListener(OnExitButton);
    }

    public void OnNextButton()
    {
        EnemyPlaceManager.Instance.ReturnAll();
        BulletPoolManager.Instance.ReturnAll();

        Time.timeScale = 1;
        //TODO 다음 스테이지 이동
        Debug.Log("다음 스테이지로 이동합니다");
        // 비동기 씬로드로 2번째 맵으로 이동
        // nextButton에 Scene button 스크립트 붙이고 이동할 씬 이름 작성하면 끝
        // 임시로 게임 종료 넣어놓겠음
    }

    public void OnExitButton()
    {
        Time.timeScale = 1;
        UIManager.Instance.GameOverUI.OnExitButton();
    }
}
