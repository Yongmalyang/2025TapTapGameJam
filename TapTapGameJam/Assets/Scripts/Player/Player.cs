using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public float floatForce = 2f;        // 떠오르는 힘
    public Transform capsuleSprite; // ← CapsuleSprite를 Inspector에 할당할 것
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
        GameObject mainArm = LeftArm; //임시로
        GameObject armWeight = Instantiate(ArmWeightPrefab, mainArm.transform);
        float armWeightWidth = armWeight.GetComponent<SpriteRenderer>().bounds.size.x;

        int childCount = mainArm.transform.childCount;

        // 이미 생성되어서 붙어있는 시점에서의 팔 개수
        // 이미 부착했으니까 자식 1개
        if (childCount == 1)
        {
            // 자기 자신에게 붙이기
            armWeight.GetComponent<ArmWeight>().AttatchSelfToArm(gameObject, true ,true);
        }
        else if(childCount > 1)
        {
            // 마지막 팔에 붙이기, 내 직전 친구
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
