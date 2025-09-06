using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GenericManagers.SingleTon<GameManager>
{
    private Vector3 checkPoint;
    public Vector3 GetCheckPoint { get { return checkPoint; } }
    protected override void Init()
    {
        
    }

    public void RegistCheckPoint(Vector3 pos)
    {
        checkPoint = pos;
        Debug.Log("체크포인트 기록" + pos);
    }
}
