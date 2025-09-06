using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public Transform cam;        // 메인 카메라
    public float parallaxSpeed;  // 0~1 (작을수록 느림)

    private Vector3 startPos;
    private float length;

    void Start()
    {
        startPos = transform.position;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float dist = cam.position.x * parallaxSpeed;
        transform.position = new Vector3(startPos.x + dist, startPos.y, startPos.z);

        // 무한 반복 처리
        float temp = cam.position.x * (1 - parallaxSpeed);
        if (temp > startPos.x + length) startPos.x += length;
        else if (temp < startPos.x - length) startPos.x -= length;
    }
}
