using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonBase : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player가 트리거 안으로 들어왔다!");
            // 여기에 원하는 동작을 넣어줘
            GameManager.Instance.StageClear();
        }
    }
}
