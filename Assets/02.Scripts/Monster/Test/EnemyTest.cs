using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyTest : MonoBehaviour
{
    [SerializeField] Enemy m_Enemy;
    [SerializeField] Vector3 spawnPos;

    public int spawnEnemyId;
    private InputAction spaceAction;
    private InputAction Action1;

    private void OnEnable()
    {
        // 스페이스바 액션 직접 생성
        spaceAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/space");
        Action1 = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/1");

        // performed 이벤트 등록
        spaceAction.started += OnSpacePressed;
        Action1.started += on1Pressed;

        spaceAction.Enable();
        Action1.Enable();
    }

    private void OnDisable()
    {
        spaceAction.Disable();
        Action1.Disable();
    }

    private void OnSpacePressed(InputAction.CallbackContext ctx)
    {
        EnemyPlaceManager.Instance.GetEnemyById(spawnEnemyId, spawnPos);
    }

    private void on1Pressed(InputAction.CallbackContext ctx)
    {
        if(m_Enemy != null)
            (m_Enemy as IDamageable).TakeDamage(10);
    }
}
