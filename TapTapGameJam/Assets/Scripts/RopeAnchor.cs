using UnityEngine;

public class RopeAnchor : MonoBehaviour
{
    public GameObject[] ropeSegments; // Circle 오브젝트 3개
    public float floatStrength = 0.5f; // 위로 뜨는 힘
    public float floatFrequency = 1f;  // 위아래 진동 주기
    public float sideMovementAmplitude = 0.2f;
    public float sideMovementFrequency = 0.5f;

    private Vector2 startPosition;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = rb.position;

        //SetupRope();
    }

    void FixedUpdate()
    {
        FloatMotion();
    }

    void FloatMotion()
    {
        float time = Time.time;

        // 위아래 부드럽게 움직임 (사인파)
        float floatOffset = Mathf.Sin(time * floatFrequency) * floatStrength;

        // 좌우 살짝 흔들림
        float sideOffset = Mathf.Sin(time * sideMovementFrequency) * sideMovementAmplitude;

        Vector2 targetPos = startPosition + new Vector2(sideOffset, floatOffset);

        // 물리 기반으로 부드럽게 따라가게 하기
        Vector2 forceDir = (targetPos - rb.position) * 2f; // 튀는 느낌 방지
        rb.AddForce(forceDir, ForceMode2D.Force);
    }
    /*
    void SetupRope()
    {
        Rigidbody2D prevRb = GetComponent<Rigidbody2D>();

        for (int i = 0; i < ropeSegments.Length; i++)
        {
            GameObject segment = ropeSegments[i];

            // Rigidbody
            Rigidbody2D rb = segment.GetComponent<Rigidbody2D>();
            if (rb == null) rb = segment.AddComponent<Rigidbody2D>();

            rb.gravityScale = 0.2f;       // 물 위에 살짝 둥둥 뜨게
            rb.mass = 0.3f;

            // 초기 위치: 오른쪽에 줄줄이 배치 (시작점 기준 + 오프셋)
            segment.transform.position = transform.position + new Vector3((i + 1) * 0.6f, 0, 0);

            // Joint
            HingeJoint2D joint = segment.GetComponent<HingeJoint2D>();
            if (joint == null) joint = segment.AddComponent<HingeJoint2D>();

            joint.connectedBody = prevRb;
            joint.autoConfigureConnectedAnchor = false;

            joint.anchor = new Vector2(-0.3f, 0f);           // 내 왼쪽 (Circle)
            joint.connectedAnchor = new Vector2(0.3f, 0f);   // 연결 대상의 오른쪽

            prevRb = rb;
        }
    }
    */
}
