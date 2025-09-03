using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyTest : MonoBehaviour
{
    [SerializeField] Enemy m_Enemy;
    public int spawnEnemyId;
    private InputAction spaceAction;

    private void OnEnable()
    {
        // 스페이스바 액션 직접 생성
        spaceAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/space");

        // performed 이벤트 등록
        spaceAction.started += OnSpacePressed;

        spaceAction.Enable();
    }

    private void OnDisable()
    {
        spaceAction.Disable();
    }

    private void OnSpacePressed(InputAction.CallbackContext ctx)
    {
        EnemyPlaceManager.Instance.GetEnemyById(spawnEnemyId, new Vector3(0,0,0));
        //(m_Enemy as Monster.IDamageable).TakeDamage(10);
    }
}
