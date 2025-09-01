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
    //TODO : 공격할 수 있는 대상인지
    public void OnShot(BulletType bulletType); //공격 방식(데미지, 발사 각도)
}
