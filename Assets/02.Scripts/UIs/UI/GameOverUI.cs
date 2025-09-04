using UnityEngine;
using UnityEngine.UI;
public class GameOverUI : UIBase
{
    [SerializeField] private Button exitButton;
    public override void Initialize()
    {
        exitButton.onClick.AddListener(OnExitButton);
    }

    public void OnExitButton()
    {
        UIManager.Instance.ShowUI(UIType.GameStart); // GameStart 전환
    }
}