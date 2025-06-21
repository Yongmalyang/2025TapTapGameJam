using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public float floatForce = 2f;            // 떠오르는 힘
    public float playerRatio;                // 플레이어의 무게나 비율 등
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

    private Vector2 inputDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 키 입력 받아두기만 함 (FixedUpdate에서 사용)
        inputDirection = Vector2.zero;

        if (Input.GetKey(KeyCode.A)) inputDirection += Vector2.left;
        if (Input.GetKey(KeyCode.D)) inputDirection += Vector2.right;
        if (Input.GetKey(KeyCode.W)) inputDirection += Vector2.up;
        if (Input.GetKey(KeyCode.S)) inputDirection += Vector2.down;
    }

    void FixedUpdate()
    {
        rb.velocity = inputDirection.normalized * floatForce;

        // 이동 중일 때만 회전
        if (inputDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(inputDirection.y, inputDirection.x) * Mathf.Rad2Deg;
            Quaternion targetRot = Quaternion.Euler(0, 0, angle);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRot, Time.fixedDeltaTime * 5f);
        }
    }

    // index: LA, RA, LL, RL
    public void AttachArmWeight(int index, int armWeightType)
    {
        Debug.Log(index);

        GameObject mainArm;
        string armTag;

        switch (index)
        {
            case 0: // LA
                mainArm = LeftArm;
                armTag = "LeftArm";
                break;
            case 1: // RA
                mainArm = RightArm;
                armTag = "RightArm";
                break;
            case 2: // LL
                mainArm = LeftLeg;
                armTag = "LeftLeg";
                break;
            case 3: // RL
                mainArm = RightLeg;
                armTag = "RightLeg";
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

        if (childCount == 1)
        {
            armWeight.GetComponent<ArmWeight>().AttatchSelfToArm(gameObject, true, index % 2 == 0);
        }
        else if (childCount > 1)
        {
            GameObject nthChild = mainArm.transform.GetChild(childCount - 2).gameObject;
            armWeight.GetComponent<ArmWeight>().AttatchSelfToArm(nthChild, false, index % 2 == 0);
        }
    }
}
