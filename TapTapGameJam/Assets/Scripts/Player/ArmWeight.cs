using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArmWeight : MonoBehaviour
{
    [SerializeField]
    private float ratio;
    // LA, RA, LL, RL
    private List<Vector2> armOffset = new List<Vector2> { new Vector2(-0.398f, -0.306f), new Vector2(0.305f, -0.319f), new Vector2(-0.217f, -0.673f), new Vector2(0.118f, -0.678f)};

    public void AttatchSelfToArm(GameObject ConnectedBody, bool isFirstArm, int armIndex)
    {
        Rigidbody2D ConnectedRB = ConnectedBody.GetComponent<Rigidbody2D>();

        // 조인트 부모 설정하기
        HingeJoint2D hinge = GetComponent<HingeJoint2D>();
        DistanceJoint2D distance = GetComponent<DistanceJoint2D>();

        hinge.connectedBody = ConnectedRB;
        distance.connectedBody = ConnectedRB;

        // 붙일 접합면 정하기 (anchor)
        float MyRatio = ratio;

        hinge.anchor = new Vector2(0, MyRatio);
        distance.anchor = new Vector2(0, MyRatio);

        if (isFirstArm)
        {
            hinge.connectedAnchor = armOffset[armIndex];
            distance.connectedAnchor = armOffset[armIndex];
        }
        else
        {
            hinge.connectedAnchor = new Vector2(0, -ConnectedBody.GetComponent<ArmWeight>().ratio);
            distance.connectedAnchor = new Vector2(0, -ConnectedBody.GetComponent<ArmWeight>().ratio);
        }
        
    }

}
