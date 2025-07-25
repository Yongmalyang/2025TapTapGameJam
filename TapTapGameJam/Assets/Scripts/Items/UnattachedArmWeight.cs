using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnattachedArmWeight : MonoBehaviour
{
    GameObject player;

    void Start()
    {
        player = GameManager.Instance.Player;
        float x = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        int armWeightType = 0;
        string tag = gameObject.tag;
        switch (tag)
        {
            case "ArmWeight1": armWeightType = 0; break;
            case "ArmWeight2": armWeightType = 1; break;
            case "ArmWeight3": armWeightType = 2; break;
            case "ArmWeight4": armWeightType = 3; break;
            default: armWeightType = 0; break;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            // 1. 충돌 지점 얻기
            Vector2 contactPoint = collision.GetContact(0).point;

            // 2. Player 오브젝트의 SpriteRenderer에서 bounds 구하기
            SpriteRenderer sr = collision.gameObject.GetComponent<SpriteRenderer>();
            Bounds bounds = sr.bounds;

            // 3. 네 코너 좌표 구하기 (월드 기준)
            Vector2 topRight = new Vector2(bounds.max.x, bounds.max.y);
            Vector2 bottomRight = new Vector2(bounds.max.x, bounds.min.y);
            Vector2 topLeft = new Vector2(bounds.min.x, bounds.max.y);
            Vector2 bottomLeft = new Vector2(bounds.min.x, bounds.min.y);

            // 4. 네 코너와 충돌 지점 간 거리 측정
            float distTopRight = Vector2.Distance(contactPoint, topRight);
            float distBottomRight = Vector2.Distance(contactPoint, bottomRight);
            float distTopLeft = Vector2.Distance(contactPoint, topLeft);
            float distBottomLeft = Vector2.Distance(contactPoint, bottomLeft);

            // 5. 최소 거리로 가장 가까운 코너 판별
            float minDist = Mathf.Min(distTopRight, distBottomRight, distTopLeft, distBottomLeft);

            // LA
            if (minDist == distTopLeft) player.GetComponent<Player>().AttachArmWeight(0, armWeightType);
            // RA
            else if (minDist == distTopRight) player.GetComponent<Player>().AttachArmWeight(1, armWeightType);
            // LL
            else if (minDist == distBottomLeft) player.GetComponent<Player>().AttachArmWeight(2, armWeightType);
            // RL
            else player.GetComponent<Player>().AttachArmWeight(3, armWeightType);

            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("LeftArm")) { player.GetComponent<Player>().AttachArmWeight(0, armWeightType); Destroy(gameObject); }
        else if (collision.gameObject.CompareTag("RightArm")) { player.GetComponent<Player>().AttachArmWeight(1, armWeightType); Destroy(gameObject); }
        else if (collision.gameObject.CompareTag("LeftLeg")) { player.GetComponent<Player>().AttachArmWeight(2, armWeightType); Destroy(gameObject); }
        else if (collision.gameObject.CompareTag("RightLeg")) { player.GetComponent<Player>().AttachArmWeight(3, armWeightType); Destroy(gameObject); }

    }
}
