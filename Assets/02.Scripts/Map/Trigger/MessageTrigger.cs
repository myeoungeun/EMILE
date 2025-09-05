using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageTrigger : MonoBehaviour
{
    [SerializeField] private string message;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null && collision.CompareTag("Player"))
        {
            UIManager.Instance.InGameUI.UITextHUD.SetText(message);
        }
    }
}
