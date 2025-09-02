using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyMappingPopup : PopupBase
{
    [Header("버튼")]
    [SerializeField] private Button saveButton;
    [SerializeField] private Button exitButton;

    [Header("키매핑 버튼")]
    [SerializeField] private Button[] keyButtons; // 키매핑 버튼 배열


    [Header("키매핑 버튼 텍스트")]
    [SerializeField] private TextMeshProUGUI[] keyTexts; // 키매핑 버튼 텍스트 배열


    public override void Initialize()
    {
        InitKeyMapping();
    }

    private void OnKeyButton(int index)
    {
        //TODO 키매핑 기능 추가
    }

    public void OnSaveButton()
    {
        //TODo 키매핑 세이브 기능 추가
    }

    public void OnExitButton()
    {
        UIManager.Instance.ClosePopup(PopupType.KeyMapping);
        UIManager.Instance.ShowPopup(PopupType.Option);

        // TODO 키를 변경하고 적용하지 않았을 경우에 적용 할 건지 묻는 팝업창을 띄운다.
        // 적용 여부 팝업창 만들기
    }

    private void InitKeyMapping() // 키매핑 배열 초기화
    {
        if(keyButtons.Length != keyTexts.Length)
        {
            Debug.LogError("keyButtons와 keyTexts의 배열 길이가 다름!");
            return;
        }
        else
        {
            for (int i = 0; i < keyButtons.Length; i++)
            {
                int index = i; // 람다식 캡처 문제 방지 (람다식은 반복문이 끝날 때의 최종값만 참조)
                keyButtons[i].onClick.RemoveAllListeners(); // 이벤트 중복호출 방지
                keyButtons[i].onClick.AddListener(() => OnKeyButton(index));
            }

        }
    }
}
