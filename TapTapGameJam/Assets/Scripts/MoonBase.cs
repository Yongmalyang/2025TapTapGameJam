using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonBase : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player�� Ʈ���� ������ ���Դ�!");
            // ���⿡ ���ϴ� ������ �־���
            GameManager.Instance.StageClear();
        }
    }
}
