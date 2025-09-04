using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITextHUD : UIBase
{
    [SerializeField] private TextMeshProUGUI uiText;
    public override void Initialize()
    {
        uiText.text = "";
    }

    public void SetText(string message)
    {
        if (uiText != null)
            uiText.text = message;
    }
}
