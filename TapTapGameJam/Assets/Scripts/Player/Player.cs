using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public float floatForce = 2f;        // 떠오르는힘
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

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A)) MovePlayer(-1, 0, 5f);
        if (Input.GetKey(KeyCode.D)) MovePlayer(1, 0, -5f);
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
                mainArm = LeftLeg;
                armTag = "LeftLeg";
                break;
            case 3: //RL
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

        // 이미 생성되어서 붙어있는 시점에서의 팔 개수
        // 즉 이미 부착했으니까 팔 1개

        // 짝수면 isLeft가 true
        if (childCount == 1)
        {
            // 자기 자신에게 붙이기
            armWeight.GetComponent<ArmWeight>().AttatchSelfToArm(gameObject, true, index);
        }
        else if (childCount > 1)
        {
            // 마지막 팔에 붙이기, 내 직전 친구
            GameObject nthChild = mainArm.transform.GetChild(childCount - 2).gameObject;
            armWeight.GetComponent<ArmWeight>().AttatchSelfToArm(nthChild, false, index);
        }

        int addedWeight = ArmWeightPrefab[armWeightType].GetComponent<ArmWeight>().weight;
        GameManager.Instance.curPlayerWeight += addedWeight;
        GameManager.Instance.mainUI.UpdateUIText(GameManager.Instance.curPlayerWeight);

        int curStage = GameManager.Instance.curStageNum;
        if (GameManager.Instance.curPlayerWeight >= GameManager.Instance.goalWeight[curStage])
        {
            Debug.Log("clear stage");
            GameManager.Instance.ResetAndGoToNextStage();
        }
            
            
    }
}