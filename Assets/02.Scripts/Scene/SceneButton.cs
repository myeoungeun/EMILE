using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneButton : MonoBehaviour
{
    [SerializeField] string sceneName;
    public void OnButtonClick()
    {
        StopAllCoroutines();
        AsyncSceneManager.GetInstance.AsyncSceneLoad(sceneName);
    }
}
