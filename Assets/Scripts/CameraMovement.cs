using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target; // 카메라가 따라갈 대상
    public float smoothSpeed = 1f; // 카메라 움직임의 부드러움을 조절
    public Vector3 offset; // 타겟과 카메라 사이의 오프셋

    private void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // 타겟의 위치에 오프셋을 더합니다 (z 값은 카메라의 초기 z 값을 유지)
            Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);
            // Vector3.Lerp를 사용해 부드럽게 위치를 변경
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            // 카메라의 위치를 업데이트
            transform.position = smoothedPosition;
        }
    }
}
