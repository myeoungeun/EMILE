using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private TitleUI titleUI;
    [SerializeField] private InGameUI inGameUI;
    [SerializeField] private GameOverUI gameOverUI;

    public TitleUI TitleUI => titleUI;
    public InGameUI InGameUI => inGameUI;
    public GameOverUI GameOverUI => gameOverUI;

    public void Initialize() // TODO 초기화해야할 함수 추가
    {
        titleUI.gameObject.SetActive(true);
        inGameUI.gameObject.SetActive(false);
        gameOverUI.gameObject.SetActive(false);
    }

    public void ShowTitleUI()
    {

    }

    public void ShowInGameUI()
    {

    }

    public void ShowGameOverUI()
    {

    }
}