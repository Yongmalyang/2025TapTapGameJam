using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oxygen : MonoBehaviour
{
    [SerializeField]
    private int oxygenPerCycle = 10;       // �ѹ��� �ִ� ��ҷ�
    [SerializeField]
    private float interval = 1f;           // �ֱ� (��)
    [SerializeField]
    private int maxCycles = 5;             // �� ���� Ƚ��

    private int usedCycles = 0;           // �̹� ����� Ƚ��
    private bool playerInside = false;    // �÷��̾ �ȿ� �ִ°�
    private Coroutine oxygenCoroutine;    // ��� ���� ����
    /*
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("��� ���� ����");
            GameManager.Instance.oxygenAmount += 10;
            GameManager.Instance.Player.GetComponent<Player>().UI.UpdateOxygenUI(10);
        }

    }
    */

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && usedCycles < maxCycles)
        {
            playerInside = true;
            if (oxygenCoroutine == null)
                oxygenCoroutine = StartCoroutine(GiveOxygenRoutine(other.GetComponent<Player>()));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            if (oxygenCoroutine != null)
            {
                StopCoroutine(oxygenCoroutine);
                oxygenCoroutine = null;
            }
        }
    }

    private IEnumerator GiveOxygenRoutine(Player player)
    {
        while (playerInside && usedCycles < maxCycles)
        {
            GameManager.Instance.oxygenAmount += oxygenPerCycle;
            player.UI.UpdateOxygenUI(GameManager.Instance.oxygenAmount);
            usedCycles++;

            if (usedCycles >= maxCycles)
            {
                Destroy(gameObject); // ��� �� �ָ� �����
                yield break;
            }

            yield return new WaitForSeconds(interval);
        }

        oxygenCoroutine = null;
    }
}
