using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectState : EnemyBaseState
{
    // ���⼭ �ؾ��ϴ� ��
    // �÷��̾ ���ݹ��� ���� ������ ���� ���·� ����
    // �÷��̾ ���� ���� ������ ������

    private float attackRange;
    private float detectRange;

    public EnemyDetectState(EnemyStateMachine owner) : base(owner)
    {
    }
}
