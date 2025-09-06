using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneButton : MonoBehaviour
{
    [SerializeField] string sceneName;
    [SerializeField] bool isRetryButton = false;
    public void OnButtonClick()
    {
        StopAllCoroutines();

        if (isRetryButton)
        {
            string retryScene = UIManager.Instance.CurrentStageName;
            if (!string.IsNullOrEmpty(retryScene))
            {
                Time.timeScale = 1f;
                EnemyPlaceManager.Instance.ReturnAll();
                BulletPoolManager.Instance.ReturnAll();
                UIManager.Instance.GameOverUI.Close();
                AsyncSceneManager.GetInstance.AsyncSceneLoad(retryScene);
            }
        }
        else
        {
            AsyncSceneManager.GetInstance.AsyncSceneLoad(sceneName);
        }
    }
}
