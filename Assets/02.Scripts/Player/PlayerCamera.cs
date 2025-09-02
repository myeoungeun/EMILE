using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Transform playerTR;
    [Range(0f,1f)][SerializeField] float sensitivity;
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 curr = Vector3.Lerp(transform.position , playerTR.position , sensitivity);
        curr.z = -10f;
        transform.position = curr;
    }
}
