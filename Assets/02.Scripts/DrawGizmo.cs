using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmo : MonoBehaviour
{
    public bool isItem; // ���������� �ƴ���
    void OnDrawGizmos()
    {
        if(isItem)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, 1f);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 1f);
        }
    }
}
