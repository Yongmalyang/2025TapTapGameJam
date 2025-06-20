using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArmWeight : MonoBehaviour
{
    [SerializeField]
    private float ratio;

    public void AttatchSelfToArm(GameObject ConnectedBody, bool isFirstArm, bool isLeft)
    {
        Rigidbody2D ConnectedRB = ConnectedBody.GetComponent<Rigidbody2D>();

        // ����Ʈ �θ� �����ϱ�
        HingeJoint2D hinge = GetComponent<HingeJoint2D>();
        DistanceJoint2D distance = GetComponent<DistanceJoint2D>();

        hinge.connectedBody = ConnectedRB;
        distance.connectedBody = ConnectedRB;

        // ���� ���ո� ���ϱ� (anchor)
        float MyRatio = ratio;
        float ParentRatio=0;

        if (isFirstArm) ParentRatio = ConnectedBody.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        else ParentRatio = ConnectedBody.GetComponent<ArmWeight>().ratio;

        // ���ʿ� ������ �����ʿ� ������
        float finalAnchor = 0;

        if (isLeft) finalAnchor = - ParentRatio - MyRatio;
        else finalAnchor = ParentRatio + MyRatio;

        hinge.connectedAnchor = new Vector2(finalAnchor, hinge.connectedAnchor.y);
        distance.connectedAnchor = new Vector2(finalAnchor, distance.connectedAnchor.y);

        // ���� ��ġ ���ϱ� (position)
        Vector3 posA = ConnectedBody.transform.position;
        float rotZ = ConnectedBody.transform.eulerAngles.z;
        float dist = ConnectedBody.GetComponent<SpriteRenderer>().bounds.size.x + GetComponent<SpriteRenderer>().bounds.size.x;

        Vector3 offset = new Vector3(0, 0, 0);

        if (isLeft) offset = Quaternion.Euler(0, 0, rotZ) * Vector3.left * dist;
        else offset = Quaternion.Euler(0, 0, rotZ) * Vector3.right * dist;

        Vector3 posB = posA + offset;

        transform.position = posB;
    }

}
