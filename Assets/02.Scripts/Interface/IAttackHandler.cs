using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    Normal,
    Pierce,
    Hollow
}

public interface IAttackHandler
{
    //TODO : ������ �� �ִ� �������
    public void OnShot(BulletType bulletType); //���� ���(������, �߻� ����)
}
