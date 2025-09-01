using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    Normal,
    Strong,
    Wide
}

public interface IAttackHandler
{
    //������ �� �ִ� �������
    public void OnShot(BulletType bulletType); //���� ���(������, �߻� ����)
}
