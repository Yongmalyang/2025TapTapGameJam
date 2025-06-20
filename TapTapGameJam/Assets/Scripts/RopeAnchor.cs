using UnityEngine;

public class RopeAnchor : MonoBehaviour
{
    public GameObject[] ropeSegments; // Circle ������Ʈ 3��
    public float floatStrength = 0.5f; // ���� �ߴ� ��
    public float floatFrequency = 1f;  // ���Ʒ� ���� �ֱ�
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

        // ���Ʒ� �ε巴�� ������ (������)
        float floatOffset = Mathf.Sin(time * floatFrequency) * floatStrength;

        // �¿� ��¦ ��鸲
        float sideOffset = Mathf.Sin(time * sideMovementFrequency) * sideMovementAmplitude;

        Vector2 targetPos = startPosition + new Vector2(sideOffset, floatOffset);

        // ���� ������� �ε巴�� ���󰡰� �ϱ�
        Vector2 forceDir = (targetPos - rb.position) * 2f; // Ƣ�� ���� ����
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

            rb.gravityScale = 0.2f;       // �� ���� ��¦ �յ� �߰�
            rb.mass = 0.3f;

            // �ʱ� ��ġ: �����ʿ� ������ ��ġ (������ ���� + ������)
            segment.transform.position = transform.position + new Vector3((i + 1) * 0.6f, 0, 0);

            // Joint
            HingeJoint2D joint = segment.GetComponent<HingeJoint2D>();
            if (joint == null) joint = segment.AddComponent<HingeJoint2D>();

            joint.connectedBody = prevRb;
            joint.autoConfigureConnectedAnchor = false;

            joint.anchor = new Vector2(-0.3f, 0f);           // �� ���� (Circle)
            joint.connectedAnchor = new Vector2(0.3f, 0f);   // ���� ����� ������

            prevRb = rb;
        }
    }
    */
}
