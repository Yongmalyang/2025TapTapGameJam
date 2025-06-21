using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageClearUI : MonoBehaviour
{
    Image stageClearUI;

    void Start()
    {
        stageClearUI = GetComponent<Image>();
    }

    public void ShowGameOverPanel()
    {
        gameObject.SetActive(true);
        stageClearUI.DOFade(1f, 1f)
            .SetEase(Ease.InOutSine)
            .SetUpdate(true); // <- 타임스케일 무시!
    }
}
