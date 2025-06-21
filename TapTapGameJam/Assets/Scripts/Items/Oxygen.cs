using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oxygen : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("��� ���� ����");
            GameManager.Instance.oxygenAmount += 10;
            GameManager.Instance.Player.GetComponent<Player>().UI.UpdateOxygenUI(10);
        }

    }

}
