using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.AddressableAssets.Build;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class AsyncSceneManager : MonoBehaviour
{
    static AsyncSceneManager instance;
    public static AsyncSceneManager GetInstance { get { return instance; } }
    [SerializeField] private Slider slider;
    public Action OnSceneChange;
    private AsyncOperationHandle<SceneInstance> currScene;
    private void Awake()
    {
        if (instance != this&& instance != null) { Destroy(gameObject);  return; }
        DontDestroyOnLoad(gameObject);
        gameObject.SetActive(false);
        slider.wholeNumbers = true;
        instance = this;
    }
    // Start is called before the first frame update
    public void AsyncSceneLoad(string sceneName)
    {
        Scene lastScene = SceneManager.GetActiveScene();
        
        var sceneOper = Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        gameObject.SetActive(true);

        if (currScene.IsValid()) sceneOper.Completed += h => { Addressables.UnloadSceneAsync(currScene); }; 
        else sceneOper.Completed += h => { SceneManager.UnloadSceneAsync(lastScene); };

        ResourceManager.GetInstance.PreLoadAsyncAll("PreLoad", (max, curr) =>
            {
                Debug.Log($"{curr}/{max}");
                slider.maxValue = max;
                slider.value = curr;
                if (max == curr)
                {
                    if (sceneOper.IsDone)
                    {
                        SceneManager.SetActiveScene(sceneOper.Result.Scene);
                        gameObject.SetActive(false);
                    }
                    else
                    {
                        sceneOper.Completed += h =>
                        {
                            // 원하는 시점에서 전환
                            SceneManager.SetActiveScene(h.Result.Scene);
                            gameObject.SetActive(false);

                        };
                    }
                }
            });
    }
    

}
