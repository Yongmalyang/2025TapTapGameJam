using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public float floatForce = 2f;        // �������� ��
    public float playerRatio;        // �������� ��
    public PlayerUI UI;

    private Rigidbody2D rb;

    [SerializeField]
    private List<GameObject> ArmWeightPrefab = new List<GameObject>();
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

    // index: LA, RA, LL, RL
    public void AttachArmWeight(int index, int armWeightType)
    {
        Debug.Log(index);

        GameObject mainArm;
        string armTag;

        switch (index)
        {
            case 0: //LA
                mainArm = LeftArm;
                armTag = "LeftArm";
                break;
            case 1: //RA
                mainArm = RightArm;
                armTag = "RightArm";
                break;
            case 2: //LL
                //mainArm = LeftLeg;
                mainArm = LeftArm;
                armTag = "LeftArm";
                break;
            case 3: //RL
                //mainArm = RightLeg;
                mainArm = RightArm;
                armTag = "RightArm";
                break;
            default:
                mainArm = LeftArm;
                armTag = "LeftArm";
                break;
        }

        GameObject armWeight = Instantiate(ArmWeightPrefab[armWeightType], mainArm.transform);
        armWeight.tag = armTag;
        float armWeightWidth = armWeight.GetComponent<SpriteRenderer>().bounds.size.x;

        int childCount = mainArm.transform.childCount;

        // �̹� �����Ǿ �پ��ִ� ���������� �� ����
        // �̹� ���������ϱ� �ڽ� 1��

        // ¦���� isLeft�� true
        if (childCount == 1)
        {
            // �ڱ� �ڽſ��� ���̱�
            armWeight.GetComponent<ArmWeight>().AttatchSelfToArm(gameObject, true, index % 2 == 0);
        }
        else if (childCount > 1)
        {
            // ������ �ȿ� ���̱�, �� ���� ģ��
            GameObject nthChild = mainArm.transform.GetChild(childCount - 2).gameObject;
            armWeight.GetComponent<ArmWeight>().AttatchSelfToArm(nthChild, false, index % 2 == 0);
        }
    }
}