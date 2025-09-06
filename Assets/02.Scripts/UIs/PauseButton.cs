using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class PauseButton : UIBase
{
    [SerializeField] private Button pauseButton;
    [SerializeField] private Image buttonIcon;

    [SerializeField] private Sprite pauseIcon;
    [SerializeField] private Sprite resumeIcon;

    private bool isPaused;
    public override void Initialize()
    {
        buttonIcon.sprite = pauseIcon;
        pauseButton.onClick.AddListener(TooglePause);
    }


    public void TooglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            UIManager.Instance.ShowPopup(PopupType.Option);
        }
        else
        {
            Time.timeScale = 1f;
            UIManager.Instance.ClosePopup(PopupType.Option);
            UIManager.Instance.ClosePopup(PopupType.KeyMapping);

        }
        UpdateIcon();

    }
    private void UpdateIcon()
    {
        buttonIcon.sprite = isPaused ? resumeIcon : pauseIcon;
    }
}