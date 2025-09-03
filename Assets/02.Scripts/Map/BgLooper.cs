using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgLooper : MonoBehaviour
{
    [SerializeField] 
    private int numBgCount;

    [SerializeField]
    [Tooltip ("Check O = true / Check X = false")] 
    private bool isMoveingRight = true; // 카메라 이동 방향에 따라 배경 재배치

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.CompareTag("Background"))
        {
            float _widthofBgObject = ((BoxCollider2D)collision).size.x;
            Vector3 pos = collision.transform.position;

            if(isMoveingRight == true)
            {
                pos.x += _widthofBgObject * numBgCount;
                collision.transform.position = pos;
                return;
            }
            else
            {
                pos.x -= _widthofBgObject * numBgCount;
                collision.transform.position = pos;
                return;
            }
        }
    }
}