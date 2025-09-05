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
        EnemyPlaceManager.Instance.ReturnAll();
        BulletPoolManager.Instance.ReturnAll();
        UIManager.Instance.ShowUI(UIType.GameStart); // GameStart 전환
    }
}