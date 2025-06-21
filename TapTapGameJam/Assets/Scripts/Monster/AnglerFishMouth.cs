using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnglerFishMouth : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        string[] armTags = { "ArmWeight1", "ArmWeight2", "ArmWeight3", "ArmWeight4" };

        // �±� ����Ʈ �ȿ� �ִ� ��츸 ����
        if (System.Array.Exists(armTags, tag => other.CompareTag(tag)))
        {
            Destroy(other.gameObject);
        }
    }
}
