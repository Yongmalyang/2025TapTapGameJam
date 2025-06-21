using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageClearUI : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> clearTextSprite = new List<Sprite>();
    [SerializeField]
    private Image clearTextTarget;
    [SerializeField]
    private Button NextButton;
    [SerializeField]
    private Image missionCompleteImage;

    public void ShowStageClearPanel(int stageNum)
    {
        gameObject.SetActive(true);
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        if (stageNum == 3)
        {
            missionCompleteImage.gameObject.SetActive(true);
        }
        else
        {
            clearTextTarget.sprite = clearTextSprite[stageNum];
            clearTextTarget.gameObject.SetActive(true);
            NextButton.gameObject.SetActive(true);
        }

    }
}
