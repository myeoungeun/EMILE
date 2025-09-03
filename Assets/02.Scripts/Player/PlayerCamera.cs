using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Transform playerTR;
    [Range(0f,1f)][SerializeField] float sensitivity;
    [SerializeField] Vector2 offset;
    [SerializeField]float minY;
    [SerializeField]float maxY;
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 curr = Vector3.Lerp(transform.position , playerTR.position , sensitivity);
        curr.x += offset.x;


        if (curr.y < minY) curr.y = minY;
        else if (curr.y > maxY) curr.y = maxY;
        else curr.y += offset.y;

        curr.z = -10f;
        transform.position = curr;
    }
}
