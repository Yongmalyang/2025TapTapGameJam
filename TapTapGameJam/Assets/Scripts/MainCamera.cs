using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private Transform target;     // 따라갈 대상 (플레이어)
    public Vector3 offset;       // 카메라와 타겟 간 거리
    public float smoothSpeed = 0.125f; // 부드럽게 따라가는 정도

    private void Start()
    {
        target = GameManager.Instance.Player.transform;
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}
