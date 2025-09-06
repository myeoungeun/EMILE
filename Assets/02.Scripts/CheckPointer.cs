using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            GameManager.GetInstance.RegistCheckPoint(transform.position);
            
            Destroy(this);
        }
    }
}
