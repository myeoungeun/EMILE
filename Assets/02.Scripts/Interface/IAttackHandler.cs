using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackHandler
{
    //������ �� �ִ� �������
    public void OnShot(out int damage, out float shotAngle); //���� ���(������, �߻� ����)
}
