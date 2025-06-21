using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oxygen : MonoBehaviour
{
    [SerializeField]
    private int oxygenPerCycle = 10;       // 한번에 주는 산소량
    [SerializeField]
    private float interval = 1f;           // 주기 (초)
    [SerializeField]
    private int maxCycles = 5;             // 총 공급 횟수

    private int usedCycles = 0;           // 이미 사용한 횟수
    private bool playerInside = false;    // 플레이어가 안에 있는가
    private Coroutine oxygenCoroutine;    // 산소 공급 루프
    /*
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("산소 영역 진입");
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
                Destroy(gameObject); // 산소 다 주면 사라짐
                yield break;
            }

            yield return new WaitForSeconds(interval);
        }

        oxygenCoroutine = null;
    }
}
