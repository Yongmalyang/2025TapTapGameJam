using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FloatingObject : MonoBehaviour
{
    public float floatForce = 2f;        // �������� ��
    public Transform capsuleSprite; // �� CapsuleSprite�� Inspector�� �Ҵ��� ��
    //public float maxFloatSpeed = 2f;     // �ִ� ��� �ӵ� ����
    //public float gravityScale = 1f;      // �߷� ����

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //rb.gravityScale = gravityScale;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("A input");
            rb.velocity = new Vector2(-floatForce, floatForce);  // x: ������, y: �Ʒ���
            Quaternion targetRot = Quaternion.Euler(0, 0, 5f); // A ���� ����
            capsuleSprite.localRotation = Quaternion.Lerp(capsuleSprite.localRotation, targetRot, Time.deltaTime * 5f);
        }

        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("D input");
            rb.velocity = new Vector2(floatForce, floatForce);   
            Quaternion targetRot = Quaternion.Euler(0, 0, -5f); 
            capsuleSprite.localRotation = Quaternion.Lerp(capsuleSprite.localRotation, targetRot, Time.deltaTime * 5f);
        }

        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("W input");
            rb.velocity = new Vector2(0, floatForce);  
            Quaternion targetRot = Quaternion.Euler(0, 0, 0); 
            capsuleSprite.localRotation = Quaternion.Lerp(capsuleSprite.localRotation, targetRot, Time.deltaTime * 5f);
        }

        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("S input");
            rb.velocity = new Vector2(0, -floatForce);   
            Quaternion targetRot = Quaternion.Euler(0, 0, 0); 
            capsuleSprite.localRotation = Quaternion.Lerp(capsuleSprite.localRotation, targetRot, Time.deltaTime * 5f);
        }
    }
}
