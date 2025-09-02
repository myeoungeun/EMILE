using UnityEngine;

public abstract class PopupBase : UIBase
{
    protected virtual void OnEnable()
    {
        // 팝업 열릴 때 필요한 추가 로직
    }

    protected virtual void OnDisable()
    {
        // 팝업 닫힐 때 필요한 추가 로직
    }
}
