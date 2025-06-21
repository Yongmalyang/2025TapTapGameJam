using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameOver : MonoBehaviour
{
    Image gameOverUI;

    void Start()
    {
        gameOverUI = GetComponent<Image>();
    }

    public void ShowGameOverPanel()
    {
        Debug.Log("game over panel show");
        gameObject.SetActive(true);
        gameOverUI.DOFade(0.5f, 1f)
            .SetEase(Ease.InOutSine)
            .SetUpdate(true); // <- 타임스케일 무시!
    }
}
