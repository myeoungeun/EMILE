using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    public GameObject menuPannel;

    public void Awake()
    {
        menuPannel = GetComponentInChildren<RectTransform>().gameObject;
    }

    public void Stop()
    {
        Time.timeScale = 0; //시간 0으로 해서 멈춤
        menuPannel.SetActive(true); //옵션창 띄우기
    }

    public void Continue()
    {
        menuPannel.SetActive(false);
        Time.timeScale = 1f;
    }
    
    public void SceneExit()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}
