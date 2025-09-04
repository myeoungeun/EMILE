using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : AttackBase, IAttackHandler
{
    public PlayerMovement playerMovement; //방향
    public GameObject bulletPrefab; //총알
    public Transform bulletStartTransform; //시작 위치
    
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
            StartShooting();
        else if (context.phase == InputActionPhase.Canceled)
            StopShooting();
    }

    protected override Transform GetBulletStart()
    {
        return bulletStartTransform;
    }

    protected override GameObject GetBulletPrefab()
    {
        return bulletPrefab;
    }
}
