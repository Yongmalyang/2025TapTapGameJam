using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Oxygen : MonoBehaviour
{
    [SerializeField]
    private int oxygenPerCycle = 10;       // 한번에 주는 산소량
    [SerializeField]
    private float interval = 1f;           // 주기 (초)
    [SerializeField]
    private int maxCycles = 5;             // 총 공급 횟수
    [SerializeField]
    private GameObject oxygenTextPrefab;

    private int usedCycles = 0;           // 이미 사용한 횟수
    private bool playerInside = false;    // 플레이어가 안에 있는가
    private Coroutine oxygenCoroutine;    // 산소 공급 루프
    private SpriteRenderer sr;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

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
            SpawnOxygenText(player.transform.position, oxygenPerCycle);
            usedCycles++;

            if (usedCycles >= maxCycles)
            {
                // 산소 다 주면 페이드 아웃
                StartCoroutine(FadeOutAndDestroy());
                yield break;
            }

            yield return new WaitForSeconds(interval);
        }

        oxygenCoroutine = null;
    }

    private IEnumerator FadeOutAndDestroy()
    {
        float fadeDuration = 1.0f;
        float elapsed = 0f;
        Color originalColor = sr.color;

        while (elapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        Destroy(gameObject);
    }

    private void SpawnOxygenText(Vector3 position, int amount)
    {
        Vector3 randomOffset = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(0.5f, 1f), 0f);
        GameObject textObj = Instantiate(oxygenTextPrefab, position + randomOffset, Quaternion.identity, GameManager.Instance.Player.GetComponent<Player>().UI.transform);
        var textMesh = textObj.GetComponentInChildren<TextMeshPro>();
        if (textMesh != null)
            textMesh.text = "+" + amount.ToString();
    }
}
