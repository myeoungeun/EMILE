using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionPopup : PopupBase
{
    [Header("사운드")]
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    [Header("버튼")]
    [SerializeField] private Button keyMappingBtn;
    [SerializeField] private Button exitBtn;
    public override void Initialize()
    {
        SetSlider();


    }

    public void SetSlider()
    {
        //TODO 슬라이더 초기값 설정
    }

    public void ShowKeyMappingPopup() // 키매핑 팝업 열기
    {
        
    }

    public void OnExit() // 돌아가기
    {
        Close();
    }
}
