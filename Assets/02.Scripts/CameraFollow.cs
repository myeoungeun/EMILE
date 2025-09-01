using UnityEngine;

public class SimpleCameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform; // 플레이어 Transform
    [SerializeField] private float lerpSpeed = 0.5f; // 부드러운 이동 속도 (0~1)
    [SerializeField] private float offsetX = 1f; // x축 오프셋 거리 (절댓값)

    private void LateUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        if (playerTransform == null) return;

        // 플레이어 방향 확인 (1: 오른쪽, -1: 왼쪽)
        float playerFacing = Mathf.Sign(playerTransform.localScale.x);

        // 방향에 따라 offsetX 조정
        float adjustedOffsetX = offsetX * playerFacing;

        Vector3 targetPosition = new Vector3(
            playerTransform.position.x + adjustedOffsetX,
            transform.position.y,
            transform.position.z); // 카메라 Z값 고정

        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed);
    }
}
