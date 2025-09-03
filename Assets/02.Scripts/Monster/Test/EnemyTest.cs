using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    [SerializeField] Enemy m_Enemy;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Monster.IDamageable damageable = m_Enemy as Monster.IDamageable;
            damageable.TakeDamage(10);
        }
    }
}
