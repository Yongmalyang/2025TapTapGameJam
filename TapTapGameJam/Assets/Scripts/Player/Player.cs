using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public float floatForce = 2f;        // �������� ��
    public Transform capsuleSprite; // �� CapsuleSprite�� Inspector�� �Ҵ��� ��
    //public float curArmLength = 0;

    private Rigidbody2D rb;

    [SerializeField]
    private GameObject ArmWeightPrefab;
    [SerializeField]
    private GameObject LeftArm;
    [SerializeField]
    private GameObject RightArm;
    [SerializeField]
    private GameObject LeftLeg;
    [SerializeField]
    private GameObject RightLeg;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A)) MovePlayer(-1, 1, 5f);
        if (Input.GetKey(KeyCode.D)) MovePlayer(1, 1, -5f);
        if (Input.GetKey(KeyCode.W)) MovePlayer(0, 1, 0);
        if (Input.GetKey(KeyCode.S)) MovePlayer(0, -1, 0);
    }

    void MovePlayer(float dirX, float dirY, float rotateZ)
    {
        rb.velocity = new Vector2(dirX * floatForce, dirY * floatForce);
        Quaternion targetRot = Quaternion.Euler(0, 0, rotateZ);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRot, Time.deltaTime * 5f);

    }

    void AttachArmWeight(int index)
    {
        GameObject mainArm = LeftArm; //�ӽ÷�
        GameObject armWeight = Instantiate(ArmWeightPrefab, mainArm.transform);
        float armWeightWidth = armWeight.GetComponent<SpriteRenderer>().bounds.size.x;

        int childCount = mainArm.transform.childCount;

        // �̹� �����Ǿ �پ��ִ� ���������� �� ����
        // �̹� ���������ϱ� �ڽ� 1��
        if (childCount == 1)
        {
            // �ڱ� �ڽſ��� ���̱�
            armWeight.GetComponent<ArmWeight>().AttatchSelfToArm(gameObject, true ,true);
        }
        else if(childCount > 1)
        {
            // ������ �ȿ� ���̱�, �� ���� ģ��
            GameObject nthChild = mainArm.transform.GetChild(childCount-2).gameObject;
            armWeight.GetComponent<ArmWeight>().AttatchSelfToArm(nthChild, false, true);
        }

        //Debug.Log(curArmLength);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ArmWeight"))
        {
            AttachArmWeight(0);
            Destroy(collision.gameObject);
        }
    }
}
