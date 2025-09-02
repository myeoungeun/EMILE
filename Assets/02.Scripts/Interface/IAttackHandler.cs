using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    Player,
    Enemy
}

public enum BulletType
{
    Normal,
    Pierce,
    Hollow,
    Homing
}

public interface IAttackHandler
{
    public void OnShot(int id); //공격 방식(데미지, 발사 각도)
}
