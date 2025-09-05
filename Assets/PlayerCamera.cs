using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Transform playerTR;
    [Range(0f,10f)][SerializeField] float sensitivity;
    [SerializeField] Vector2 offset;
    [SerializeField]float minY;
    [SerializeField]float maxY;
    // Update is called once per frame
    private IEnumerator Start()
    {
        Player stat = playerTR.parent.GetComponent<Player>();
        yield return new WaitUntil(() => stat.Stat != null);
        playerTR.parent.GetComponent<Player>().Stat.Respawn += ResetPosition;
    }
    void FixedUpdate()
    {
        float dist = GetEuclidDist(transform.position, playerTR.position);
        Vector3 curr;
        if (dist > 0)
        {
             curr = Vector3.Lerp(transform.position, playerTR.position, sensitivity / dist);
        }
        else
        {
            curr = Vector3.Lerp(transform.position, playerTR.position, sensitivity);
        }
        curr.x += offset.x;


        if (curr.y < minY) curr.y = minY;
        else if (curr.y > maxY) curr.y = maxY;
        else curr.y += offset.y;

        curr.z = -10f;
        transform.position = curr;
    }
    public void ResetPosition()
    {
        Vector3 pos = GameManager.GetInstance.GetCheckPoint;
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }
    private float GetEuclidDist(Vector2 a , Vector2 b)
    {
        return Mathf.Pow((a.x - b.x),2) + Mathf.Pow((a.y - b.y),2);
    }
}
