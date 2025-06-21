using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private Transform target;     // ���� ��� (�÷��̾�)
    public Vector3 offset;       // ī�޶�� Ÿ�� �� �Ÿ�
    public float smoothSpeed = 0.125f; // �ε巴�� ���󰡴� ����

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
