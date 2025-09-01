using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackHandler
{
    //공격할 수 있는 대상인지
    public void OnShot(out int damage, out float shotAngle); //공격 방식(데미지, 발사 각도)
}
